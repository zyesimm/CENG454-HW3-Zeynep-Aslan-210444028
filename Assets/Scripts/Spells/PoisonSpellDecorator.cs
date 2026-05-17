using UnityEngine;

public class PoisonSpellDecorator : SpellDecorator
{
    private readonly float spreadAngle = 10f;

    public PoisonSpellDecorator(ISpell wrappedSpell) : base(wrappedSpell)
    {
    }

    public override void Cast(Vector3 origin, Vector2 direction)
    {
        base.Cast(origin, direction);

        Vector2 spreadDirection = Quaternion.Euler(0f, 0f, spreadAngle) * direction;
        wrappedSpell.Cast(origin, spreadDirection.normalized);
    }
}