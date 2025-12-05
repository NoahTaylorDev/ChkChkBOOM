using UnityEngine;
using UnityEngine.UIElements;

public class PlayerHUD : MonoBehaviour
{
    private UIDocument uiDocument;
    private Label boombucksCounter;
    private Label ammoCounter;
    private VisualElement healthProgressNode;

    [SerializeField] public GlobalData globalData;
    [SerializeField] private PlayerGun playerGun;
    private Player player;

    void Awake()
    {
        uiDocument = GetComponent<UIDocument>();
        
        boombucksCounter = uiDocument.rootVisualElement.Q("CurrencyCounter") as Label;
        ammoCounter = uiDocument.rootVisualElement.Q("AmmoCounter") as Label;
        
        var healthProgressBar = uiDocument.rootVisualElement.Q("ProgressBar");
        healthProgressNode = healthProgressBar?.Q("ProgressNode");
        player = FindFirstObjectByType<Player>();
    }

    void Start()
    {
        UpdateBoombucksDisplay();
        UpdateAmmoDisplay();
        UpdateHealthBar();
    }

    void Update()
    {
        UpdateAmmoDisplay();
        UpdateHealthBar();
    }

    public void UpdateBoombucksDisplay()
    {
        if (boombucksCounter != null && globalData != null)
        {
            boombucksCounter.text = $": {globalData.boombucks}";
        }
    }

    public void UpdateAmmoDisplay()
    {
        if (ammoCounter != null && playerGun != null)
        {
            ammoCounter.text = $": {playerGun.ammoRemaining}";
        }
    }

    public void UpdateHealthBar()
    {
        if (healthProgressNode != null && player != null)
        {
            float healthPercentage = player.life / player.maxLife;
            healthProgressNode.style.width = Length.Percent(healthPercentage * 100f);
        }
    }
}
