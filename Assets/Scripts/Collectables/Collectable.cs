using UnityEngine;
using UnityEngine.Events;

public enum CollectableType
{
    Health,
    Shotgun
}

public class Collectable : MonoBehaviour
{
    public UnityEvent<CollectableType> OnCollected;
    public CollectableType variantType;
    [SerializeField] private float bobHeight = 0.5f;
    [SerializeField] private float bobSpeed = 2f;
    
    private Vector3 startPosition;
    private bool hasBeenCollected = false;
    
    void Start()
    {
        startPosition = transform.position;
    }
    
    void Update()
    {
        float newY = startPosition.y + Mathf.Sin(Time.time * bobSpeed) * bobHeight;
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (hasBeenCollected) return;
        
        if (other.CompareTag("Player"))
        {
            // Player player = other.GetComponent<Player>();
            // if (player != null)
            // {
            //     player.SetHealth(healAmount);
            //     hasBeenCollected = true;
            //     Destroy(gameObject);
            // }
            hasBeenCollected = true;
            OnCollected?.Invoke(variantType);
            Destroy(gameObject);
        }
    }
}