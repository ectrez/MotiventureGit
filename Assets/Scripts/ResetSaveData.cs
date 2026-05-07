using UnityEngine;

public class ResetSaveData : MonoBehaviour
{
    void Start()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();

        Debug.Log("All save data deleted.");
    }
}