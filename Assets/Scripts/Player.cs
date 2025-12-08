using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;

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
    private PlayerHUD playerHUD;

    [SerializeField] private bool hasShotgun;

    public float maxLife = 10f;
    public float life = 10f;
    private bool facingRight = true;

    void Start()
    {
        hasShotgun = false;
        rigidBody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        gun = GetComponentInChildren<PlayerGun>();
        playerHUD = FindFirstObjectByType<PlayerHUD>();
        gun.SwapGun(0);
        mainCamera = Camera.main;
        moveInput = InputSystem.actions.FindAction("Move");
        moveInput.Enable();

        // initialize HUD health display
        playerHUD.UpdateHealthBar();
    }

    void Update()
    {
        // Ensure we have a camera reference (some scenes or play-modes may not have Camera.main immediately)
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
            if (mainCamera == null) return; // nothing we can do this frame
        }

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
        if (life <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        GetComponent<Rigidbody2D>().simulated = false;
        GetComponent<SpriteRenderer>().enabled = false;
        this.enabled = false;

        FindFirstObjectByType<PlayerMenuComponent>().OnPlayerDeath();
    }

    public void SetHealth(float healthChange)
    {
        life += healthChange;
        life = Mathf.Clamp(life, 0, maxLife);

        if (playerHUD != null)
        {
            playerHUD.UpdateHealthBar();
        }
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
                break;
        }
    }

    void OnApplicationQuit()
    {
        SaveSystem.SaveGame();
    }
}



