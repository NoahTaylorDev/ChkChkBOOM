using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ExitComponent : MonoBehaviour
{
    
    private bool isActive = false;
    
    private bool isPlayerPresent = false;
    
    void Start()
    {
                  
    }

    
    void Update()
    {
        if (isActive && isPlayerPresent)
        {
            if (Keyboard.current[Key.E].wasPressedThisFrame)
            {
                SceneManager.LoadScene("Main");
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
