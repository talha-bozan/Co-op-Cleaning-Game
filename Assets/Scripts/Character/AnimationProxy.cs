using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationProxy : MonoBehaviour
{
    private CharacterAttack _attack;
    private void Start()
    {
        _attack = GetComponentInParent<CharacterAttack>();
    }
    public void InitiateAttack()
    {
        _attack.AttackNow();
    }
}
