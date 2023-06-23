using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private GameObject _player;

    private KeyCode _up;
    private KeyCode _down;
    private KeyCode _left;
    private KeyCode _right;

    private bool _canMove = true;
    private float _speed = 5f;

    public PlayerMovement(GameObject player, KeyCode up, KeyCode down, KeyCode left, KeyCode right, bool canMove = true)
    {
        this._canMove = canMove;
        this._up = up;
        this._down = down;
        this._left = left;
        this._right = right;
        this._player = player;
    }


    public void MovementKeys()
    {
        if (!_canMove) return;

        if (Input.GetKey(_up))
        {
            _player.GetComponent<CharacterController>().Move(Vector3.forward * Time.deltaTime * _speed);
        }
        else if (Input.GetKey(_down))
        {
            _player.GetComponent<CharacterController>().Move(Vector3.back * Time.deltaTime * _speed);
        }
        else if (Input.GetKey(_left))
        {
            _player.GetComponent<CharacterController>().Move(Vector3.left * Time.deltaTime * _speed);
        }
        else if (Input.GetKey(_right))
        {
            _player.GetComponent<CharacterController>().Move(Vector3.right * Time.deltaTime * _speed);
        }
    }


    public void SetMovementEnabled(bool canMove)
    {
        _canMove = canMove;
    }
}
