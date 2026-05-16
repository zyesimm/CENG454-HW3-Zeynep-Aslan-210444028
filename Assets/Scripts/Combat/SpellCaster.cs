using UnityEngine;

public class SpellCaster : MonoBehaviour
{
    [SerializeField] private ObjectPool projectilePool;
    [SerializeField] private Transform firePoint;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private float fireCooldown = 0.3f;
    [SerializeField] private float targetRange = 8f;

    private float cooldownTimer;
    private ISpell currentSpell;

    private void Awake()
    {
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
            return ((Vector2)closestEnemy.transform.position - (Vector2)firePoint.position).normalized;
        }

        return playerController.FacingDirection.normalized;
    }

    private Enemy FindClosestEnemy()
    {
        Enemy[] enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);

        Enemy closestEnemy = null;
        float closestDistance = targetRange;

        foreach (Enemy enemy in enemies)
        {
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