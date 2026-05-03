using UnityEngine;
using System;

public class PlayerStats : MonoBehaviour
{
    public int strength = 10;
    public int vitality = 10;
    public int agility = 10;
    public int intelligence = 10;

    [Header("Currency")]
    public int gold = 0;

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
    private int runGold = 0;
    private bool rankedUpThisRun = false;

    private const int KEY_COOLDOWN_SECONDS = 10800; // 3 hours

    void Awake()
    {
        LoadStats();
        RefreshKeysIfNeeded();
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
        RefreshKeysIfNeeded();

        if (keys <= 0)
            return false;

        keys--;

        PlayerPrefs.SetString("LastKeyUseTime", DateTime.Now.ToString());

        SaveStats();
        return true;
    }

    public void RefreshKeysIfNeeded()
    {
        if (!PlayerPrefs.HasKey("LastKeyUseTime"))
        {
            PlayerPrefs.SetString("LastKeyUseTime", DateTime.Now.ToString());
            PlayerPrefs.Save();
            return;
        }

        DateTime lastUse = DateTime.Parse(PlayerPrefs.GetString("LastKeyUseTime"));
        TimeSpan elapsed = DateTime.Now - lastUse;

        if (elapsed.TotalSeconds >= KEY_COOLDOWN_SECONDS)
        {
            keys = maxKeys;
            PlayerPrefs.SetString("LastKeyUseTime", DateTime.Now.ToString());
            SaveStats();
        }
    }

    public void StartDungeonRun()
    {
        runXP = 0;
        runGold = 0;
        rankedUpThisRun = false;

        PlayerPrefs.SetInt("RunXP", 0);
        PlayerPrefs.SetInt("RunGold", 0);

        PlayerPrefs.SetInt("XPAtRunStart", currentXP);
        PlayerPrefs.SetInt("XPNeededAtRunStart", xpNeeded);
        PlayerPrefs.SetInt("XPAfterRun", currentXP);
        PlayerPrefs.SetInt("XPNeededAfterRun", xpNeeded);

        PlayerPrefs.SetString("RankAtRunStart", adventurerRank);
        PlayerPrefs.SetString("RankAfterRun", adventurerRank);
        PlayerPrefs.SetInt("RankedUpThisRun", 0);

        PlayerPrefs.Save();
    }

    public void AddXP(int amount)
    {
        runXP = PlayerPrefs.GetInt("RunXP", 0);
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
        PlayerPrefs.SetInt("XPAfterRun", currentXP);
        PlayerPrefs.SetInt("XPNeededAfterRun", xpNeeded);
        PlayerPrefs.SetString("RankAfterRun", adventurerRank);
        PlayerPrefs.SetInt("RankedUpThisRun", rankedUpThisRun ? 1 : 0);

        SaveStats();
    }

    public void AddGold(int amount)
    {
        runGold = PlayerPrefs.GetInt("RunGold", 0);
        runGold += amount;

        gold += amount;

        PlayerPrefs.SetInt("RunGold", runGold);

        SaveStats();
    }

    public bool SpendGold(int amount)
    {
        if (gold < amount)
            return false;

        gold -= amount;
        SaveStats();
        return true;
    }

    public void SaveFloorRecord(int floorReached)
    {
        if (floorReached > floorRecord)
            floorRecord = floorReached;

        PlayerPrefs.SetInt("LastRunFloor", floorReached);
        SaveStats();
    }

    public void AddStrength(int amount)
    {
        strength += amount;
        SaveStats();
    }

    public void AddVitality(int amount)
    {
        vitality += amount;
        SaveStats();
    }

    public void AddAgility(int amount)
    {
        agility += amount;
        SaveStats();
    }

    public void AddIntelligence(int amount)
    {
        intelligence += amount;
        SaveStats();
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
        PlayerPrefs.SetInt("HasSave", 1);

        PlayerPrefs.SetInt("Strength", strength);
        PlayerPrefs.SetInt("Vitality", vitality);
        PlayerPrefs.SetInt("Agility", agility);
        PlayerPrefs.SetInt("Intelligence", intelligence);

        PlayerPrefs.SetInt("Gold", gold);

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
        if (!PlayerPrefs.HasKey("HasSave"))
        {
            SaveStats();
            return;
        }

        strength = PlayerPrefs.GetInt("Strength");
        vitality = PlayerPrefs.GetInt("Vitality");
        agility = PlayerPrefs.GetInt("Agility");
        intelligence = PlayerPrefs.GetInt("Intelligence");

        gold = PlayerPrefs.GetInt("Gold");

        keys = PlayerPrefs.GetInt("Keys");
        maxKeys = PlayerPrefs.GetInt("MaxKeys");

        currentXP = PlayerPrefs.GetInt("CurrentXP");
        xpNeeded = PlayerPrefs.GetInt("XPNeeded");
        rankLevel = PlayerPrefs.GetInt("RankLevel");
        adventurerRank = PlayerPrefs.GetString("AdventurerRank");

        floorRecord = PlayerPrefs.GetInt("FloorRecord");
    }
}