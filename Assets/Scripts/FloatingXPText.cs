using UnityEngine;
using TMPro;

public class FloatingXPText : MonoBehaviour
{
    public float moveSpeed = 80f;
    public float lifetime = 0.5f;

    private TMP_Text text;
    private CanvasGroup canvasGroup;
    private float timer;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();

        if (text == null)
            text = GetComponentInChildren<TMP_Text>(true);

        canvasGroup = GetComponent<CanvasGroup>();

        if (canvasGroup == null)
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
    }

    public void Setup(string value)
    {
        if (text == null)
            text = GetComponent<TMP_Text>();

        if (text == null)
            text = GetComponentInChildren<TMP_Text>(true);

        if (text != null)
            text.text = value;
        else
            Debug.LogWarning("FloatingXPText could not find a TMP text component on " + gameObject.name);
    }

    private void OnEnable()
    {
        timer = lifetime;

        if (canvasGroup == null)
            canvasGroup = GetComponent<CanvasGroup>();

        if (canvasGroup != null)
            canvasGroup.alpha = 1f;
    }

    private void Update()
    {
        float delta = Time.deltaTime;

        transform.Translate(Vector3.up * moveSpeed * delta);

        timer -= delta;

        if (canvasGroup != null)
            canvasGroup.alpha = timer / lifetime;

        if (timer <= 0f)
            Destroy(gameObject);
    }
}