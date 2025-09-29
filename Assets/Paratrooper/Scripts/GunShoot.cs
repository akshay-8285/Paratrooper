using UnityEngine;

public class GunShoot : MonoBehaviour
{
    [SerializeField] private Transform gunPoint;       // jaha se bullet niklegi
    [SerializeField] private float rotateSpeed = 10f;
    [SerializeField] private float minZrotate = -80f;
    [SerializeField] private float maxZrotate = 80f;

    private float zRotate = 0f;

    void Update()
    {
        // Rotation
        float horizontal = Input.GetAxis("Horizontal");
        zRotate -= horizontal * rotateSpeed * Time.deltaTime;
        zRotate = Mathf.Clamp(zRotate, minZrotate, maxZrotate);
        transform.rotation = Quaternion.Euler(0, 0, zRotate);

        // Shooting
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ObjectPool.Instance.GetPooledObject("Bullet", gunPoint.position, gunPoint.rotation);
            Debug.Log("Bullet Fired!");
        }
    }
}
