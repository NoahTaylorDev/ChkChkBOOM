using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

public class ShopUIController : MonoBehaviour
{
    private UIDocument shopDocument;
    private Button closeButton;
    private Button healthButton;
    private Button damageButton;
    private VisualElement healthProgressNode;
    private VisualElement damageProgressNode;

    [SerializeField] public GlobalData globalData;

    [SerializeField] private int healthMaxUpgrades = 10;
    
    [SerializeField] private int damageMaxUpgrades = 10;

    private int healthUpgradeCount = 0;
    private int damageUpgradeCount = 0;

    void Awake()
    {
        shopDocument = GetComponent<UIDocument>();
        
        closeButton = shopDocument.rootVisualElement.Q("CloseShopButton") as Button;
        healthButton = shopDocument.rootVisualElement.Q("HealthButton") as Button;
        damageButton = shopDocument.rootVisualElement.Q("DamageButton") as Button;
        
        var container = shopDocument.rootVisualElement.Q("Container");
        int progressBarCount = 0;
        
        foreach (var child in container.Children())
        {
            var progressBar = child.Q("ProgressBar");
            if (progressBar != null)
            {
                progressBarCount++;
                var node = progressBar.Q("ProgressNode");
                
                if (progressBarCount == 1)
                {
                    healthProgressNode = node;
                }
                else if (progressBarCount == 2)
                {
                    damageProgressNode = node;
                    break;
                }
            }
        }
        
        closeButton.RegisterCallback<ClickEvent>(OnCloseClick);
        healthButton.RegisterCallback<ClickEvent>(OnHealthBuyClick);
        damageButton.RegisterCallback<ClickEvent>(OnDamageBuyClick);
        
        UpdateCostDisplay();
        shopDocument.rootVisualElement.style.display = DisplayStyle.None;
    }

    private void OnCloseClick(ClickEvent evt)
    {
        HideShop();
    }

    private void OnHealthBuyClick(ClickEvent evt)
    {
        if (healthUpgradeCount < healthMaxUpgrades && globalData.SpendBoombucks(globalData.healthCost))
        {
            globalData.IncrementHealth();
            healthUpgradeCount++;
            UpdateHealthProgressBar();
            UpdateCostDisplay();
        }
    }

    private void OnDamageBuyClick(ClickEvent evt)
    {
        if (damageUpgradeCount < damageMaxUpgrades && globalData.SpendBoombucks(globalData.damageCost))
        {
            globalData.IncrementDamage();
            damageUpgradeCount++;
            UpdateDamageProgressBar();
            UpdateCostDisplay();
        }
    }

    private void UpdateCostDisplay()
    {
        if (healthButton != null)
        {
            healthButton.text = $"Health\n{globalData.healthCost}";
        }
        if (damageButton != null)
        {
            damageButton.text = $"Damage\n{globalData.damageCost}";
        }
    }

    private void UpdateHealthProgressBar()
    {
        if (healthProgressNode != null)
        {
            float fillPercentage = (float)healthUpgradeCount / healthMaxUpgrades;
            healthProgressNode.style.width = Length.Percent(fillPercentage * 100f);
        }
    }

    private void UpdateDamageProgressBar()
    {
        if (damageProgressNode != null)
        {
            float fillPercentage = (float)damageUpgradeCount / damageMaxUpgrades;
            damageProgressNode.style.width = Length.Percent(fillPercentage * 100f);
        }
    }

    public void ShowShop()
    {
        shopDocument.rootVisualElement.style.display = DisplayStyle.Flex;
    }

    public void HideShop()
    {
        shopDocument.rootVisualElement.style.display = DisplayStyle.None;
    }

    void OnDisable()
    {
        closeButton?.UnregisterCallback<ClickEvent>(OnCloseClick);
        healthButton?.UnregisterCallback<ClickEvent>(OnHealthBuyClick);
        damageButton?.UnregisterCallback<ClickEvent>(OnDamageBuyClick);
    }
}
