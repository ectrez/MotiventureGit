using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ProfileStatsUI : MonoBehaviour
{
    public PlayerStats playerStats;

    [Header("Stats")]
    public TMP_Text strText;
    public TMP_Text vitText;
    public TMP_Text agiText;
    public TMP_Text intText;

    [Header("Progression")]
    public TMP_Text bpText;
    public TMP_Text rankText;
    public TMP_Text keysText;
    public TMP_Text goldText;
    public TMP_Text xpText;

    public string guildSceneName = "Guild";

    void Start()
    {
        UpdateProfileUI();
    }

    public void UpdateProfileUI()
    {
        if (playerStats == null) return;

        if (strText != null)
            strText.text = "STR: " + playerStats.strength;

        if (vitText != null)
            vitText.text = "VIT: " + playerStats.vitality;

        if (agiText != null)
            agiText.text = "AGI: " + playerStats.agility;

        if (intText != null)
            intText.text = "INT: " + playerStats.intelligence;

        if (bpText != null)
            bpText.text = "BP: " + playerStats.GetBP();

        if (rankText != null)
            rankText.text = "Rank: " + playerStats.adventurerRank;

        if (keysText != null)
            keysText.text = "Keys: " + playerStats.keys + "/" + playerStats.maxKeys;

        if (goldText != null)
            goldText.text = "Gold: " + playerStats.gold;

        if (xpText != null)
            xpText.text = "XP: " + playerStats.currentXP + "/" + playerStats.xpNeeded;
    }

    public void BackToGuild()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(guildSceneName);
    }
}