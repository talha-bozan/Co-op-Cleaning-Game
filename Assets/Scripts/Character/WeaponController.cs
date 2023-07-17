using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private float hitRadius;
    [SerializeField] private LayerMask characterLayer;
    private Transform _rayPoint;

    private void Start()
    {
        _rayPoint = transform.GetChild(0);
    }

    public void Attack()
    {
        var hits = new Collider[1];
        var hitCount = Physics.OverlapSphereNonAlloc(_rayPoint.position, hitRadius, hits, characterLayer);
        if (hitCount < 1) return;

        if(hits[0].TryGetComponent<CharacterMovement>(out CharacterMovement movement))
        {
            movement.ONCharacterGetHit();
        }
        
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(_rayPoint.position, hitRadius);
    }


}
