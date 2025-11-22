using UnityEngine;

[CreateAssetMenu(fileName = "GlobalData", menuName = "Scriptable Objects/GlobalData")]
public class GlobalData : ScriptableObject
{
    public int boombucks;
    public float playerHealth;

    public float playerHealthIncrement;
    public float playerDamage;

    public float playerDamageIncrement;

    public int healthCost;
    public int damageCost;

    public GlobalData()
    {
        boombucks = 10;
        playerHealth = 50f;
        playerHealthIncrement = 10f;
        playerDamage = 4f;
        playerDamageIncrement = 1f;
        healthCost = 100;
        damageCost = 100;
    }


    public void AddBoombucks(int amount)
    {
        boombucks += amount;
    }

    public bool SpendBoombucks(int amount)
    {
        if (boombucks >= amount)
        {
            boombucks -= amount;
            return true;
        }
        return false;
    }

    public void IncrementHealth()
    {
        healthCost = (int)(healthCost * 1.2f);
        playerHealth += playerHealthIncrement;
    }

    public void IncrementDamage()
    {
        damageCost = (int)(damageCost * 1.2f);
        playerDamage += playerDamageIncrement;
    }

}