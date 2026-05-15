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
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);

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
        LifeBlossom lifeBlossom = collision.gameObject.GetComponent<LifeBlossom>();

        if (lifeBlossom != null)
        {
            lifeBlossom.TakeDamage(damageToCore);
            Destroy(gameObject);
        }
    }   
}