using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

public class RoomController : MonoBehaviour
{
    public UnityEvent OnRoomCleared;
    [SerializeField] private Enemy[] enemies;
    private List<EnemyNest> nests = new List<EnemyNest>();
    [SerializeField] private EnemyNest nest;

    [SerializeField] private GameObject[] spawnPoints;

    private int remainingEnemies;
    private int remainingNests;

    void Start()
    {
        int randomIndex = Random.Range(0, spawnPoints.Length);
        EnemyNest spawnedNest = Instantiate(
            nest, 
            spawnPoints[randomIndex].transform.position, 
            Quaternion.identity
        );
        nests.Add(spawnedNest);
    }

    public void OnSpawnerDestroyed()
    {
        if(nests.Count == 0)
        {
            Debug.Log("Room Cleared");
            OnRoomCleared?.Invoke();
        }
    }
}

