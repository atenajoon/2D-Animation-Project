using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdSpawner : MonoBehaviour
{
        public Transform _birdSpawnPoint;
    public GameObject _birdPrefab;
    // Start is called before the first frame update
    void Start()
    {
                // Instantiate(whatToSpawn, whereToSpawn, adjustedRotation)
        Instantiate(_birdPrefab, _birdSpawnPoint.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
