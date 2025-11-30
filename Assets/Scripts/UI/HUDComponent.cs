
using UnityEngine;
using UnityEngine.UI;

public class HUDComponent : MonoBehaviour
{

    [SerializeField] private Image GunTypeShotgun;
    [SerializeField] private Player player;
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnShotgunCollected()
    {
        Debug.Log("heloo" + GunTypeShotgun);
        GunTypeShotgun.color = new Color32(255, 255, 225, 255);
    }
}
