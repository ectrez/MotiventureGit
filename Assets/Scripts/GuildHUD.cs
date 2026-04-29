using UnityEngine;
using TMPro;

public class GuildHUD : MonoBehaviour
{
    public PlayerStats playerStats;

    public TMP_Text bpText;
    public TMP_Text rankText;
    public TMP_Text keysText;

    void Start()
    {
        UpdateHUD();
    }

    public void UpdateHUD()
    {
        if (playerStats == null) return;

        if (bpText != null)
            bpText.text = "BP: " + playerStats.GetBP();

        if (rankText != null)
            rankText.text = "Rank: " + playerStats.adventurerRank;

        if (keysText != null)
            keysText.text = playerStats.keys + "/" + playerStats.maxKeys;
    }
}