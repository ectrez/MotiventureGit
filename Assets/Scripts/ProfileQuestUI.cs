using UnityEngine;
using TMPro;
using System;

public class ProfileQuestUI : MonoBehaviour
{
    public PlayerStats playerStats;
    public ProfileStatsUI profileStatsUI;

    [Header("Quest Section")]
    public GameObject questSection;

    [Header("Quest UI")]
    public TMP_Text questNameText;
    public TMP_Text questRewardText;
    public TMP_Text timerText;
    public GameObject claimButton;

    void Start()
    {
        if (playerStats == null)
            playerStats = GetComponent<PlayerStats>();

        if (profileStatsUI == null)
            profileStatsUI = GetComponent<ProfileStatsUI>();

        UpdateQuestUI();
    }

    void Update()
    {
        UpdateQuestUI();
    }

    private void UpdateQuestUI()
    {
        if (PlayerPrefs.GetInt("HasActiveQuest", 0) == 0)
        {
            if (questSection != null)
                questSection.SetActive(false);

            return;
        }

        if (questSection != null)
            questSection.SetActive(true);

        string questName = PlayerPrefs.GetString("ActiveQuestName", "Unknown Quest");
        string questStat = PlayerPrefs.GetString("ActiveQuestStat", "STR");
        string startTimeString = PlayerPrefs.GetString("QuestStartTime", DateTime.Now.ToString());
        int duration = PlayerPrefs.GetInt("QuestDurationSeconds", 1800);

        DateTime startTime = DateTime.Parse(startTimeString);
        TimeSpan elapsed = DateTime.Now - startTime;
        int remainingSeconds = duration - Mathf.FloorToInt((float)elapsed.TotalSeconds);

        if (questNameText != null)
            questNameText.text = questName;

        if (questRewardText != null)
            questRewardText.text = "+5 " + questStat;

        if (remainingSeconds <= 0)
        {
            if (timerText != null)
                timerText.text = "Ready to claim";

            if (claimButton != null)
                claimButton.SetActive(true);
        }
        else
        {
            TimeSpan remaining = TimeSpan.FromSeconds(remainingSeconds);

            if (timerText != null)
                timerText.text = remaining.Minutes.ToString("00") + ":" + remaining.Seconds.ToString("00");

            if (claimButton != null)
                claimButton.SetActive(false);
        }
    }

    public void ClaimQuestReward()
    {
        if (PlayerPrefs.GetInt("HasActiveQuest", 0) == 0) return;
        if (playerStats == null) return;

        string questStat = PlayerPrefs.GetString("ActiveQuestStat", "STR");

        if (questStat == "STR")
            playerStats.AddStrength(5);
        else if (questStat == "VIT")
            playerStats.AddVitality(5);
        else if (questStat == "AGI")
            playerStats.AddAgility(5);
        else if (questStat == "INT")
            playerStats.AddIntelligence(5);

        PlayerPrefs.SetInt("HasActiveQuest", 0);
        PlayerPrefs.Save();

        if (profileStatsUI != null)
            profileStatsUI.UpdateProfileUI();

        if (questSection != null)
            questSection.SetActive(false);
    }
}