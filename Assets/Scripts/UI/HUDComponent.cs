
using UnityEngine;
using UnityEngine.UI;

public class HUDComponent : MonoBehaviour
{

    [SerializeField] private Image GunTypeShotgun;
    [SerializeField] private Player player;
    
    void Start()
    {
        player.OnShotgunCollected.AddListener(() => OnShotgunCollected());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnShotgunCollected()
    {
        GunTypeShotgun.color = new Color32(255, 255, 225, 255);
    }
}
