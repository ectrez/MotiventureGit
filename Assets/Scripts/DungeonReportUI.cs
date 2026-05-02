using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DungeonReportUI : MonoBehaviour
{
    public TMP_Text xpGainedText;
    public TMP_Text goldGainedText;
    public TMP_Text xpProgressText;
    public TMP_Text floorText;
    public TMP_Text floorRecordText;
    public TMP_Text rankText;
    public TMP_Text rankUpText;

    public string guildSceneName = "Guild";

    void Start()
    {
        int runXP = PlayerPrefs.GetInt("RunXP", 0);
        int runGold = PlayerPrefs.GetInt("RunGold", 0);

        int xpStart = PlayerPrefs.GetInt("XPAtRunStart", 0);
        int xpNeededStart = PlayerPrefs.GetInt("XPNeededAtRunStart", 5);

        int xpAfter = PlayerPrefs.GetInt("XPAfterRun", PlayerPrefs.GetInt("CurrentXP", 0));
        int xpNeededAfter = PlayerPrefs.GetInt("XPNeededAfterRun", PlayerPrefs.GetInt("XPNeeded", 5));

        int lastRunFloor = PlayerPrefs.GetInt("LastRunFloor", 1);
        int floorRecord = PlayerPrefs.GetInt("FloorRecord", 0);

        string rankStart = PlayerPrefs.GetString("RankAtRunStart", "F");
        string rankAfter = PlayerPrefs.GetString("RankAfterRun", PlayerPrefs.GetString("AdventurerRank", "F"));

        bool rankedUp = PlayerPrefs.GetInt("RankedUpThisRun", 0) == 1;

        if (xpGainedText != null)
            xpGainedText.text = "XP Gained: +" + runXP;

        if (goldGainedText != null)
            goldGainedText.text = "Gold Gained: +" + runGold;

        if (xpProgressText != null)
            xpProgressText.text = xpStart + "/" + xpNeededStart + " XP  →  " + xpAfter + "/" + xpNeededAfter + " XP";

        if (floorText != null)
            floorText.text = "Floor Reached: " + lastRunFloor;

        if (floorRecordText != null)
            floorRecordText.text = "Floor Record: " + floorRecord;

        if (rankText != null)
            rankText.text = "Rank: " + rankStart + " → " + rankAfter;

        if (rankUpText != null)
            rankUpText.text = rankedUp ? "RANK UP!" : "";
    }

    public void ContinueToGuild()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(guildSceneName);
    }
}