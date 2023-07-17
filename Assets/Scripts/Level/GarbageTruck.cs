using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

public class GarbageTruck : MonoBehaviour
{
    
    void Start()
    {
        EventManager.Instance.ONCityTrashIsFull += ONCityTrashIsFull;
    }

    private void ONCityTrashIsFull(bool isFull)
    {
        if (!isFull) return;
        LeanTween.moveX(gameObject, 1f, 2f).setEase(LeanTweenType.easeOutQuad);
        Invoke(nameof(AfterTrashCollection), 3f);
    }

    private void AfterTrashCollection()
    {
        EventManager.Instance.OnONCityTrashIsFull(false);
        LeanTween.moveX(gameObject, -20f, 2f).setEase(LeanTweenType.easeInQuad);
    }
}
