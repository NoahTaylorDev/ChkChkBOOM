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

    private bool chestOpened = false;
    private bool chestActive = false; 

    void Awake()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

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
                    spriteRenderer.sprite = openChestSprite;
                    Collectable shotgun = Instantiate(shotgunPrefab, rigidBody2D.position, Quaternion.identity);
                    shotgun.variantType = CollectableType.Shotgun;
                }
        }
        
    }

    public void ActivateChest()
    {
        chestActive = true;
        Debug.Log("Chest Active");
        spriteRenderer.sprite = activeChestSprite;
    }
}
