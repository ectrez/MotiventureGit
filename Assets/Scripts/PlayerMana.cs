using UnityEngine;

public class PlayerMana : MonoBehaviour
{
    public PlayerStats stats;
    public Transform fillTransform;

    public int maxMana;
    public int currentMana;

    public Vector3 fullManaPosition;
    public Vector3 zeroManaPosition;

    void Start()
    {
        ResetToFullMana();
    }

    public bool CanCastFireball()
    {
        return currentMana >= 5;
    }

    public bool TrySpendMana(int amount)
    {
        if (currentMana < amount)
            return false;

        currentMana -= amount;
        UpdateBar();
        return true;
    }

    public void RestoreFullMana()
    {
        currentMana = maxMana;
        UpdateBar();
    }

    public void ResetToFullMana()
    {
        maxMana = stats.GetMaxMana();
        currentMana = maxMana;
        UpdateBar();
    }

    void UpdateBar()
    {
        float percent = (float)currentMana / maxMana;
        fillTransform.localPosition = Vector3.Lerp(zeroManaPosition, fullManaPosition, percent);
    }
}