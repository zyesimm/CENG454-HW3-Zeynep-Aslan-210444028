using UnityEngine;

public class SpellCaster : MonoBehaviour
{
    [SerializeField] private ObjectPool projectilePool;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireCooldown = 0.3f;

    private float cooldownTimer;

    private void Update()
    {
        cooldownTimer -= Time.deltaTime;

        if (Input.GetMouseButton(0) && cooldownTimer <= 0f)
        {
            CastThornShot();
            cooldownTimer = fireCooldown;
        }
    }

    private void CastThornShot()
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;

        Vector2 direction = (mouseWorldPosition - firePoint.position).normalized;

        GameObject projectileObject = projectilePool.GetObject(firePoint.position, Quaternion.identity);

        if (projectileObject.TryGetComponent(out ThornProjectile projectile))
        {
            projectile.Initialize(projectilePool, direction);
        }
    }
}