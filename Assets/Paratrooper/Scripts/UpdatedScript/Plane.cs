using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
   [SerializeField] private Transform spawnPoint;
   [SerializeField] private float SpawnTime = 3f;
   [Range(1f, 50f)]
   [SerializeField] private float moveSpeed;
    void Start()
    {
        StartCoroutine(spawnPlane());
    }

    void Update()
    {
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
    }

    private IEnumerator spawnPlane()
    {
        while(true)
        {
            if(spawnPoint == null)
            {
                Debug.LogWarning("Spawn point is not assigned.");
                
            }
            else
            {
                ObjectPooler_.Instance.GetPoolObject("Plane", spawnPoint.position, spawnPoint.rotation);
            }
            
            
            yield return new WaitForSeconds(SpawnTime);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Bullet"))
        {
            ObjectPooler_.Instance.ReturnToPool("Plane", gameObject);
            AudioManager_.Instance.PlayExplosionSound();
            GameManager_.Instance.OnEnemyDestroyed(5);
            Debug.Log("Plane hit boundary and returned to pool" + gameObject.name);

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Boundary") || collision.gameObject.CompareTag("Bullet"))
        {
            ObjectPooler_.Instance.ReturnToPool("Plane", gameObject);
            Debug.Log("Plane hit boundary and returned to pool" + gameObject.name);

        }
    }
}
