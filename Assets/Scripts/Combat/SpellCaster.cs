using UnityEngine;

public class SpellCaster : MonoBehaviour
{
    [SerializeField] private ObjectPool projectilePool;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireCooldown = 0.3f;

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

        if (Input.GetMouseButton(0) && cooldownTimer <= 0f)
        {
            CastCurrentSpell();
            cooldownTimer = fireCooldown;
        }
    }

    private void CastCurrentSpell()
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;

        Vector2 direction = (mouseWorldPosition - firePoint.position).normalized;

        currentSpell.Cast(firePoint.position, direction);
    }
}