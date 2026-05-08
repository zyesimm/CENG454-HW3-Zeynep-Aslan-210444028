using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealth = 20f;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float damageToCore = 10f;

    private float currentHealth;
    private Transform target;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    private void FixedUpdate()
    {
        if (target == null) return;

        Vector2 direction;

        IMovementStrategy movementStrategy = GetComponent<IMovementStrategy>();

        if (movementStrategy != null)
        {
            direction = movementStrategy.GetMovementDirection(transform.position, target.position);
        }
        else
        {
            direction = (target.position - transform.position).normalized;
        }

        rb.linearVelocity = direction * moveSpeed;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        EnemyEvents.RaiseEnemyDied();
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();

        if (damageable != null && collision.gameObject.GetComponent<LifeBlossom>() != null)
        {
            damageable.TakeDamage(damageToCore);
            Die();
        }
    }
}