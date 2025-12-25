using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chopper_ : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float SpawnTime = 2f;  
    [SerializeField] private float moveSpeed = 5f;

    void Start()
    {
        StartCoroutine(spawnChopper());
    }
    void Update()
    {
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
    }

    private IEnumerator spawnChopper()
    {
        while(true)
        {
            if(spawnPoint == null)
            {
                Debug.LogWarning("Spawn point is not assigned.");
                
            }
            else
            {
                ObjectPooler_.Instance.GetPoolObject("Chopper", spawnPoint.position, spawnPoint.rotation);
            }
            
            
            yield return new WaitForSeconds(SpawnTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Bullet"))
        {
            ObjectPooler_.Instance.ReturnToPool("Chopper", gameObject);
            AudioManager_.Instance.PlayExplosionSound();
            GameManager_.Instance.OnEnemyDestroyed(5);
            Debug.Log("Chopper hit boundary and returned to pool" + gameObject.name);

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Boundary")|| collision.gameObject.CompareTag("Bullet"))
        {
            ObjectPooler_.Instance.ReturnToPool("Chopper", gameObject);
            Debug.Log("Chopper hit boundary and returned to pool" + gameObject.name);

        }
    }
}
