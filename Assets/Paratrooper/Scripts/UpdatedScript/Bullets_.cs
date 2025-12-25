using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets_ : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Boundary") || collision.gameObject.CompareTag("Enemy"))
        {
            ObjectPooler_.Instance.ReturnToPool("Bullet", gameObject);            
            Debug.Log("Bullet hit and returned to pool");
        }
    }
}
