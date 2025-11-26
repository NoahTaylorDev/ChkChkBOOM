using UnityEngine;
using UnityEngine.UI;

public class HealthbarUIComponent : MonoBehaviour
{
    public float health, maxHealth;
    [SerializeField]
    private RectTransform healthBar;

    public void SetMaxHealth(float newHealth)
    {
        maxHealth = newHealth;
    }

    public void SetHealth(float newHealth)
    {
        health = Mathf.Clamp(newHealth, 0, maxHealth);
        float healthPercent = health / maxHealth;
        healthBar.anchorMax = new Vector2(healthPercent, 1f);
    }
}