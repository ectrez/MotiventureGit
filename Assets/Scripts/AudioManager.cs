using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Combat SFX")]
    public AudioClip swordSwing;
    public AudioClip enemyHurt;
    public AudioClip enemyDead;
    public AudioClip playerHurt;
    public AudioClip parry;
    public AudioClip fireball;
    public AudioClip potionUse;

    [Header("UI SFX")]
    public AudioClip buttonHover;
    public AudioClip buttonClick;

    [Header("Progression SFX")]
    public AudioClip rankUp;

    [Header("Music")]
    public AudioClip dungeonMusic;
    public AudioClip otherSceneMusic;

    public void PlaySwordSwing() { PlaySFX(swordSwing); }
    public void PlayEnemyHurt() { PlaySFX(enemyHurt); }
    public void PlayEnemyDead() { PlaySFX(enemyDead); }
    public void PlayPlayerHurt() { PlaySFX(playerHurt); }
    public void PlayParry() { PlaySFX(parry); }
    public void PlayFireball() { PlaySFX(fireball); }
    public void PlayPotionUse() { PlaySFX(potionUse); }
    public void PlayButtonHover() { PlaySFX(buttonHover); }
    public void PlayButtonClick() { PlaySFX(buttonClick); }
    public void PlayRankUp() { PlaySFX(rankUp); }

    public void PlayDungeonMusic()
    {
        PlayMusic(dungeonMusic);
    }

    public void PlayOtherSceneMusic()
    {
        PlayMusic(otherSceneMusic);
    }

    private void PlaySFX(AudioClip clip)
    {
        if (sfxSource == null || clip == null) return;
        sfxSource.PlayOneShot(clip);
    }

    private void PlayMusic(AudioClip clip)
    {
        if (musicSource == null || clip == null) return;

        if (musicSource.clip == clip && musicSource.isPlaying) return;

        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }
}