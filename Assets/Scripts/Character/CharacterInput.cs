using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterInput : MonoBehaviour
{

    private CharacterMovement _movement;
    private CharacterAnimation _animation;
    private CharacterCollection _collection;
    private CharacterAttack _attack;
    private Vector3 _userInput;
    private PlayerControls _playerControl;

    private InputActionAsset _inputActions;
    private InputActionMap _actionMap;
    private PlayerInput _playerInput;


    private void Awake()
    {
        _attack = GetComponent<CharacterAttack>();
        _collection = GetComponent<CharacterCollection>();
        _movement = GetComponent<CharacterMovement>();
        _animation = GetComponent<CharacterAnimation>();
        _playerInput = GetComponent<PlayerInput>();
        _collection.SetUserId((int)_playerInput.user.id-1);
        _inputActions = _playerInput.actions;
        _actionMap = _inputActions.FindActionMap("Player");

    }
    private void OnEnable()
    {
        _actionMap.FindAction("Movement").performed += Movement;
        _actionMap.FindAction("Run").performed += ONRunning;
        _actionMap.FindAction("Run").canceled   += ONRuningCanceled;
        _actionMap.FindAction("CatchPlayer").performed += ONCatchPlayer; 
        _actionMap.Enable();
    }

    private void ONCatchPlayer(InputAction.CallbackContext context)
    {
        _attack.InitAttack();
    }

    private void ONRuningCanceled(InputAction.CallbackContext context)
    {
        Debug.Log("Running Canceled");
        _movement.OnCharacterRun(false);
    }

    private void ONRunning(InputAction.CallbackContext context)
    {
        Debug.Log("Running Started");
        _movement.OnCharacterRun(true);
    }

    public void Movement(InputAction.CallbackContext context)
    {
        var input = context.ReadValue<Vector2>();
        _userInput.x = input.x;
        _userInput.z = input.y;
        _userInput.Normalize();
        _movement.PlayerInput = _userInput;
        _animation.PlayRuning(_userInput.sqrMagnitude);
    }

}
