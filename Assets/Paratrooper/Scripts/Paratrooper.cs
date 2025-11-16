using UnityEngine;
using System.Collections;

public class Paratrooper : MonoBehaviour
{
    [Header("Paratrooper Components")]
    [SerializeField] private GameObject parachute;
    [SerializeField] private GameObject soldier;
    
    [Header("Movement Settings")]
    [SerializeField] private float normalFallSpeed = 2f;
    [SerializeField] private float parachuteFallSpeed = 1f;
    [SerializeField] private float fastFallSpeed = 4f;
    [SerializeField] private float parachuteDelay = 2f;
    
    [Header("Ground Detection")]
    [SerializeField] private float groundY = -5f;
    
    private bool parachuteActive = false;
    private bool parachuteDestroyed = false;
    private bool hasLanded = false;
    private Coroutine parachuteCoroutine;

    void OnEnable()
    {
        // Reset state
        parachuteActive = false;
        parachuteDestroyed = false;
        hasLanded = false;

        
        transform.rotation = Quaternion.identity;

        // Start parachute timing
        if (parachuteCoroutine != null)
            StopCoroutine(parachuteCoroutine);
        parachuteCoroutine = StartCoroutine(ActivateParachuteAfterDelay());
    }

    void OnDisable()
    {
        
        if (parachuteCoroutine != null)
        {
            StopCoroutine(parachuteCoroutine);
            parachuteCoroutine = null;
        }
    }

    void Update()
    {
        if (hasLanded) return;

        // Check for ground collision
        if (transform.position.y <= groundY)
        {
            Land();
            return;
        }

        // Move downward (always world down)
        float currentSpeed = GetCurrentFallSpeed();
        transform.Translate(Vector3.down * currentSpeed * Time.deltaTime, Space.World);
    }

    private IEnumerator ActivateParachuteAfterDelay()
    {
        yield return new WaitForSeconds(parachuteDelay);
        
        if (!parachuteDestroyed && parachute != null)
        {
            parachute.SetActive(true);
            parachuteActive = true;
        }
    }

    private float GetCurrentFallSpeed()
    {
        if (parachuteDestroyed)
            return fastFallSpeed;
        else if (parachuteActive)
            return parachuteFallSpeed;
        else
            return normalFallSpeed;
    }

    private void Land()
    {
        if (hasLanded) return;
        
        hasLanded = true;
        
        // Position exactly on ground
        Vector3 pos = transform.position;
        pos.y = groundY;
        transform.position = pos;
        
        // Deactivate parachute
        if (parachute != null)
            parachute.SetActive(false);
            
        Debug.Log("Paratrooper landed!");
        
        // Return to pool after landing
        Invoke(nameof(ReturnToPool), 2f);
    }

    public void OnParachuteHit()
    {
        if (parachuteDestroyed) return;
        
        parachuteDestroyed = true;
        parachuteActive = false;
        
        if (parachute != null)
            parachute.SetActive(false);
            
        Debug.Log("Parachute destroyed - falling faster!");
    }

    public void OnSoldierHit()
    {
        Debug.Log("Soldier hit!");
        ReturnToPool();
    }

    private void ReturnToPool()
    {
        if (ObjectPool.Instance != null)
        {
            ObjectPool.Instance.ReturnToPool("Paratrooper", gameObject);
        }
    }
}
