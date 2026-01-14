using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Para_Manager : MonoBehaviour
{
    public static Para_Manager Instance;

    [SerializeField] float spawnInterval = 2f;
    private List<Transform> dropPoints = new List<Transform>();

    private void Awake()
    {
        Instance = this;
        
    }

    public void AddDropPoint(Transform sp)
    {
        if (!dropPoints.Contains(sp))
            dropPoints.Add(sp);
        Debug.Log("Drop point count: " + dropPoints.Count);
    }

    Transform GetRandomDropPoint()
    {
        if (dropPoints.Count == 0)
            return null;

        return dropPoints[Random.Range(0, dropPoints.Count)];
    }

    private void Start()
    {
        StartCoroutine(SpawnParatrooper());
        
    }

    private IEnumerator SpawnParatrooper()
    {
        while (true)
        {
            Transform sp = GetRandomDropPoint();

            if (sp == null)
            {
                yield return null;
                continue;
            }

            Vector3 vp = Camera.main.WorldToViewportPoint(sp.position);
            bool insideX = vp.x > 0f && vp.x < 1f;

            if (insideX)
            {
                GameObject paraObj = ObjectPooler_.Instance.GetPoolObject(
                    "Paratrooper",
                    sp.position,
                    sp.rotation
                );

                Collider2D mycollider = sp.GetComponentInParent<Collider2D>();
                Paratrooper_ para = paraObj.GetComponent<Paratrooper_>();
                para.StartCoroutine(para.OpenParachute());
                
                
                para.IgnoreCollision(mycollider);
                //para.EnableCollider();
                


                Debug.Log("Paratrooper spawned at: " + sp.position);
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    
}
