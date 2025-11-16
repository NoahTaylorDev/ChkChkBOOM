using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private HealthbarUIComponent healthbarUI;
    private Rigidbody2D rigidBody2D;
    private InputAction moveInput;
    private Camera mainCamera;
    private Vector3 mouseScreenPos;
    private Vector3 mouseWorldPos;
    private SpriteRenderer spriteRenderer;
    private PlayerGun gun;

    public float maxLife = 10f;
    public float life = 10f;
    private bool facingRight = false;

    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        gun = GetComponentInChildren<PlayerGun>();
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
        gun.transform.position = transform.position + direction * gun.gunDistance;
        gun.AimAt(mouseWorldPos);
    }

    private void HandleInput()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            gun.TryShoot();
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
        SetHealth(damage);
        if(life <= 0)
        {
            
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    public void SetHealth(float healthChange)
    {
        life -= healthChange;
        life = Mathf.Clamp(life, 0, maxLife);
        healthbarUI.SetHealth(life);
    }
}


