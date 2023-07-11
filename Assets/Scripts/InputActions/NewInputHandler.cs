using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class NewInputHandler : MonoBehaviour
{
    [SerializeField] private PlayerControl _playerControl;
    public void OnMove(InputAction.CallbackContext context) { _playerControl.movementInput = context.ReadValue<Vector2>(); }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.action.triggered && !_playerControl.jumpTriggered)
        {
            _playerControl.jumped = true;
            _playerControl.jumpTriggered = true;
        }
    }
    public void OnCatch(InputAction.CallbackContext context)
    {
        if (_playerControl.isICaught)
            return; // Do not jump if the player is caught
        _playerControl.isPunching = context.action.triggered;
        if (context.action.triggered)
        {
            _playerControl.catchPlayer();
        }
    }
    public void OnRun(InputAction.CallbackContext context)
    {
        _playerControl.isRunning = context.action.triggered;
    }
}
