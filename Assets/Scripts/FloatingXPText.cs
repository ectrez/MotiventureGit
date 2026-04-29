using UnityEngine;
using TMPro;

public class FloatingXPText : MonoBehaviour
{
    public float moveSpeed = 80f;
    public float lifetime = 0.5f;

    private TextMeshProUGUI text;
    private CanvasGroup canvasGroup;
    private float timer;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();

        canvasGroup = GetComponent<CanvasGroup>();

        if (canvasGroup == null)
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        timer = lifetime;
        canvasGroup.alpha = 1f;

        if (text != null)
            text.text = "+1XP";
    }

    private void Update()
    {
        float delta = Time.deltaTime;

        transform.Translate(Vector3.up * moveSpeed * delta);

        timer -= delta;

        canvasGroup.alpha = timer / lifetime;

        if (timer <= 0f)
            Destroy(gameObject);
    }
}