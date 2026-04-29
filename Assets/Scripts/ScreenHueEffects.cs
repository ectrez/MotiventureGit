using UnityEngine;

public class ScreenHueEffects : MonoBehaviour
{
    public SpriteRenderer potionHueRenderer;
    public SpriteRenderer damageHueRenderer;

    public float fadeInDuration = 0.1f;
    public float fadeOutDuration = 0.25f;
    public float maxAlpha = 0.6f;

    private bool potionPlaying = false;
    private bool damagePlaying = false;

    private float potionTimer = 0f;
    private float damageTimer = 0f;

    void Update()
    {
        UpdatePotionHue();
        UpdateDamageHue();
    }

    public void PlayPotionHue()
    {
        potionPlaying = true;
        potionTimer = 0f;
        SetAlpha(potionHueRenderer, 0f);
    }

    public void PlayDamageHue()
    {
        damagePlaying = true;
        damageTimer = 0f;
        SetAlpha(damageHueRenderer, 0f);
    }

    void UpdatePotionHue()
    {
        if (!potionPlaying || potionHueRenderer == null) return;

        potionTimer += Time.deltaTime;
        float total = fadeInDuration + fadeOutDuration;

        if (potionTimer <= fadeInDuration)
        {
            float t = potionTimer / fadeInDuration;
            SetAlpha(potionHueRenderer, Mathf.Lerp(0f, maxAlpha, t));
        }
        else if (potionTimer <= total)
        {
            float t = (potionTimer - fadeInDuration) / fadeOutDuration;
            SetAlpha(potionHueRenderer, Mathf.Lerp(maxAlpha, 0f, t));
        }
        else
        {
            SetAlpha(potionHueRenderer, 0f);
            potionPlaying = false;
        }
    }

    void UpdateDamageHue()
    {
        if (!damagePlaying || damageHueRenderer == null) return;

        damageTimer += Time.deltaTime;
        float total = fadeInDuration + fadeOutDuration;

        if (damageTimer <= fadeInDuration)
        {
            float t = damageTimer / fadeInDuration;
            SetAlpha(damageHueRenderer, Mathf.Lerp(0f, maxAlpha, t));
        }
        else if (damageTimer <= total)
        {
            float t = (damageTimer - fadeInDuration) / fadeOutDuration;
            SetAlpha(damageHueRenderer, Mathf.Lerp(maxAlpha, 0f, t));
        }
        else
        {
            SetAlpha(damageHueRenderer, 0f);
            damagePlaying = false;
        }
    }

    void SetAlpha(SpriteRenderer renderer, float alpha)
    {
        if (renderer == null) return;

        Color c = renderer.color;
        c.a = alpha;
        renderer.color = c;
    }
}