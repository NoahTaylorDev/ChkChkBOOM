using UnityEngine;
using UnityEngine.InputSystem;

public class ExitComponent : MonoBehaviour
{
    
    private bool isActive = false;
    
    private bool isPlayerPresent = false;

    private PlayerUIController playerUIController;
    
    void Start()
    {
        playerUIController = FindFirstObjectByType<PlayerUIController>();           
    }

    
    void Update()
    {
        if (isActive && isPlayerPresent)
        {
            if (Keyboard.current[Key.E].wasPressedThisFrame)
            {
                playerUIController.RestartScene();
            }
        }
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        isPlayerPresent = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        isPlayerPresent = false;
    }

    public void Activate()
    {
        isActive = true;
    }
}
