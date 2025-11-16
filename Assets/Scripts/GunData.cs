using UnityEngine;

[CreateAssetMenu(fileName = "GunData", menuName = "Scriptable Objects/GunData")]
public class GunData : ScriptableObject
{
    [Header("Shooting")]
    public float fireRate = 5f;
    public float bulletSpeed = 20f;
    public int damage = 10;
    
    [Header("Visuals")]
    public Sprite gunSprite;
    public float gunDistance = 1f;
}
