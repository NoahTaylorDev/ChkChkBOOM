using UnityEngine;

public class ChestComponent : MonoBehaviour
{

    [SerializeField]
    private Sprite disabledChestSprite;
    [SerializeField]
    private Sprite activeChestSprite;
    [SerializeField]
    private RoomController roomController;

    private SpriteRenderer spriteRenderer;

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
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    public void ActivateChest()
    {
        Debug.Log("Chest Active");
        spriteRenderer.sprite = activeChestSprite;
    }
}
