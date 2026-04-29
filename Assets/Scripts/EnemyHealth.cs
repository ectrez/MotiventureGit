using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public FloorManager floorManager;
    public EnemyCombat enemyCombat;
    public Transform fillTransform;

    public int baseMaxHealth = 10;

    public int maxHealth;
    public int currentHealth;

    public Vector3 fullHealthPosition;
    public Vector3 zeroHealthPosition;

    void Start()
    {
        ResetForCurrentFloor();
    }

    public void TakeDamage(int amount)
    {
        if (enemyCombat == null) return;
        if (enemyCombat.IsDead()) return;

        currentHealth -= amount;

        if (currentHealth < 0)
            currentHealth = 0;

        UpdateBar();

        if (currentHealth == 0)
            enemyCombat.Die();
    }

    public void ResetForCurrentFloor()
    {
        int floor = 1;

        if (floorManager != null)
            floor = floorManager.GetFloor();

        maxHealth = baseMaxHealth + ((floor - 1) * 2);
        currentHealth = maxHealth;
        UpdateBar();
    }

    void UpdateBar()
    {
        float percent = (float)currentHealth / maxHealth;
        fillTransform.localPosition = Vector3.Lerp(zeroHealthPosition, fullHealthPosition, percent);
    }
}