using System;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;


public class Player : MonoBehaviour
{
    [SerializeField] 
    private float speed = 5f;

    [SerializeField] 
    private HealthbarUIComponent healthbarUI;

    [SerializeField]
    private Collectable collectable;

    [SerializeField]
    private float HealthGain = 2f;

    [SerializeField] 
    private GameObject GunTypeSprite;

    private Rigidbody2D rigidBody2D;
    private InputAction moveInput;
    private Camera mainCamera;
    private Vector3 mouseScreenPos;
    private Vector3 mouseWorldPos;
    private SpriteRenderer spriteRenderer;
    private PlayerGun gun;

    [SerializeField] private bool hasShotgun;

    

    public float maxLife = 10f;
    public float life = 10f;
    private bool facingRight = false;

    //Implement, Press E to interact.
    

    void Start()
    {
        hasShotgun = false;
        rigidBody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        gun = GetComponentInChildren<PlayerGun>();
        gun.SwapGun(0);
        mainCamera = Camera.main;
        moveInput = InputSystem.actions.FindAction("Move");
        moveInput.Enable();
        healthbarUI.SetMaxHealth(maxLife);
    }


    void Update()
    {
        mouseScreenPos = Mouse.current.position.ReadValue();
        mouseWorldPos = mainCamera.ScreenToWorldPoint(mouseScreenPos);
        mouseWorldPos.z = 0;

        HandleInput();
        HandleMovement();
        
    }

    private void HandleAiming()
    {
        Vector3 direction = (mouseWorldPos - transform.position).normalized;
        gun.transform.position = transform.position + direction * gun.gunData.gunDistance;
        gun.AimAt(mouseWorldPos);
    }

    private void HandleInput()
    {
        if (Mouse.current.leftButton.isPressed)
        {
            gun.TryShoot();
        }

        int maxGunIndex = hasShotgun ? 2 : 1;

        for (int i = 0; i < maxGunIndex; i++)
        {
            if (Keyboard.current[(Key)(Key.Digit1 + i)].wasPressedThisFrame)
            {
                gun.SwapGun(i);
                break;
            }
        }
    }

private void HandleMovement()
{
    bool mouseLeft = mouseWorldPos.x < transform.position.x;
    
    if (Math.Abs(mouseWorldPos.x - transform.position.x) > 1 || 
        Math.Abs(mouseWorldPos.y - transform.position.y) > 1)
    {
        HandleAiming();
        if (mouseLeft && facingRight)
        {
            Flip();
        }
        else if (!mouseLeft && !facingRight)
        {
            Flip();
        }
    }

    Vector2 moveValue = moveInput.ReadValue<Vector2>();
    rigidBody2D.linearVelocity = moveValue * speed;
}

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;
        gun.transform.localScale = gun.transform.localScale * -1;
    }

    public void TakeDamage(float damage)
    {
        SetHealth(-damage);
        if(life <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        GetComponent<Rigidbody2D>().simulated = false;
        GetComponent<SpriteRenderer>().enabled = false;
        this.enabled = false;
        
        FindFirstObjectByType<PlayerMenuComponent>().ShowPlayerMenu();
    }

    public void SetHealth(float healthChange)
    {
        life += healthChange;
        life = Mathf.Clamp(life, 0, maxLife);
        healthbarUI.SetHealth(life);
    }

    public void OnPickupCollectable(CollectableType type)
    {
        switch (type)
        {
            case CollectableType.Health:
                SetHealth(HealthGain);
                break;
            case CollectableType.Shotgun:
                this.hasShotgun = true;
                FindFirstObjectByType<HUDComponent>().OnShotgunCollected();
                break;
        }
    }

    void OnApplicationQuit()
    {
        SaveSystem.SaveGame();
    }
}



