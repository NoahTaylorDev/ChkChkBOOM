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

    private SpriteRenderer spriteRenderer;

    private bool chestOpened = false; 

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        roomController.OnRoomCleared.AddListener(() => ActivateChest());

    }
    void Update()
    {
        
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if(!chestOpened)
        {
            if (collision.gameObject.CompareTag("Player"))
                {
                    chestOpened = true;
                    spriteRenderer.sprite = openChestSprite;
                }
        }
        
    }

    public void ActivateChest()
    {
        
        Debug.Log("Chest Active");
        spriteRenderer.sprite = activeChestSprite;
    }
}
