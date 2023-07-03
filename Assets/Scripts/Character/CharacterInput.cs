using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInput : MonoBehaviour
{

    private CharacterMovement _movement;
    private CharacterAnimation _animation;
    private Vector3 _userInput;

    void Start()
    {
        _movement = GetComponent<CharacterMovement>();
        _animation = GetComponent<CharacterAnimation>();
    }

    
    void Update()
    {
        _userInput.x = Input.GetAxisRaw("Horizontal");
        _userInput.z = Input.GetAxisRaw("Vertical");
        _userInput.Normalize();
        _movement.PlayerInput = _userInput;
        _animation.PlayRuning(_userInput.sqrMagnitude);
        
    }
}
