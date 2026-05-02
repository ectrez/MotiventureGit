using UnityEngine;
using System.Collections.Generic;

public class LeaderboardController : MonoBehaviour
{
    public PlayerStats playerStats;
    public Transform contentParent;
    public GameObject entryPrefab;

    private class Entry
    {
        public string name;
        public int bp;

        public Entry(string n, int b)
        {
            name = n;
            bp = b;
        }
    }

    void OnEnable()
    {
        Build();
    }

    void Build()
    {
        foreach (Transform child in contentParent)
            Destroy(child.gameObject);

        playerStats.LoadStats();

        List<Entry> entries = new List<Entry>
        {
            new Entry("Grandmaster Rowan", 300),
            new Entry("Crimson Darius", 235),
            new Entry("Storm Commander", 230),
            new Entry("Golden Valen", 225),
            new Entry("Frost Ronan", 210),
            new Entry("Iron Valkyrie", 200),
            new Entry("Silver Cedric", 185),
            new Entry("Crimson Knight", 170),
            new Entry("Storm Alric", 155),
            new Entry("Ashen Blade", 130),
            new Entry("Frost Sentinel", 120),
            new Entry("Blue Falcon", 110),
            new Entry("Ember Guard", 100),
            new Entry("Grim Squire", 90),
            new Entry("Wandering Spear", 80),
            new Entry("Rogue Halberd", 65),
            new Entry("Iron Initiate", 60),
            new Entry("Rookie Ember", 55),
            new Entry("Brave Civilian", 50),
            new Entry("Village Guard", 45),
            new Entry("Lost Traveller", 42),
            new Entry("You", playerStats.GetBP())
        };

        entries.Sort((a, b) => b.bp.CompareTo(a.bp));

        for (int i = 0; i < entries.Count; i++)
        {
            GameObject obj = Instantiate(entryPrefab, contentParent);
            obj.GetComponent<LeaderboardEntryUI>().Setup(i + 1, entries[i].name, entries[i].bp);
        }
    }
}