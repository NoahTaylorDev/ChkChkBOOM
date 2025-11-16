using UnityEngine;
using UnityEngine.UI;

public class HealthbarUIComponent : MonoBehaviour
{
    public float health, maxHealth, width, height;
    [SerializeField]
    private RectTransform healthBar;

    public void SetMaxHealth(float newHealth)
    {
        maxHealth = newHealth;
    }

    public void SetHealth(float newHealth)
    {
        health = newHealth;
        float newWidth = health / maxHealth * width;
        healthBar.sizeDelta = new Vector2(newWidth, height);
    }
        
}
