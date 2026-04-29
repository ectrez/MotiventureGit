using UnityEngine;

public class GuildPageController : MonoBehaviour
{
    public GameObject questPage;
    public GameObject leaderboardPage;
    public GameObject shopPage;

    private void Start()
    {
        CloseAllPages();
    }

    public void OpenQuestPage()
    {
        CloseAllPages();

        if (questPage != null)
            questPage.SetActive(true);
    }

    public void OpenLeaderboardPage()
    {
        CloseAllPages();

        if (leaderboardPage != null)
            leaderboardPage.SetActive(true);
    }

    public void OpenShopPage()
    {
        CloseAllPages();

        if (shopPage != null)
            shopPage.SetActive(true);
    }

    public void CloseAllPages()
    {
        if (questPage != null)
            questPage.SetActive(false);

        if (leaderboardPage != null)
            leaderboardPage.SetActive(false);

        if (shopPage != null)
            shopPage.SetActive(false);
    }
}