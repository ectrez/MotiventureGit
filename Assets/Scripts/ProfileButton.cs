using UnityEngine;
using UnityEngine.SceneManagement;

public class ProfileButton : MonoBehaviour
{
    public string profileSceneName = "Profile";

    public void OpenProfile()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(profileSceneName);
    }
}