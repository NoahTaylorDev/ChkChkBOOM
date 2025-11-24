using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

public class RoomController : MonoBehaviour
{
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
        
        // spawnedNest.OnDestroyed.AddListener(() => OnSpawnerDestroyed());
    }

    public void OnSpawnerDestroyed()
    {
        Debug.Log("Nest Count: " + nests.Count);
    }
}

