using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharSliderPosition : MonoBehaviour
{
    [SerializeField] private GameObject slider;
    [SerializeField] private Transform player;
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        slider.transform.position = new Vector3(player.position.x, slider.transform.position.y, player.position.z);    
    }
}
