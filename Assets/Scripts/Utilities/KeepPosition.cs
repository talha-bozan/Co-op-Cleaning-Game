using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepPosition : MonoBehaviour
{
    private Vector3 _startPosition;
    private void Awake()
    {
        _startPosition = transform.localPosition;
    }

    private void LateUpdate()
    {
        transform.localPosition = _startPosition;
    }
}
