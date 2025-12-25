using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shoot_ : MonoBehaviour
{
    [SerializeField] private float minRotateAngle = -45f;
    [SerializeField] private float maxRoatateAngle = 45f;
    [SerializeField] private float roatateSpeed = 10f;
    [SerializeField] private float shootForce = 10f;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate = 0.5f;
    [SerializeField] private TMP_Text ammoText , decreseAmmoText;
    [SerializeField] private GameObject muzzleFlash;
    private float rotate = 0f;
    private int currentAmmo;
    private bool canShoot = true;


    private void Start()
    {
        currentAmmo = 10;
        ammoText.text = currentAmmo.ToString();
        muzzleFlash.SetActive(false);
    }
    private void Update()
    {
        RotateGun();
        if(Input.GetKeyDown(KeyCode.Space) && canShoot)
        {
            StartCoroutine(fireSequence());
            
        }
        
    }
    public IEnumerator fireSequence()
    {
       
        canShoot = false;
        GameObject bullet = ObjectPooler_.Instance.GetPoolObject("Bullet", firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if(rb != null)
        {
            rb.AddForce(firePoint.up * shootForce, ForceMode2D.Impulse);
            GameManager_.Instance.OnBulletFired();
            AudioManager_.Instance.PlayShootSound();
            StartCoroutine(muzzleFlashEffect());
            Debug.Log(rb.velocity.magnitude);
        }
        yield return new WaitForSeconds(fireRate);
        canShoot = true;  
        
    }
    private void RotateGun()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        rotate -= horizontalInput * roatateSpeed * Time.deltaTime;
        rotate = Mathf.Clamp(rotate, minRotateAngle, maxRoatateAngle);
        transform.rotation = Quaternion.Euler(0, 0, rotate);

    }

    private IEnumerator muzzleFlashEffect()
    {
        muzzleFlash.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        muzzleFlash.SetActive(false);
    }
    
    
    
}
