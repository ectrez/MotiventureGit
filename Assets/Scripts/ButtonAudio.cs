using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonAudio : MonoBehaviour, IPointerEnterHandler
{
    public AudioManager audioManager;

    void Start()
    {
        if (audioManager == null)
            audioManager = FindFirstObjectByType<AudioManager>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (audioManager != null)
            audioManager.PlayButtonHover();
    }

    public void PlayClick()
    {
        if (audioManager != null)
            audioManager.PlayButtonClick();
    }
}