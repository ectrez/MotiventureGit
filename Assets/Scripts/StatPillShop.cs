using UnityEngine;

public class StatPillShop : MonoBehaviour
{
    public PlayerStats playerStats;
    public GuildHUD guildHUD;

    public int pillCost = 5;

    public void BuyStrengthPill()
    {
        BuyPill("STR");
    }

    public void BuyVitalityPill()
    {
        BuyPill("VIT");
    }

    public void BuyAgilityPill()
    {
        BuyPill("AGI");
    }

    public void BuyIntelligencePill()
    {
        BuyPill("INT");
    }

    private void BuyPill(string stat)
    {
        if (playerStats == null) return;

        if (!playerStats.SpendGold(pillCost))
        {
            Debug.Log("Not enough gold.");
            return;
        }

        if (stat == "STR")
            playerStats.AddStrength(1);
        else if (stat == "VIT")
            playerStats.AddVitality(1);
        else if (stat == "AGI")
            playerStats.AddAgility(1);
        else if (stat == "INT")
            playerStats.AddIntelligence(1);

        if (guildHUD != null)
            guildHUD.UpdateHUD();

        Debug.Log(stat + " pill bought.");
    }
}