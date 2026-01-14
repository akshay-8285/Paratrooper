using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectPooler_ : MonoBehaviour
{

    public static ObjectPooler_ Instance;

    public List<Pool_Data> poolDataList;
    //private Queue<GameObject> pool;
    private Dictionary<string, Queue<GameObject>> poolDictionary;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        SetPoolData();
    }
    public void SetPoolData()
    {
        //pool = new Queue<GameObject>();
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach (Pool_Data pool in poolDataList)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < pool.poolSize; i++)
            {
                GameObject obj = Instantiate(pool.prefab, transform);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            poolDictionary.Add(pool.poolTag, objectPool);
        }
    }



    public GameObject GetPoolObject(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return null;
        }

        GameObject obj = poolDictionary[tag].Dequeue();
        obj.SetActive(true);
        obj.transform.position = position;
        obj.transform.rotation = rotation;
        //pool.Enqueue(obj);
        return obj;
    }


    public void ReturnToPool(string tag, GameObject obj)
    {
        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }

        obj.SetActive(false);
        poolDictionary[tag].Enqueue(obj);
    }

}
