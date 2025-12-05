using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rigidBody2D;
    private Transform playerTransform;

    public float enemyHealth = 10f;
    public float enemyDamage = 2f;
    public float enemyAttackCooldown = 0.5f;
    public float moveSpeed = 3f;
    public float fleeTime = 2f;
    public float fleeSpeed = 6f;

    private float nextAttackTime;
    private float fleeEndTime;
    private bool isFleeing = false;

    private Player player;

    private PlayerHUD playerUI;

    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        playerUI = FindFirstObjectByType<PlayerHUD>();
        nextAttackTime = Time.time;
        player = FindFirstObjectByType<Player>();
        if (player != null)
        {
            playerTransform = player.transform;
        }
    }

    void Update()
    {
        if (playerTransform == null) return;

        if (isFleeing)
        {
            if (Time.time >= fleeEndTime)
            {
                isFleeing = false;
            }
            else
            {
                FleeFromPlayer();
            }
        }
        else
        {
            ChasePlayer();
        }
    }

    private void ChasePlayer()
    {
        Vector2 direction =
            (playerTransform.position - transform.position).normalized;
        rigidBody2D.linearVelocity = direction * moveSpeed;
    }

    private void FleeFromPlayer()
    {
        Vector2 direction =
            (transform.position - playerTransform.position).normalized;
        rigidBody2D.linearVelocity = direction * fleeSpeed;
    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        if (nextAttackTime < Time.time)
        {
            if (other.TryGetComponent<Player>(out Player player))
            {
                player.TakeDamage(enemyDamage);
                nextAttackTime = Time.time + enemyAttackCooldown;
                isFleeing = true;
                fleeEndTime = Time.time + fleeTime;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        enemyHealth -= damage;
        if (enemyHealth < 0)
        {
            Die();
        }
    }

    public void Die()
    {
        playerUI.globalData.AddBoombucks(10);
        playerUI.UpdateBoombucksDisplay();
        Destroy(gameObject);
    }
}