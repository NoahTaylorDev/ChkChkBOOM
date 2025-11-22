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

    [SerializeField]
    private TMP_Text healthText;

    [SerializeField]
    private TMP_Text damageText;
    [SerializeField]
    private TMP_Text healthInfoText;
    [SerializeField]
    private TMP_Text damageInfoText;

    

    [SerializeField] public GlobalData globalData;

    void Start()
    {
        SaveSystem.LoadGame();
        globalData.AddBoombucks(500);
        UpdateHealthText();
        UpdateDamageText();
        UpdateBoomBucksText();
        UpdateHealthInfo();
        UpdateDamageInfo();
    }

    public void CloseClick()
    {
        this.GameObject().SetActive(false);
    }

    public void UpdateHealthInfo()
    {
        healthInfoText.text = $"Health: {globalData.playerHealth} => {globalData.playerHealth + globalData.playerHealthIncrement}";
    }

    public void UpdateDamageInfo()
    {
        damageInfoText.text = $"Damage: {globalData.playerDamage} => {globalData.playerDamage + globalData.playerDamageIncrement}";
    }

    public void UpdateHealthText()
    {
        healthText.text = $"{globalData.healthCost}";
    }

    public void UpdateDamageText()
    {
        damageText.text = $"{globalData.damageCost}";
    }

    public void UpdateBoomBucksText()
    {
        boombucksText.text = $"{globalData.boombucks}";
    }



    public void BuyDamageClick()
    {
        if (globalData.SpendBoombucks(globalData.damageCost))
        {
            globalData.IncrementDamage();
            UpdateDamageText();
            UpdateBoomBucksText();
            UpdateDamageInfo();
        } 
    }

    public void BuyHealthClick()
    {
        if (globalData.SpendBoombucks(globalData.healthCost))
        {
            globalData.IncrementHealth();
            UpdateHealthText();
            UpdateBoomBucksText();
            UpdateHealthInfo();
        } 
    }
}
