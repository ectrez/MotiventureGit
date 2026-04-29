using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int strength = 10;
    public int vitality = 10;
    public int agility = 10;
    public int intelligence = 10;

    public int GetMaxHealth()
    {
        return vitality + agility;
    }

    public int GetMaxMana()
    {
        return intelligence;
    }

    public int GetSwordDamage()
    {
        return Mathf.Max(1, Mathf.RoundToInt(strength * 0.1f));
    }
}