using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonButton : MonoBehaviour
{
    public string dungeonSceneName = "Dungeon";

    public PlayerStats playerStats;
    public GuildHUD guildHUD;

    public void EnterDungeon()
    {
        if (playerStats == null)
        {
            Debug.LogWarning("DungeonButton: PlayerStats is not assigned.");
            return;
        }

        if (!playerStats.UseKey())
        {
            Debug.Log("No keys remaining.");
            return;
        }

        playerStats.StartDungeonRun();

        if (guildHUD != null)
            guildHUD.UpdateHUD();

        Time.timeScale = 1f;
        SceneManager.LoadScene(dungeonSceneName);
    }
}