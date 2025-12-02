using UnityEngine;
using UnityEngine.Events;

public enum CollectableType
{
    Health,
    Shotgun
}

public class Collectable : MonoBehaviour
{
    public CollectableType variantType;
    [SerializeField] private float bobHeight = 0.5f;
    [SerializeField] private float bobSpeed = 2f;
    private Player player;
    
    private Vector3 startPosition;
    private bool hasBeenCollected = false;
    
    void Start()
    {
        startPosition = transform.position;
        player = FindFirstObjectByType<Player>();
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
            if (player != null)
            {
                hasBeenCollected = true;
                FindAnyObjectByType<Player>().OnPickupCollectable(variantType);
                Destroy(gameObject);
            }
            
        }
    }
}