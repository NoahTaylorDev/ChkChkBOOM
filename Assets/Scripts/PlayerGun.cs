using Mono.Cecil;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    
    [SerializeField] public int gunIndex = 0;
    public GunData gunData => availableGuns[gunIndex];
    [SerializeField] GunData[] availableGuns;
    [SerializeField] private Bullet bulletPrefab;
    public Transform firePoint;
    private SpriteRenderer spriteRenderer;
    private float lastFired = 0;
    private PlayerHUD HUD;

    public int ammoRemaining;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        HUD = FindFirstObjectByType<PlayerHUD>();
        UpdateGunVisuals();
        ammoRemaining = gunData.bulletCount;
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
            ammoRemaining -= 1;
            HUD.UpdateAmmoDisplay();
            for (int i = 0; i < gunData.bulletsPerShot; i++)
            {
                FireSingleBullet();
            }
            lastFired = Time.time;
        }
    }

    private void FireSingleBullet()
    {
        Vector3 baseDirection = firePoint.right;
        
        float spreadAngle = Random.Range(
            -gunData.bulletSpread / 2f,
            gunData.bulletSpread / 2f
        );
        Quaternion spreadRotation = Quaternion.Euler(0, 0, spreadAngle);
        Vector3 shootDirection = spreadRotation * baseDirection;
       
        Bullet bulletInstance = Instantiate(
            bulletPrefab,
            firePoint.position,
            Quaternion.LookRotation(Vector3.forward, shootDirection) 
                * Quaternion.Euler(0, 0, 90)
        );

        bulletInstance.FireBullet(
            shootDirection,
            gunData.bulletSpeed,
            gunData.damage,
            gunData.GetBulletLifetime()
        );
    }

    public void SwapGun(int index)
    {
        if (index >= 0 && index < availableGuns.Length)
        {
            gunIndex = index;
            UpdateGunVisuals();

            
        Debug.Log("HUD is null: " + HUD == null);
            HUD.UpdateAmmoDisplay();
            ammoRemaining = gunData.bulletCount;
            
            
        }
    }

    private void UpdateGunVisuals()
    {
        if (spriteRenderer != null && gunData.gunSprite)
        {
            spriteRenderer.sprite = gunData.gunSprite;
        }
    }
}