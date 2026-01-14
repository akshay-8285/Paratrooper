using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform chopperSpawnPoint;
    [SerializeField] private Transform planeSpawnPoint;
    [SerializeField] private float planeSpawnTime = 3f;
    [SerializeField] private float chopperSpawnTime = 2f;
    void Start()
    {
        StartCoroutine(SpawnPlane());
        StartCoroutine(SpawnChopper());
    }

    private IEnumerator SpawnPlane()
    {
        while(true)
        {
            
            ObjectPooler_.Instance.GetPoolObject("Plane", planeSpawnPoint.position, planeSpawnPoint.rotation);

            yield return new WaitForSeconds(planeSpawnTime);
        }

    }
    private IEnumerator SpawnChopper()
    {
        while(true)
        {
            
            ObjectPooler_.Instance.GetPoolObject("Chopper", chopperSpawnPoint.position, chopperSpawnPoint.rotation);
            yield return new WaitForSeconds(chopperSpawnTime);
        }
    }

   
}
