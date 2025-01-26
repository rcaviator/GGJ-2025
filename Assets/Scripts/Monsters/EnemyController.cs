using GGJ2025.Utilities;
using UnityEngine;

// Base state class that all enemy states will inherit from
public abstract class EnemyState
{
    protected EnemyController enemy;
    protected Transform player;

    public EnemyState(EnemyController enemy, Transform player)
    {
        this.enemy = enemy;
        this.player = player;
    }

    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();
}

// Idle state - enemy is stationary until player enters detection range
public class IdleState : EnemyState
{
    public IdleState(EnemyController enemy, Transform player) : base(enemy, player) { }

    public override void EnterState()
    {
        // Play idle animation
        enemy.animator.SetTrigger("Idle");
    }

    public override void UpdateState()
    {
        // Check if player is within detection range
        float distanceToPlayer = Vector2.Distance(enemy.transform.position, player.position);
        if (distanceToPlayer <= enemy.detectionRange)
        {
            enemy.ChangeState(new ChaseState(enemy, player));
        }
    }

    public override void ExitState()
    {
        // Clean up any idle state specific things
    }
}

// Chase state - enemy follows the player
public class ChaseState : EnemyState
{
    public ChaseState(EnemyController enemy, Transform player) : base(enemy, player) { }

    public override void EnterState()
    {
        // Start movement animation
        enemy.animator.SetBool("IsMoving", true);
    }

    public override void UpdateState()
    {
        // Move towards player
        Vector2 direction = (player.position - enemy.transform.position).normalized;
        enemy.rb.linearVelocity = direction * enemy.moveSpeed;

        // Update animation based on movement direction
        UpdateAnimationDirection(direction);

        // Check if within attack range
        float distanceToPlayer = Vector2.Distance(enemy.transform.position, player.position);
        if (distanceToPlayer <= enemy.attackRange)
        {
            enemy.ChangeState(new AttackState(enemy, player));
        }
    }

    private void UpdateAnimationDirection(Vector2 direction)
    {
        // Set animation parameters based on movement direction
        enemy.animator.SetFloat("Horizontal", direction.x);
        enemy.animator.SetFloat("Vertical", direction.y);
    }

    public override void ExitState()
    {
        enemy.animator.SetBool("IsMoving", false);
        enemy.rb.linearVelocity = Vector2.zero;
    }
}

// Attack state - enemy performs attack when close to player
public class AttackState : EnemyState
{
    private float attackTimer;

    public AttackState(EnemyController enemy, Transform player) : base(enemy, player) { }

    public override void EnterState()
    {
        attackTimer = 0f;
        // Trigger attack animation
        enemy.animator.SetTrigger("Attack");
    }

    public override void UpdateState()
    {
        attackTimer += Time.deltaTime;

        // After attack animation/cooldown, check distance
        if (attackTimer >= enemy.attackDuration)
        {
            float distanceToPlayer = Vector2.Distance(enemy.transform.position, player.position);

            // Reset the attack trigger to allow movement animations to play
            enemy.animator.ResetTrigger("Attack");

            if (distanceToPlayer > enemy.attackRange)
            {
                enemy.animator.SetBool("IsMoving", true);
                enemy.ChangeState(new ChaseState(enemy, player));
            }
            else
            {
                // Reset attack
                attackTimer = 0f;
                enemy.animator.SetTrigger("Attack");
            }
        }
    }

    public override void ExitState()
    {
        // Clean up any attack state specific things
    }
}

// Main enemy controller that manages states and contains enemy properties
public class EnemyController : MonoBehaviour
{
    [Header("Components")]
    public Animator animator;
    public Rigidbody2D rb;
    [SerializeField] private Health health;

    [Header("Health Settings")]
    public float maxHealth = 100f;    // How much health the enemy starts with

    [Header("Detection Settings")]
    public float detectionRange = 5f;
    public float attackRange = 1f;
    
    [Header("Movement Settings")]
    public float moveSpeed = 3f;
    
    [Header("Attack Settings")]
    public float attackDuration = 1f;
    public float attackDamage = 20f;

    private Health playerHealth;

    private EnemyState currentState;
    private Transform player;

    void Start()
    {
        // Get required components
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        if (health == null)
        {
            Debug.LogError("Health component missing on enemy!");
            return;
        }
        
        // Find the player
        player = GameObject.FindGameObjectWithTag("Player").transform;
        
        // Get the player's health component:
        playerHealth = player.GetComponent<Health>();

        // Start in idle state
        ChangeState(new IdleState(this, player));
    }

    public void OnHealthUpdated(Health health)
    {
        Debug.Log($"Enemy Health Changed: {health.Current}/{health.Max}");
    }

    public void OnHealthEmpty(Health health)
    {
        Debug.Log("Enemy has died!");
        enabled = false;
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
        }
        Destroy(gameObject);
    }

    public void DealDamage()
    {
        // Check if we're in range before dealing damage
        if (Vector2.Distance(transform.position, player.position) <= attackRange)
        {
            if (playerHealth != null)
            {
                // Check if we're in range before dealing damage
                if (Vector2.Distance(transform.position, player.position) <= attackRange)
                {
                    if (playerHealth != null)
                    {
                        // Deal damage by reducing current health
                        playerHealth.Current -= attackDamage;
                    }
                }

                // Optional: Spawn hit effect at the point of impact
                Vector2 hitPoint = (transform.position + player.position) * 0.5f;
                
                // Optional: Play hit sound effect
                // AudioManager.Instance.PlayGamePlaySoundEffect(GameSoundEffect.EnemyHit);
            }
        }
    }

    void Update()
    {
        if (currentState != null)
        {
            currentState.UpdateState();
        }
    }

    public void ChangeState(EnemyState newState)
    {
        if (currentState != null)
        {
            currentState.ExitState();
        }

        currentState = newState;
        currentState.EnterState();
    }
}