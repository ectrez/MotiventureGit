using UnityEngine;
using System.Collections;

public class IntroSceneController : MonoBehaviour
{
    public SpriteRenderer introImage;
    public GameObject tutorialPanel;

    public float introDuration = 2f;
    public float fadeDuration = 1f;

    private bool waitingForClick = false;

    void Start()
    {
        Time.timeScale = 0f;

        introImage.gameObject.SetActive(true);
        tutorialPanel.SetActive(false);

        SetSpriteAlpha(1f);

        StartCoroutine(IntroSequence());
    }

    void Update()
    {
        if (!waitingForClick) return;

        if (Input.GetMouseButtonDown(0))
        {
            StartDungeon();
        }
    }

    private IEnumerator IntroSequence()
    {
        yield return new WaitForSecondsRealtime(introDuration);

        yield return StartCoroutine(FadeOutIntroImage());

        introImage.gameObject.SetActive(false);
        tutorialPanel.SetActive(true);

        waitingForClick = true;
    }

    private void StartDungeon()
    {
        waitingForClick = false;
        tutorialPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    private IEnumerator FadeOutIntroImage()
    {
        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.unscaledDeltaTime;

            float alpha = 1f - (timer / fadeDuration);
            SetSpriteAlpha(alpha);

            yield return null;
        }

        SetSpriteAlpha(0f);
    }

    private void SetSpriteAlpha(float alpha)
    {
        Color colour = introImage.color;
        colour.a = alpha;
        introImage.color = colour;
    }
}