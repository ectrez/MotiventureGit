using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DungeonReportUI : MonoBehaviour
{
    public TMP_Text xpGainedText;
    public TMP_Text xpProgressText;
    public TMP_Text floorText;
    public TMP_Text floorRecordText;
    public TMP_Text rankText;
    public TMP_Text rankUpText;

    public string guildSceneName = "Guild";

    void Start()
    {
        int runXP = PlayerPrefs.GetInt("RunXP", 0);

        int xpStart = PlayerPrefs.GetInt("XPAtRunStart", 0);
        int xpNeededStart = PlayerPrefs.GetInt("XPNeededAtRunStart", 5);

        int currentXP = PlayerPrefs.GetInt("CurrentXP", 0);
        int xpNeeded = PlayerPrefs.GetInt("XPNeeded", 5);

        int lastRunFloor = PlayerPrefs.GetInt("LastRunFloor", 1);
        int floorRecord = PlayerPrefs.GetInt("FloorRecord", 0);

        string currentRank = PlayerPrefs.GetString("AdventurerRank", "F");
        string rankStart = PlayerPrefs.GetString("RankAtRunStart", "F");

        bool rankedUp = PlayerPrefs.GetInt("RankedUpThisRun", 0) == 1;

        if (xpGainedText != null)
            xpGainedText.text = "XP Gained: +" + runXP;

        if (xpProgressText != null)
            xpProgressText.text = xpStart + "/" + xpNeededStart + " XP  →  " + currentXP + "/" + xpNeeded + " XP";

        if (floorText != null)
            floorText.text = "Floor Reached: " + lastRunFloor;

        if (floorRecordText != null)
            floorRecordText.text = "Floor Record: " + floorRecord;

        if (rankText != null)
            rankText.text = "Rank: " + rankStart + " → " + currentRank;

        if (rankUpText != null)
            rankUpText.text = rankedUp ? "RANK UP!" : "";
    }

    public void ContinueToGuild()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(guildSceneName);
    }
}