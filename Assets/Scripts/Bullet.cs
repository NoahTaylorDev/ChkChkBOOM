using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 direction;
    private float speed;
    private int damage;
    private Rigidbody2D rb;
    private bool hasHit = false;

    private float lifetime = 2f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Debug.Log($"Bullet created: {gameObject.GetInstanceID()}");
        Destroy(gameObject, lifetime);
    }

    public void FireBullet(
        Vector3 shootDirection,
        float bulletSpeed,
        int bulletDamage
    )
    {
        direction = shootDirection;
        speed = bulletSpeed;
        damage = bulletDamage;
        rb.linearVelocity = direction * speed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (hasHit) return;

        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                hasHit = true;
                Debug.Log($"Bullet {gameObject.GetInstanceID()} hit enemy");
                enemy.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
        else if (other.CompareTag("EnemyNest"))
        {
            EnemyNest nest = other.GetComponent<EnemyNest>();
            if (nest != null)
            {
                hasHit = true;
                Debug.Log($"Bullet {gameObject.GetInstanceID()} hit nest");
                nest.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }

    void OnDestroy()
    {
        Debug.Log($"Bullet {gameObject.GetInstanceID()} destroyed");
    }
}