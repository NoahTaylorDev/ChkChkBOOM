using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ProgressionMenuController : MonoBehaviour
{
    [SerializeField]
    private Button close;
    [SerializeField]
    private Button buyDamage;
    [SerializeField]
    private Button buyHealth;
    [SerializeField]
    private TMP_Text boombucksText;
    private int damageCost = 100;
    private int healthCost = 100;

    private TMP_Text healthText;

    [SerializeField] public GlobalData globalData;

    public int Boombucks => globalData.boombucks;

    void Start()
    {
        healthText = GetComponentInChildren<TMP_Text>();
        healthText.text = $"{healthCost}";
        boombucksText.text = $"{Boombucks}";
        SaveSystem.LoadGame();
    }

    public void CloseClick()
    {
        this.GameObject().SetActive(false);
    }

    public void BuyDamageClick()
    {
        
    }

    public void BuyHealthClick()
    {
        if (globalData.SpendBoombucks(healthCost))
        {
            healthCost += 100;
        }
        
        healthText.text = $"{healthCost}";
        boombucksText.text = $"{Boombucks}";
    }
}
