using UnityEngine;

public class EnemyRewardSpawner : MonoBehaviour
{
    public GameObject xpPrefab;
    public GameObject goldPrefab;

    public RectTransform spawnArea;
    public Transform canvasTransform;

    public void SpawnRewards()
    {
        SpawnXP();
        SpawnGold();
    }

    void SpawnXP()
    {
        GameObject obj = Instantiate(xpPrefab, canvasTransform);
        obj.transform.position = GetRandomPosition();

        obj.GetComponent<FloatingXPText>().Setup("+1XP");

        obj.SetActive(true);
    }

    void SpawnGold()
    {
        int gold = Random.Range(1, 4);

        GameObject obj = Instantiate(goldPrefab, canvasTransform);
        obj.transform.position = GetRandomPosition();

        obj.GetComponent<FloatingXPText>().Setup("+   " + gold + " Gold");

        obj.SetActive(true);
    }

    Vector3 GetRandomPosition()
    {
        Vector3[] corners = new Vector3[4];
        spawnArea.GetWorldCorners(corners);

        float x = Random.Range(corners[0].x, corners[2].x);
        float y = Random.Range(corners[0].y, corners[2].y);

        return new Vector3(x, y, 0f);
    }
}