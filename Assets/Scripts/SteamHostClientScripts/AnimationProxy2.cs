using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationProxy2 : MonoBehaviour
{
    private CharacterAttack2 _attack;
    private void Start()
    {
        _attack = GetComponentInParent<CharacterAttack2>();
    }
    public void InitiateAttack()
    {
        _attack.AttackNow();
    }
}
