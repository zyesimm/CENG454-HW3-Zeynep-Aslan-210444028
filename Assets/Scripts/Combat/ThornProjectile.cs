using UnityEngine;

public class ThornProjectile : MonoBehaviour, IPoolable
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float damage = 20f;
    [SerializeField] private float lifeTime = 2f;

    [Header("Visual")]
    [SerializeField] private Transform visual;
   

    private ObjectPool ownerPool;
    private float lifeTimer;
    private Vector2 moveDirection;

    public void Initialize(ObjectPool pool, Vector2 direction)
    {
        ownerPool = pool;
        moveDirection = direction.normalized;
        if (moveDirection != Vector2.zero)
        {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
        }
    }

    private void Update()
    {
        transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);

        lifeTimer -= Time.deltaTime;

        if (lifeTimer <= 0f)
        {
            ReturnToPool();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Enemy>() != null)
        {
            if (other.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(damage);
            }
            
            ReturnToPool();
            return;
        }
        if (other.GetComponent<LifeBlossom>() != null)
        {
            ReturnToPool();
        }
    }

    public void OnSpawned()
    {
        lifeTimer = lifeTime;
    }

    public void OnDespawned()
    {
        lifeTimer = 0f;
        moveDirection = Vector2.zero;
    }

    private void ReturnToPool()
    {
        if (ownerPool != null)
        {
            ownerPool.ReturnObject(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}