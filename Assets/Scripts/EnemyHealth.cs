using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public FloorManager floorManager;
    public EnemyCombat enemyCombat;
    public Transform fillTransform;
    public PlayerStats playerStats;

    [Header("Death Rewards")]
    public EnemyRewardSpawner rewardSpawner;
    public int xpReward = 1;
    public int minGoldReward = 1;
    public int maxGoldReward = 5;

    public int baseMaxHealth = 10;

    public int maxHealth;
    public int currentHealth;

    public Vector3 fullHealthPosition;
    public Vector3 zeroHealthPosition;

    private bool rewardsGiven = false;

    void Start()
    {
        if (playerStats == null)
            playerStats = FindFirstObjectByType<PlayerStats>();

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
        {
            GiveXPAndRewards();
            enemyCombat.Die();
        }
    }

    private void GiveXPAndRewards()
    {
        if (rewardsGiven) return;

        rewardsGiven = true;

        int goldReward = Random.Range(minGoldReward, maxGoldReward + 1);

        if (playerStats != null)
        {
            playerStats.AddXP(xpReward);
            playerStats.AddGold(goldReward);

            Debug.Log("Saved rewards: +" + xpReward + " XP, +" + goldReward + " Gold");
        }
        else
        {
            Debug.LogWarning("EnemyHealth: No PlayerStats found. XP and gold were not saved.");
        }

        if (rewardSpawner != null)
            rewardSpawner.SpawnRewards();
    }

    public void ResetForCurrentFloor()
    {
        rewardsGiven = false;

        int floor = 1;

        if (floorManager != null)
            floor = floorManager.GetFloor();

        maxHealth = baseMaxHealth + ((floor - 1) * 2);
        currentHealth = maxHealth;

        UpdateBar();
    }

    void UpdateBar()
    {
        if (fillTransform == null) return;

        float percent = (float)currentHealth / maxHealth;
        fillTransform.localPosition = Vector3.Lerp(zeroHealthPosition, fullHealthPosition, percent);
    }
}