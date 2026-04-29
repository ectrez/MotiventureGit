using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int strength = 10;
    public int vitality = 10;
    public int agility = 10;
    public int intelligence = 10;

    [Header("Keys")]
    public int keys = 6;
    public int maxKeys = 6;

    [Header("Rank / XP")]
    public int currentXP = 0;
    public int xpNeeded = 5;
    public int rankLevel = 0;
    public string adventurerRank = "F";

    [Header("Records")]
    public int floorRecord = 0;

    private int runXP = 0;
    private int xpAtRunStart = 0;
    private int xpNeededAtRunStart = 5;
    private string rankAtRunStart = "F";
    private bool rankedUpThisRun = false;

    void Awake()
    {
        LoadStats();
    }

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

    public int GetBP()
    {
        return strength + vitality + agility + intelligence;
    }

    public bool UseKey()
    {
        if (keys <= 0)
            return false;

        keys--;
        SaveStats();
        return true;
    }

    public void StartDungeonRun()
    {
        runXP = 0;
        xpAtRunStart = currentXP;
        xpNeededAtRunStart = xpNeeded;
        rankAtRunStart = adventurerRank;
        rankedUpThisRun = false;

        PlayerPrefs.SetInt("RunXP", runXP);
        PlayerPrefs.SetInt("XPAtRunStart", xpAtRunStart);
        PlayerPrefs.SetInt("XPNeededAtRunStart", xpNeededAtRunStart);
        PlayerPrefs.SetString("RankAtRunStart", rankAtRunStart);
        PlayerPrefs.SetInt("RankedUpThisRun", 0);
        PlayerPrefs.Save();
    }

    public void AddXP(int amount)
    {
        runXP += amount;
        currentXP += amount;

        while (currentXP >= xpNeeded)
        {
            currentXP -= xpNeeded;
            rankLevel++;
            xpNeeded *= 2;
            rankedUpThisRun = true;
            UpdateRankName();
        }

        PlayerPrefs.SetInt("RunXP", runXP);
        PlayerPrefs.SetInt("RankedUpThisRun", rankedUpThisRun ? 1 : 0);

        SaveStats();
    }

    public void SaveFloorRecord(int floorReached)
    {
        if (floorReached > floorRecord)
        {
            floorRecord = floorReached;
            SaveStats();
        }

        PlayerPrefs.SetInt("LastRunFloor", floorReached);
        PlayerPrefs.Save();
    }

    private void UpdateRankName()
    {
        if (rankLevel <= 0) adventurerRank = "F";
        else if (rankLevel == 1) adventurerRank = "E";
        else if (rankLevel == 2) adventurerRank = "D";
        else if (rankLevel == 3) adventurerRank = "C";
        else if (rankLevel == 4) adventurerRank = "B";
        else if (rankLevel == 5) adventurerRank = "A";
        else adventurerRank = "S";
    }

    public void SaveStats()
    {
        PlayerPrefs.SetInt("Strength", strength);
        PlayerPrefs.SetInt("Vitality", vitality);
        PlayerPrefs.SetInt("Agility", agility);
        PlayerPrefs.SetInt("Intelligence", intelligence);

        PlayerPrefs.SetInt("Keys", keys);
        PlayerPrefs.SetInt("MaxKeys", maxKeys);

        PlayerPrefs.SetInt("CurrentXP", currentXP);
        PlayerPrefs.SetInt("XPNeeded", xpNeeded);
        PlayerPrefs.SetInt("RankLevel", rankLevel);
        PlayerPrefs.SetString("AdventurerRank", adventurerRank);

        PlayerPrefs.SetInt("FloorRecord", floorRecord);

        PlayerPrefs.Save();
    }

    public void LoadStats()
    {
        strength = PlayerPrefs.GetInt("Strength", 10);
        vitality = PlayerPrefs.GetInt("Vitality", 10);
        agility = PlayerPrefs.GetInt("Agility", 10);
        intelligence = PlayerPrefs.GetInt("Intelligence", 10);

        keys = PlayerPrefs.GetInt("Keys", 6);
        maxKeys = PlayerPrefs.GetInt("MaxKeys", 6);

        currentXP = PlayerPrefs.GetInt("CurrentXP", 0);
        xpNeeded = PlayerPrefs.GetInt("XPNeeded", 5);
        rankLevel = PlayerPrefs.GetInt("RankLevel", 0);
        adventurerRank = PlayerPrefs.GetString("AdventurerRank", "F");

        floorRecord = PlayerPrefs.GetInt("FloorRecord", 0);
    }
}