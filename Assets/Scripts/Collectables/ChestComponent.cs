using UnityEngine;

public class ChestComponent : MonoBehaviour
{

    [SerializeField]
    private Sprite disabledChestSprite;
    [SerializeField]
    private Sprite activeChestSprite;

    [SerializeField]
    private Sprite openChestSprite;
    [SerializeField]
    private RoomController roomController;

    [SerializeField]
    private Collectable shotgunPrefab;

    private SpriteRenderer spriteRenderer;

    private Rigidbody2D rigidBody2D;

    private ExitComponent exit;

    private PlayerUIController playerUI;

    private bool chestOpened = false;
    private bool chestActive = false; 

    void Awake()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        exit = FindFirstObjectByType<ExitComponent>();
        playerUI = FindAnyObjectByType<PlayerUIController>();
    }
    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if(!chestOpened && chestActive)
        {
            if (collision.gameObject.CompareTag("Player"))
                {
                    chestOpened = true;
                    exit.Activate();
                    spriteRenderer.sprite = openChestSprite;
                    Vector3 newPosition = rigidBody2D.position;
                    newPosition.y += 2;
                    Collectable shotgun = Instantiate(shotgunPrefab, newPosition, Quaternion.identity);
                    shotgun.variantType = CollectableType.Shotgun;
                    playerUI.globalData.AddBoombucks(100);
                }
        }
        
    }

    public void ActivateChest()
    {
        chestActive = true;
        spriteRenderer.sprite = activeChestSprite;

        
    }
}
