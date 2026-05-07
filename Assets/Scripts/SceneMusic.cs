using UnityEngine;

public class SceneMusic : MonoBehaviour
{
    public AudioManager audioManager;
    public bool useDungeonMusic;

    void Start()
    {
        if (audioManager == null)
            audioManager = FindFirstObjectByType<AudioManager>();

        if (audioManager == null) return;

        if (useDungeonMusic)
            audioManager.PlayDungeonMusic();
        else
            audioManager.PlayOtherSceneMusic();
    }
}