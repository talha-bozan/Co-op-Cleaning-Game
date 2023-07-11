using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBagController : MonoBehaviour
{
    
    public void MoveToBin(Vector3 endPosition)
    {
        StartCoroutine(MoveToBinRoutine(endPosition));
        StartCoroutine(RotateToBinRoutine());
    }

    IEnumerator MoveToBinRoutine(Vector3 targetPosition)
    {
        var counter = 0f;
        var startPosition = transform.localPosition;
        while (counter <= .5f)
        {
            var t = Easings.QuadEaseOut(counter, 0f, 1f, .5f);
            transform.localPosition = Vector3.Lerp(startPosition, targetPosition, t);
            counter += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = targetPosition;
    }

    IEnumerator RotateToBinRoutine()
    {
        var counter = 0f;
        var startRotation = transform.localEulerAngles;
        var targetRotation = Random.rotation.eulerAngles;
        while (counter <= .5f)
        {
            var t = Easings.QuadEaseOut(counter, 0f, 1f, .5f);
            transform.localEulerAngles = Vector3.Lerp(startRotation, targetRotation, t);
            counter += Time.deltaTime;
            yield return null;
        }

        transform.localEulerAngles = targetRotation;
    }
}
