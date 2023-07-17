using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    private Animator _animator;


    //Animation Hashes
    private int _runingHash = Animator.StringToHash("RunningMagnitude");

    void Start()
    {
        _animator = transform.GetChild(0).GetComponent<Animator>();
    }

    
    public void PlayRuning(float magnitude)
    {
        _animator ??= transform.GetChild(0).GetComponent<Animator>();
        _animator.SetFloat(_runingHash, magnitude);
    }
}
