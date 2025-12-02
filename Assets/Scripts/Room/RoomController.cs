using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class RoomController : MonoBehaviour
{
    [SerializeField] private Enemy[] enemies;
    private List<EnemyNest> nests = new List<EnemyNest>();
    [SerializeField] private EnemyNest nest;

    private ChestComponent chest;

    [SerializeField] private GameObject[] spawnPoints;

    private int remainingEnemies;
    private int remainingNests;

    void Start()
    {
        chest = FindFirstObjectByType<ChestComponent>();
        int randomIndex = Random.Range(0, spawnPoints.Length);
        EnemyNest spawnedNest = Instantiate(
            nest, 
            spawnPoints[randomIndex].transform.position, 
            Quaternion.identity
        );
        nests.Add(spawnedNest);
    }

    public void OnSpawnerDestroyed(EnemyNest nest)
    {
        nests.Remove(nest);
        if(nests.Count == 0)
        {
            chest.ActivateChest();
        }
    }
}

