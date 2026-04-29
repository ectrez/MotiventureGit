using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public enum Direction { Left, Right, Overhead }

    [System.Serializable]
    public class DirectionSprites
    {
        public Sprite idle;
        public Sprite swing;
    }

    public SpriteRenderer spriteRenderer;

    public DirectionSprites left;
    public DirectionSprites right;
    public DirectionSprites overhead;

    public Sprite potionUseSprite;
    public Sprite fireballCastSprite;

    public float swingDuration = 0.3f;
    public float potionSpriteDuration = 0.3f;
    public float fireballCastDuration = 0.3f;

    public EnemyCombat enemyCombat;
    public EnemyHealth enemyHealth;
    public PlayerStats playerStats;
    public PlayerHealth playerHealth;
    public PlayerMana playerMana;
    public ScreenHueEffects hueEffects;

    private Direction currentDirection = Direction.Left;

    private bool isSwinging = false;
    private float swingTimer = 0f;

    private bool isUsingPotionVisual = false;
    private float potionVisualTimer = 0f;

    private bool isCastingFireball = false;
    private float fireballCastTimer = 0f;

    void Update()
    {
        HandleInput();
        HandleSwing();
        HandlePotionVisual();
        HandleFireballVisual();
        UpdateVisual();
    }

    void HandleInput()
    {
        if (isSwinging || isUsingPotionVisual || isCastingFireball) return;

        if (Input.GetMouseButton(0))
        {
            float x = Input.mousePosition.x;

            if (x < Screen.width * 0.33f)
                currentDirection = Direction.Left;
            else if (x > Screen.width * 0.66f)
                currentDirection = Direction.Right;
            else
                currentDirection = Direction.Overhead;
        }

        if (Input.GetMouseButtonUp(0))
        {
            StartSwing();
            TryParry();
            TryDamageEnemy();
        }
    }

    void StartSwing()
    {
        isSwinging = true;
        swingTimer = swingDuration;
    }

    public void UsePotionVisual()
    {
        if (isSwinging || isCastingFireball || isUsingPotionVisual) return;

        isUsingPotionVisual = true;
        potionVisualTimer = potionSpriteDuration;

        if (hueEffects != null)
            hueEffects.PlayPotionHue();
    }

    public void CastFireball()
    {
        if (isSwinging || isUsingPotionVisual || isCastingFireball) return;
        if (playerMana == null) return;
        if (enemyHealth == null) return;
        if (enemyCombat != null && enemyCombat.IsDead()) return;

        if (!playerMana.TrySpendMana(5))
            return;

        isCastingFireball = true;
        fireballCastTimer = fireballCastDuration;

        enemyHealth.TakeDamage(15);
    }

    void TryParry()
    {
        if (enemyCombat == null) return;
        if (enemyCombat.IsDead()) return;
        if (!enemyCombat.IsSwinging()) return;

        if ((int)currentDirection == (int)enemyCombat.GetDirection())
            enemyCombat.Parry();
    }

    void TryDamageEnemy()
    {
        if (enemyHealth == null) return;
        if (enemyCombat != null && enemyCombat.IsDead()) return;
        if (playerStats == null) return;

        enemyHealth.TakeDamage(playerStats.GetSwordDamage());
    }

    void HandleSwing()
    {
        if (!isSwinging) return;

        swingTimer -= Time.deltaTime;

        if (swingTimer <= 0f)
            isSwinging = false;
    }

    void HandlePotionVisual()
    {
        if (!isUsingPotionVisual) return;

        potionVisualTimer -= Time.deltaTime;

        if (potionVisualTimer <= 0f)
            isUsingPotionVisual = false;
    }

    void HandleFireballVisual()
    {
        if (!isCastingFireball) return;

        fireballCastTimer -= Time.deltaTime;

        if (fireballCastTimer <= 0f)
            isCastingFireball = false;
    }

    void UpdateVisual()
    {
        if (isUsingPotionVisual)
        {
            spriteRenderer.sprite = potionUseSprite;
            return;
        }

        if (isCastingFireball)
        {
            spriteRenderer.sprite = fireballCastSprite;
            return;
        }

        DirectionSprites sprites = GetSprites();

        if (isSwinging)
            spriteRenderer.sprite = sprites.swing;
        else
            spriteRenderer.sprite = sprites.idle;
    }

    DirectionSprites GetSprites()
    {
        switch (currentDirection)
        {
            case Direction.Left: return left;
            case Direction.Right: return right;
            case Direction.Overhead: return overhead;
        }

        return left;
    }
}