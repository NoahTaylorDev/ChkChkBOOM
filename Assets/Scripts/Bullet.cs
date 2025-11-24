using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 direction;
    private float speed;
    private int damage;
    private Rigidbody2D rb;
    private bool hasHit = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void FireBullet(
        Vector3 shootDirection,
        float bulletSpeed,
        int bulletDamage,
        float lifetime
    )
    {
        direction = shootDirection;
        speed = bulletSpeed;
        damage = bulletDamage;
        rb.linearVelocity = direction * speed;
        
        Destroy(gameObject, lifetime);
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
                nest.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }

    void OnDestroy()
    {
        
    }
}