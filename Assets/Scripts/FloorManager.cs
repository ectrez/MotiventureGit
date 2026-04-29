using UnityEngine;
using TMPro;

public class FloorManager : MonoBehaviour
{
    public int currentFloor = 1;
    public TextMeshProUGUI floorText;

    void Start()
    {
        UpdateFloorText();
    }

    public void NextFloor()
    {
        currentFloor++;
        UpdateFloorText();
    }

    public int GetFloor()
    {
        return currentFloor;
    }

    void UpdateFloorText()
    {
        if (floorText != null)
            floorText.text = "Floor " + currentFloor.ToString();
    }
}