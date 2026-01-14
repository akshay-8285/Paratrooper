using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chopper_ : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

   
    void Update()
    {
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
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
