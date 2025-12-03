using UnityEngine;
[RequireComponent(typeof(ObjectData))]
public class DestructableObjectComponent : MonoBehaviour
{
    
    [SerializeField] private float maxObjectHealth;
    [SerializeField] public ObjectData objectData;
    private float currentObjectHealth;

    

    private SpriteRenderer spriteRenderer;


    void Start()
    {
        currentObjectHealth = maxObjectHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = objectData.objectSprite;
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        DamageObject();
    }

    private void DamageObject()
    {
        currentObjectHealth -= 1;
        
        if (currentObjectHealth <= 0)
        {
            Destroy(gameObject);
        }
        else if (currentObjectHealth < (maxObjectHealth / 3*4))
        {
            spriteRenderer.sprite = objectData.heavyDamageSprite;
        }
        else if (currentObjectHealth < (maxObjectHealth / 2))
        {
            spriteRenderer.sprite = objectData.mediumDamageSprite;
        }
    }
}
