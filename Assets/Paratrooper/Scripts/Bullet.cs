using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifetime = 5f;
    
    [Header("Collision Settings")]
    [SerializeField] private string parachuteTag = "Parachute";
    [SerializeField] private string soldierTag = "Soldier";
    
    private void OnEnable()
    {
        // Auto-destroy bullet after lifetime
        Invoke(nameof(ReturnToPool), lifetime);
    }
    
    private void OnDisable()
    {
        // Cancel any pending invokes
        CancelInvoke();
    }
    
    void Update()
    {
        // Move bullet upward
        transform.Translate(Vector3.up * speed * Time.deltaTime, Space.Self);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        HandleCollision(other);
    }
    
    private void HandleCollision(Collider2D other)
    {
        if (other.CompareTag(parachuteTag))
        {
            // Hit parachute - find parent paratrooper and call parachute hit
            Paratrooper paratrooper = other.GetComponentInParent<Paratrooper>();
            if (paratrooper != null)
            {
                paratrooper.OnParachuteHit();
            }
            
            Debug.Log("Bullet hit parachute!");
            ReturnToPool();
        }
        else if (other.CompareTag(soldierTag))
        {
            // Hit soldier - find parent paratrooper and call soldier hit
            Paratrooper paratrooper = other.GetComponentInParent<Paratrooper>();
            if (paratrooper != null)
            {
                paratrooper.OnSoldierHit();
            }
            
            Debug.Log("Bullet hit soldier!");
            ReturnToPool();
        }
    }
    
    private void ReturnToPool()
    {
        if (ObjectPool.Instance != null)
        {
            ObjectPool.Instance.ReturnToPool("Bullet", gameObject);
        }
        else
        {
            // Fallback: destroy if no pool available
            Destroy(gameObject);
        }
    }
}
