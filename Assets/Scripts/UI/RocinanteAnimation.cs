using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

public class RocinanteAnimation : MonoBehaviour
{

    [SerializeField] private float horseYTarget;
    [SerializeField] private float horseRotationTime;
    private RectTransform _horse;
    private RectTransform _rocinanteText;
    private LTDescr _horseRotationTween;

    void Start()
    {
        _horse = transform.GetChild(0).GetComponent<RectTransform>();
        _rocinanteText = transform.GetChild(1).GetComponent<RectTransform>();
    
        LeanTween.scale(_rocinanteText,Vector3.one,.75f).setEase(LeanTweenType.easeOutBack).setDelay(1f);
        LeanTween.moveLocalY(_horse.gameObject,horseYTarget,.75f).setEase(LeanTweenType.easeInQuad);
        LeanTween.rotateZ(_horse.gameObject,15f, horseRotationTime*.5f).setEase(LeanTweenType.easeOutQuad).setDelay(.75f).setOnComplete(HorseRotationLoop);
        Invoke(nameof(PlaySfx),.3f);

    }

    private void HorseRotationLoop(){
        _horseRotationTween = LeanTween.rotateZ(_horse.gameObject, -15f, horseRotationTime).setEase(LeanTweenType.easeInOutQuad).setLoopPingPong(30);
    }

    private void PlaySfx(){
        AudioManager.Instance.PlayRocinanteSfx();
    }


    private void OnDisable() => _horseRotationTween.pause();

}
