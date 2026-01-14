using System.Collections;
using UnityEditor.Callbacks;
using UnityEngine;

public class Paratrooper_ : MonoBehaviour
{
    private Collider2D myCollider;
    [SerializeField] private GameObject parachutePrefab;
    [SerializeField] private float parachuteDelay = 0.5f;
    [SerializeField] private float parachuteGravity = 2f;
    private Rigidbody2D rb;
    public float normalParachuteGravity;
    private bool parachuteOpen;

    private void Awake()
    {
        myCollider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        normalParachuteGravity = rb.gravityScale;
        if (parachutePrefab != null)
        {
            parachutePrefab.SetActive(false);
        }
    }
    // void FixedUpdate()
    // {
    //     float maxFallSpeed = parachuteOpen ? -2f : -10f;

    //     if (rb.velocity.y < maxFallSpeed)
    //         rb.velocity = new Vector2(rb.velocity.x, maxFallSpeed);
    // }


    public void IgnoreCollision(Collider2D other)
    {

        if (other != null && myCollider != null)
        {
            Physics2D.IgnoreCollision(myCollider, other, true);
        }
    }
    public void EnableCollider()
    {
        if (myCollider != null)
        {
            myCollider.enabled = true;
        }
    }
    public IEnumerator OpenParachute()
    {
        yield return new WaitForSeconds(parachuteDelay);
        if (parachutePrefab != null && !parachuteOpen)
        {
            parachuteOpen = true;
            parachutePrefab.SetActive(true);
            rb.gravityScale = parachuteGravity;
            rb.velocity = new Vector2(rb.velocity.x, 0f);


        }
    }
    public void CloseParachute()
    {
        if(parachutePrefab != null && parachuteOpen)
        {
            parachuteOpen = false;
            parachutePrefab.SetActive(false);
            rb.gravityScale = normalParachuteGravity;
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            CloseParachute();
        }
    }


}
