using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    private Animator _animator;


    //Animation Hashes
    private int _runingHash = Animator.StringToHash("RunningMagnitude");
    private int _attackHash = Animator.StringToHash("Attack");
    private int _getHitHash = Animator.StringToHash("GetHit");

    void Start()
    {
        _animator = transform.GetChild(0).GetComponent<Animator>();
    }

    
    public void PlayRuning(float magnitude)
    {
        _animator ??= transform.GetChild(0).GetComponent<Animator>();
        _animator.SetFloat(_runingHash, magnitude);
    }

    public void PlayAttack()
    {
        _animator.SetTrigger(_attackHash);
    }

    public void PlayGetHit()
    {
        _animator.SetTrigger(_getHitHash);
    }
}
