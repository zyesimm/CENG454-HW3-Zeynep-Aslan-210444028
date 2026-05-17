using UnityEngine;

public class SpellCaster : MonoBehaviour
{
    [SerializeField] private ObjectPool projectilePool;
    [SerializeField] private Transform firePoint;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private float fireCooldown = 0.3f;
    [SerializeField] private float targetRange = 10f;

    private float cooldownTimer;
    private ISpell currentSpell;

    private void Awake()
    {
        if (playerController == null)
        {
            playerController = GetComponent<PlayerController>();
        }

        if (firePoint == null)
        {
            firePoint = transform;
        }

        currentSpell = new PoisonSpellDecorator(
            new ThornSpell(projectilePool)
        );
    }

    private void Update()
    {
        cooldownTimer -= Time.deltaTime;

        if (Input.GetKey(KeyCode.Space) && cooldownTimer <= 0f)
        {
            CastCurrentSpell();
            cooldownTimer = fireCooldown;
        }
    }

    private void CastCurrentSpell()
    {
        Vector2 direction = GetAimDirection();
        currentSpell.Cast(firePoint.position, direction);
    }

    private Vector2 GetAimDirection()
    {
        Enemy closestEnemy = FindClosestEnemy();

        if (closestEnemy != null)
        {
            Vector2 targetDirection =
                ((Vector2)closestEnemy.transform.position - (Vector2)firePoint.position).normalized;

            if (targetDirection != Vector2.zero)
            {
                return targetDirection;
            }
        }

        if (playerController != null && playerController.FacingDirection != Vector2.zero)
        {
            return playerController.FacingDirection.normalized;
        }

        return Vector2.right;
    }

    private Enemy FindClosestEnemy()
    {
        Enemy[] enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);

        Enemy closestEnemy = null;
        float closestDistance = targetRange;

        foreach (Enemy enemy in enemies)
        {
            if (enemy == null || !enemy.gameObject.activeInHierarchy) continue;

            float distance = Vector2.Distance(firePoint.position, enemy.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }

        return closestEnemy;
    }
}