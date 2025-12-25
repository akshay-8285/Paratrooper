using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Practice_2 : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Button spawnButton;
    void Start()
    {
        spawnButton.onClick.AddListener(SpawnBall);
    }

    public void SpawnBall()
    {
        float ranomDirection = Random.Range(-1f, 1f);
        Vector3 spawnPostion = new Vector3(ranomDirection, spawnPoint.position.y, spawnPoint.position.z);
        GameObject ball = Instantiate(ballPrefab, spawnPostion, Quaternion.identity);
        
    }
}
