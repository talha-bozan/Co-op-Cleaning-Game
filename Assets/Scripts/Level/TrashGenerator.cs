using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] trashPrefabs;
    private Transform _startPoint;

    //variables for Object Pooling
    private List<GameObject> _trashPool;
    private int _trashPoolCount=20;
    

    //Variables for grid positions
    [SerializeField] private Vector2 gridSize;
    [SerializeField] private float gridPointOffset;
    [SerializeField] private float randomPositionOffset;
    private List<Vector3> _gridPositions;

    private ParticleSystem _spawnParticle;
    private Vector3 _particleScale = new Vector3(1f,.2f,1f);


    void Start()
    {
        
        _startPoint = transform.GetChild(0);
        _spawnParticle = _startPoint.GetChild(0).GetComponent<ParticleSystem>();
        
        GeneratePool();
    }


    //for optimization, generate a pool for object pooling, add same certain amount from every item to the pool and then shuffle
    private void GeneratePool()
    {
        _trashPool = new List<GameObject>();
        for (int i = 0; i < trashPrefabs.Length; i++)
        {
            for (int j = 0; j < _trashPoolCount; j++)
            {
                var trash = Instantiate(trashPrefabs[i], transform);
                trash.SetActive(false);
                _trashPool.Add(trash);
            }
        }

        ShuffleTrashItems();
    }



    //Fisher-yates shuffle
    private void ShuffleTrashItems()
    {
        for (int i = 0; i < _trashPool.Count; i++)
        {
            var rnd = Random.Range(i, _trashPool.Count);
            var temp = _trashPool[i];
            _trashPool[i] = _trashPool[rnd];
            _trashPool[rnd] = temp;
        }

        GenerateGrid();
    }


    //Generate grid points
    private void GenerateGrid()
    {
        _gridPositions = new List<Vector3>();
        var localStart = _startPoint.localPosition;
        for (int i = 0; i < gridSize.x; i++)
        {
            for (int j = 0; j < gridSize.y; j++)
            {
                var gp = new Vector3(localStart.x - (i * gridPointOffset), localStart.y, localStart.z + (j * gridPointOffset));
                var randomOffset = Random.insideUnitSphere * randomPositionOffset;
                randomOffset.y = 0f;
                _gridPositions.Add(gp+randomOffset);
            }
        }

        ShuffleGridPositions();
    }


    private void ShuffleGridPositions()
    {
        for (int i = 0; i < _gridPositions.Count; i++)
        {
            var rnd = Random.Range(i, _gridPositions.Count);
            var temp = _gridPositions[i];
            _gridPositions[i] = _gridPositions[rnd];
            _gridPositions[rnd] = temp;
        }

        PlaceObjects();
    }


    private void PlaceObjects()
    {
        StartCoroutine(TrashSpawnRoutine());
    }


    IEnumerator TrashSpawnRoutine()
    {
        yield return new WaitForSeconds(1f);
        _spawnParticle.Play(true);
        LeanTween.scale(_spawnParticle.gameObject, _particleScale, .5f).setEase(LeanTweenType.easeOutQuad);
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < _gridPositions.Count; i++)
        {
            _trashPool[i].transform.localPosition = _gridPositions[i];
            _trashPool[i].SetActive(true);
            yield return null;
        }

        yield return new WaitForSeconds(.25f);
        LeanTween.scale(_spawnParticle.gameObject, Vector3.zero, .5f).setEase(LeanTweenType.easeOutQuad);
        yield return new WaitForSeconds(.55f);
        _spawnParticle.Stop(true);


    }


}
