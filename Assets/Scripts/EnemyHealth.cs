using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public FloorManager floorManager;
    public EnemyCombat enemyCombat;
    public Transform fillTransform;
    public PlayerStats playerStats;

    [Header("Floating XP Text")]
    public GameObject xpTextPrefab;
    public Transform xpTextSpawnPoint;
    public Transform canvasTransform;

    public int baseMaxHealth = 10;

    public int maxHealth;
    public int currentHealth;

    public Vector3 fullHealthPosition;
    public Vector3 zeroHealthPosition;

    private bool xpGiven = false;

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
        {
            GiveXP();
            enemyCombat.Die();
        }
    }

    private void GiveXP()
    {
        if (xpGiven) return;

        xpGiven = true;

        if (playerStats != null)
            playerStats.AddXP(1);

        SpawnXPText();
    }

    private void SpawnXPText()
    {
        if (xpTextPrefab == null) return;
        if (canvasTransform == null) return;

        GameObject obj = Instantiate(xpTextPrefab, canvasTransform);

        if (xpTextSpawnPoint != null)
        {
            obj.transform.position = xpTextSpawnPoint.position;
        }
        else
        {
            obj.transform.position = transform.position;
        }

        obj.SetActive(true);
    }

    public void ResetForCurrentFloor()
    {
        xpGiven = false;

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