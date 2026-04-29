using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public enum Direction { Left, Right, Overhead }
    public enum State { Idle, Swing, Hit, Parried, Dead }

    [System.Serializable]
    public class DirectionSprites
    {
        public Sprite idle;
        public Sprite swing;
        public Sprite hit;
        public Sprite parried;
        public Sprite dead;
    }

    public SpriteRenderer spriteRenderer;

    public DirectionSprites left;
    public DirectionSprites right;
    public DirectionSprites overhead;

    public float decisionInterval = 0.5f;
    public float swingDuration = 0.5f;
    public float hitDuration = 0.5f;
    public float parryDuration = 0.5f;
    public float respawnDelay = 3f;

    public FloorManager floorManager;
    public PlayerHealth playerHealth;
    public EnemyHealth enemyHealth;

    public int damageOnHit;

    private float decisionTimer;
    private State currentState = State.Idle;
    private Direction currentDirection = Direction.Left;
    private float stateTimer;
    private float respawnTimer;

    void Start()
    {
        SetupDamageFromFloor();
        decisionTimer = decisionInterval;
        UpdateVisual();
    }

    void Update()
    {
        HandleDecision();
        HandleState();
        HandleRespawn();
        UpdateVisual();
    }

    void SetupDamageFromFloor()
    {
        int floor = 1;

        if (floorManager != null)
            floor = floorManager.GetFloor();

        damageOnHit = 2 + (floor - 1);
    }

    void HandleDecision()
    {
        if (currentState != State.Idle) return;

        decisionTimer -= Time.deltaTime;

        if (decisionTimer <= 0f)
        {
            decisionTimer = decisionInterval;

            if (Random.value < 0.5f)
                StartSwing();
            else
                PickNewIdleDirection();
        }
    }

    void PickNewIdleDirection()
    {
        Direction newDirection = currentDirection;

        while (newDirection == currentDirection)
            newDirection = (Direction)Random.Range(0, 3);

        currentDirection = newDirection;
    }

    void StartSwing()
    {
        if (currentState == State.Dead) return;

        currentState = State.Swing;
        stateTimer = swingDuration;
    }

    void StartHit()
    {
        if (currentState == State.Dead) return;

        currentState = State.Hit;
        stateTimer = hitDuration;

        if (playerHealth != null)
            playerHealth.TakeDamage(damageOnHit);
    }

    public void Parry()
    {
        if (currentState != State.Swing) return;

        currentState = State.Parried;
        stateTimer = parryDuration;
    }

    public void Die()
    {
        if (currentState == State.Dead) return;

        currentState = State.Dead;
        respawnTimer = respawnDelay;
    }

    void HandleRespawn()
    {
        if (currentState != State.Dead) return;

        respawnTimer -= Time.deltaTime;

        if (respawnTimer <= 0f)
        {
            if (floorManager != null)
                floorManager.NextFloor();

            SetupDamageFromFloor();

            if (enemyHealth != null)
                enemyHealth.ResetForCurrentFloor();

            currentState = State.Idle;
            decisionTimer = decisionInterval;
            currentDirection = Direction.Left;
        }
    }

    void HandleState()
    {
        if (currentState == State.Swing)
        {
            stateTimer -= Time.deltaTime;

            if (stateTimer <= 0f)
                StartHit();
        }
        else if (currentState == State.Hit)
        {
            stateTimer -= Time.deltaTime;

            if (stateTimer <= 0f)
                currentState = State.Idle;
        }
        else if (currentState == State.Parried)
        {
            stateTimer -= Time.deltaTime;

            if (stateTimer <= 0f)
                currentState = State.Idle;
        }
    }

    void UpdateVisual()
    {
        DirectionSprites sprites = GetSprites();

        switch (currentState)
        {
            case State.Idle:
                spriteRenderer.sprite = sprites.idle;
                break;
            case State.Swing:
                spriteRenderer.sprite = sprites.swing;
                break;
            case State.Hit:
                spriteRenderer.sprite = sprites.hit;
                break;
            case State.Parried:
                spriteRenderer.sprite = sprites.parried;
                break;
            case State.Dead:
                spriteRenderer.sprite = sprites.dead;
                break;
        }
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

    public bool IsSwinging()
    {
        return currentState == State.Swing;
    }

    public Direction GetDirection()
    {
        return currentDirection;
    }

    public bool IsDead()
    {
        return currentState == State.Dead;
    }
}