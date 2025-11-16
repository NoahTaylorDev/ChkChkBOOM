using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    [SerializeField] private GunData gunData;
    [SerializeField] private Bullet bulletPrefab;
    public Transform firePoint;

    private SpriteRenderer spriteRenderer;
    
    private float lastFired = 0;
    public float gunDistance => gunData.gunDistance;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null && gunData.gunSprite)
        {
            spriteRenderer.sprite = gunData.gunSprite;
        }
    }

    public void AimAt(Vector2 targetPosition)
    {
        Vector2 direction = targetPosition - (Vector2)transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void TryShoot()
    {
        if (Time.time >= lastFired + 1f / gunData.fireRate)
        {
            Vector3 shootDirection = firePoint.right;
            Bullet bulletInstance = Instantiate(
                bulletPrefab,
                firePoint.position,
                transform.rotation
            );

            bulletInstance.FireBullet(
                shootDirection,
                gunData.bulletSpeed,
                gunData.damage
            );
            lastFired = Time.time;
        }
    }
}