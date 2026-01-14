using UnityEngine;

public class SpawnPara_ : MonoBehaviour
{
    public Transform spawnPoint;

    private void Start()
    {
        Para_Manager.Instance.AddDropPoint(spawnPoint);
    }
}
