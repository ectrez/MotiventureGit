using UnityEngine;
using TMPro;

public class LeaderboardEntryUI : MonoBehaviour
{
    public TMP_Text rankText;
    public TMP_Text nameText;
    public TMP_Text bpText;

    public void Setup(int rank, string playerName, int bp)
    {
        rankText.text = rank.ToString();
        nameText.text = playerName;
        bpText.text = bp + " BP";
    }
}