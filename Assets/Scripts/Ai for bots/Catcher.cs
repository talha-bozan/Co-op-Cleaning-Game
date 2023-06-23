using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Catcher : MonoBehaviour
{
    [SerializeField] private Transform player;
    private NavMeshAgent nMesh;
    // Start is called before the first frame update
    void Start()
    {
        nMesh = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        nMesh.destination = player.position;
    }


}
