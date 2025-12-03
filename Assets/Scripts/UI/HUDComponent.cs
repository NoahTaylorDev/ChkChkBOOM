
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDComponent : MonoBehaviour
{

    [SerializeField] private Image GunTypeShotgun;
    [SerializeField] private Player player;

    [SerializeField] private PlayerGun playerGun;

    [SerializeField] private TextMeshProUGUI AmmoCountText;
    [SerializeField] private Image AmmoTypeImage;

    public float ammoType;

    public float ammoCount;
    
    void Start()
    {
        AmmoCountText.text = $": {playerGun.ammoRemaining}";
    }

    void Update()
    {
        
    }

    public void UpdateAmmoCount()
    {
        AmmoCountText.text = $": {playerGun.ammoRemaining}";
    }

    public void UpdateAmmoTypeImage()
    {
        AmmoTypeImage.sprite = playerGun.gunData.AmmoType;
    }

    public void OnShotgunCollected()
    {
        GunTypeShotgun.color = new Color32(255, 255, 225, 255);
    }
}
