using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pool
{
    public string tag;           // Unique tag like "Bullet", "Chopper", "Enemy"
    public GameObject prefab;    // Prefab to spawn
    public int size;             // Pool size
}

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;

    [SerializeField] private List<Pool> pools; // Add pools from Inspector
    private Dictionary<string, Queue<GameObject>> poolDictionary;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    /// <summary>
    /// Get object from pool. If pool is empty, create a new one dynamically.
    /// </summary>
    public GameObject GetPooledObject(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return null;
        }

        GameObject obj;
        if (poolDictionary[tag].Count > 0)
        {
            obj = poolDictionary[tag].Dequeue();
        }
        else
        {
            // Pool khatam ho gaya, naya object banado
            Pool poolConfig = pools.Find(p => p.tag == tag);
            if (poolConfig == null)
            {
                Debug.LogError("No pool config found for tag: " + tag);
                return null;
            }

            obj = Instantiate(poolConfig.prefab);
        }

        obj.SetActive(true);
        obj.transform.position = position;
        obj.transform.rotation = rotation;

        // Ye object jab ReturnToPool call hoga tab dubara queue me jayega
        return obj;
    }

    /// <summary>
    /// Safely return object to pool.
    /// </summary>
    public void ReturnToPool(string tag, GameObject obj)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            Destroy(obj); // agar galti se galat tag ka object return kare to destroy kar do
            return;
        }

        if (!poolDictionary[tag].Contains(obj)) // ✅ duplicate add hone se bacha liya
        {
            obj.SetActive(false);
            poolDictionary[tag].Enqueue(obj);
        }
    }
}
