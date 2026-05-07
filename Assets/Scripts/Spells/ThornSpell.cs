using UnityEngine;

public class ThornSpell : ISpell
{
    private readonly ObjectPool projectilePool;

    public ThornSpell(ObjectPool projectilePool)
    {
        this.projectilePool = projectilePool;
    }

    public void Cast(Vector3 origin, Vector2 direction)
    {
        GameObject projectileObject = projectilePool.GetObject(origin, Quaternion.identity);

        if (projectileObject.TryGetComponent(out ThornProjectile projectile))
        {
            projectile.Initialize(projectilePool, direction);
        }
    }
}