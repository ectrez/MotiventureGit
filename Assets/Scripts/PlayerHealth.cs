using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public PlayerStats stats;
    public Transform fillTransform;
    public ScreenHueEffects hueEffects;

    public int maxHealth;
    public int currentHealth;

    public Vector3 fullHealthPosition = new Vector3(0f, -2.51999998f, 0f);
    public Vector3 zeroHealthPosition = new Vector3(-4.8f, -2.51999998f, 0f);

    private bool isDead = false;

    void Start()
    {
        ResetToFullHealth();
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return;

        currentHealth -= amount;

        if (currentHealth < 0)
            currentHealth = 0;

        UpdateBar();

        if (hueEffects != null)
            hueEffects.PlayDamageHue();

        if (currentHealth <= 0)
            isDead = true;
    }

    public void HealThirtyPercent()
    {
        if (isDead) return;

        int healAmount = Mathf.RoundToInt(maxHealth * 0.3f);

        currentHealth += healAmount;

        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        UpdateBar();
    }

    public void ResetToFullHealth()
    {
        maxHealth = stats.GetMaxHealth();
        currentHealth = maxHealth;
        isDead = false;
        UpdateBar();
    }

    void UpdateBar()
    {
        float percent = (float)currentHealth / maxHealth;
        fillTransform.localPosition = Vector3.Lerp(zeroHealthPosition, fullHealthPosition, percent);
    }

    public bool IsDead()
    {
        return isDead;
    }
}