using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PotionSystem : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public PlayerMana playerMana;
    public PlayerCombat playerCombat;
    public AudioManager audioManager;

    public Button potionButton;
    public TextMeshProUGUI potionCountText;

    public int maxPotionsPerRun = 3;
    public int currentPotions;

    void Start()
    {
        if (audioManager == null)
            audioManager = FindFirstObjectByType<AudioManager>();

        ResetPotionsForRun();
    }

    public void ResetPotionsForRun()
    {
        currentPotions = maxPotionsPerRun;
        UpdatePotionUI();
    }

    public void UsePotion()
    {
        if (playerHealth == null) return;
        if (playerMana == null) return;
        if (playerHealth.IsDead()) return;
        if (currentPotions <= 0) return;

        currentPotions--;

        playerHealth.HealThirtyPercent();
        playerMana.RestoreFullMana();

        if (audioManager != null)
            audioManager.PlayPotionUse();

        if (playerCombat != null)
            playerCombat.UsePotionVisual();

        UpdatePotionUI();
    }

    void UpdatePotionUI()
    {
        if (potionCountText != null)
            potionCountText.text = currentPotions.ToString();

        if (potionButton != null)
            potionButton.interactable = currentPotions > 0;
    }
}