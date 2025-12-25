  using System.Collections;
using UnityEngine;

public class Chopper : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private Transform dropPoint;   // jaha se paratrooper girega
    [SerializeField] private float minX = -8f;      // screen ke left boundary
    [SerializeField] private float maxX = 8f;       // screen ke right
    [SerializeField] private float dropDelay = 2f;
    [SerializeField] private float rotate;

    void Start()
    {
        StartCoroutine(SpawnParatrooper());
    }

    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
        // Vector3 pos = transform.position;
        // pos.x = Mathf.Clamp(pos.x, minX, maxX);
        // transform.position = pos;
    }

    private IEnumerator SpawnParatrooper()
    {
        while (true)
        {
            yield return new WaitForSeconds(dropDelay);

            ObjectPool.Instance.GetPooledObject("Paratrooper", dropPoint.position, Quaternion.Euler(0, 0, 0));
            Debug.Log("Paratrooper Dropped!"+ gameObject.name);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            // Chopper hit by bullet
            Debug.Log("Chopper Hit!");
            ObjectPool.Instance.ReturnToPool("Chopper", this.gameObject);
            ObjectPool.Instance.ReturnToPool("Bullet", collision.gameObject);
            // Optionally, you can add more effects or score increment here
        }
    }
}
