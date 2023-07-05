using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    // Start is called before the first frame update;
    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;
    [SerializeField] private GameObject player3;
    [SerializeField] private GameObject player4;

   // private bool _isCharacterActive = true;
    //private bool _canMove = true;
    private PlayerMovement player1Movement;
    private PlayerMovement player2Movement;
    private PlayerMovement player3Movement;
    private PlayerMovement player4Movement;

    //yukarı assagı sol sag
    void Start()
    {
        player1Movement = new PlayerMovement(player1, KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow);
        player2Movement = new PlayerMovement(player2, KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D);
        player3Movement = new PlayerMovement(player3, KeyCode.I, KeyCode.K, KeyCode.J, KeyCode.L);
        player4Movement = new PlayerMovement(player4, KeyCode.Keypad8, KeyCode.Keypad2, KeyCode.Keypad4, KeyCode.Keypad6);


    }

    void Update()
    {
        player1Movement.MovementKeys();
        player2Movement.MovementKeys();
        player3Movement.MovementKeys();
        player4Movement.MovementKeys();
    }


}
