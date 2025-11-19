using UnityEngine;

[CreateAssetMenu(fileName = "GlobalData", menuName = "Scriptable Objects/GlobalData")]
public class GlobalData : ScriptableObject
{
    public int boombucks;
    public float playerHealth;
    public float playerDamageBonus;

    public GlobalData()
    {
        boombucks = 10;
        playerHealth = 10f;
        playerDamageBonus = 2f;
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


}