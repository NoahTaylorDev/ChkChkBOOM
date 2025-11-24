using UnityEngine;

public class ChestComponent : MonoBehaviour
{

    [SerializeField]
    private Sprite disabledChestSprite;
    [SerializeField]
    private Sprite activeChestSprite;


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
}
