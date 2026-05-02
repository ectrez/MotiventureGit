using UnityEngine;
using TMPro;

public class GuildHUD : MonoBehaviour
{
    public PlayerStats playerStats;

    [Header("Top HUD TMPs")]
    public TMP_Text bpText;
    public TMP_Text rankText;
    public TMP_Text keysText;
    public TMP_Text goldText;

    void Start()
    {
        UpdateHUD();
    }

    public void UpdateHUD()
    {
        if (playerStats == null)
        {
            Debug.LogWarning("GuildHUD: PlayerStats is not assigned.");
            return;
        }

        playerStats.LoadStats();

        if (bpText != null)
            bpText.text = "BP: " + playerStats.GetBP();

        if (rankText != null)
            rankText.text = "Rank: " + playerStats.adventurerRank;

        if (keysText != null)
            keysText.text = playerStats.keys + "/" + playerStats.maxKeys;

        if (goldText != null)
            goldText.text = ": " + playerStats.gold;
        else
            Debug.LogWarning("GuildHUD: Gold TMP is not assigned.");
    }
}