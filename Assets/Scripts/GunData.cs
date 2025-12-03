using UnityEngine;

[CreateAssetMenu(fileName = "GunData", menuName = "Scriptable Objects/GunData")]
public class GunData : ScriptableObject
{
    [Header("Shooting")]
    public float fireRate = 5f;
    public float bulletSpeed = 20f;
    public int damage = 10;
    public int bulletsPerShot = 1;
    public int bulletCount = 1;
    public float reloadSpeed = 0.1f;
    
    [Header("Ballistics")]
    [Tooltip("Maximum distance bullets travel before being destroyed")]
    public float shotDistance = 50f;
    [Tooltip("Spread angle in degrees (0 = perfect accuracy)")]
    public float bulletSpread = 0f;
    
    [Header("Visuals")]
    public Sprite gunSprite;
    public Sprite AmmoType;
    public float gunDistance = 1f;
    
    /// <summary>
    /// Calculates bullet lifetime based on distance and speed
    /// </summary>
    public float GetBulletLifetime()
    {
        return shotDistance / bulletSpeed;
    }
}