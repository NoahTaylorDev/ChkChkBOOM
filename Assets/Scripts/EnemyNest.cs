using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class EnemyNest : MonoBehaviour
{
    public UnityEvent OnDestroyed;

    public float nestHealth = 20f;
    public float spawnRate = 10f;

    public GameObject enemyPrefab;

    public Transform spawnPoint;

    private float lastSpawnTime;
    void Start()
    {
        lastSpawnTime = Time.time;
    }

    
    void Update()
    {
        if(Time.time >= lastSpawnTime + spawnRate)
        {
            lastSpawnTime = Time.time;
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
    }

    public void TakeDamage(int damage)
    {
        nestHealth -= damage;
        if (nestHealth < 0)
        {
            Die();
        }
    }

    public void Die()
    {
        OnDestroyed?.Invoke();
        Destroy(gameObject);
    }
}
