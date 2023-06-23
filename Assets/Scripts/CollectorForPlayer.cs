using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectorForPlayer : MonoBehaviour
{
    public int capacity = 5;
    public List<TrashData> trashObjects;
    // public GameObject trashPrefab; // You no longer need this.

    // A helper class to store both game object and its position.
    public class TrashData
    {
        public GameObject gameObject;
        public Vector3 position;

        public TrashData(GameObject gameObject, Vector3 position)
        {
            this.gameObject = gameObject;
            this.position = position;
        }
    }


    void Start()
    {
        trashObjects = new List<TrashData>();
    }

    void Update()
    {
        detector();

        if (Input.GetKeyDown(KeyCode.G) && trashObjects.Count > 0)
        {
            foreach (TrashData trash in trashObjects)
            {
                // Re-enable the object at its original position.
                trash.gameObject.transform.position = trash.position;
                trash.gameObject.SetActive(true);
            }

            trashObjects.Clear();
        }
    }

    private void detector()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 2f);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.tag == "Trash" && trashObjects.Count < capacity)
            {
                if (!trashObjects.Exists(t => t.gameObject == hitCollider.gameObject)) // modified check
                {
                    Debug.Log("Trash is in range");
                    trashObjects.Add(new TrashData(hitCollider.gameObject, hitCollider.gameObject.transform.position));
                    hitCollider.gameObject.SetActive(false); // disable instead of destroy
                }
            }
        }
    }

}
