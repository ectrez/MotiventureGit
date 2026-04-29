using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonButton : MonoBehaviour
{
    public string dungeonSceneName = "Dungeon";

    public PlayerStats playerStats;
    public GuildHUD guildHUD;

    public void EnterDungeon()
    {
        if (playerStats == null) return;

        if (!playerStats.UseKey())
        {
            Debug.Log("No keys remaining.");
            return;
        }

        if (guildHUD != null)
            guildHUD.UpdateHUD();

        Time.timeScale = 1f;
        SceneManager.LoadScene(dungeonSceneName);
    }
}