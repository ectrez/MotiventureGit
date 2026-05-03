using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class QuestManager : MonoBehaviour
{
    public string profileSceneName = "Profile";

    [Header("Quest Settings")]
    public int questDurationSeconds = 1800;

    private string[] strengthQuests =
    {
        "Do a workout session",
        "Study for 30 minutes",
        "Clean your house",
        "Do your weekly shopping",
        "Do your emails",
        "Handle a task you've been putting off",
        "Cook a proper meal",
        "Organise your space"
    };

    private string[] vitalityQuests =
    {
        "Take a full shower",
        "Do your laundry",
        "Clean your room",
        "Clean your kitchen",
        "Do a self-care routine",
        "Reset your living space"
    };

    private string[] agilityQuests =
    {
        "Do a workout session",
        "Go for a run",
        "Do a cardio session",
        "Do a stretch session",
        "Follow a fitness routine",
        "Do a home workout"
    };

    private string[] intelligenceQuests =
    {
        "Study for 30 minutes",
        "Work on an assignment",
        "Write 300 words",
        "Revise your notes",
        "Read educational material",
        "Do a work session"
    };

    public void StartStrengthQuest()
    {
        AssignRandomQuest("STR", strengthQuests);
    }

    public void StartVitalityQuest()
    {
        AssignRandomQuest("VIT", vitalityQuests);
    }

    public void StartAgilityQuest()
    {
        AssignRandomQuest("AGI", agilityQuests);
    }

    public void StartIntelligenceQuest()
    {
        AssignRandomQuest("INT", intelligenceQuests);
    }

    private void AssignRandomQuest(string statReward, string[] questList)
    {
        string chosenQuest = questList[UnityEngine.Random.Range(0, questList.Length)];

        PlayerPrefs.SetString("ActiveQuestName", chosenQuest);
        PlayerPrefs.SetString("ActiveQuestStat", statReward);
        PlayerPrefs.SetString("QuestStartTime", DateTime.Now.ToString());
        PlayerPrefs.SetInt("QuestDurationSeconds", questDurationSeconds);
        PlayerPrefs.SetInt("QuestRewardClaimed", 0);
        PlayerPrefs.SetInt("HasActiveQuest", 1);

        PlayerPrefs.Save();

        Time.timeScale = 1f;
        SceneManager.LoadScene(profileSceneName);
    }
}