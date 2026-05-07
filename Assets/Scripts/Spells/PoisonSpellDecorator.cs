using UnityEngine;

public class PoisonSpellDecorator : SpellDecorator
{
    public PoisonSpellDecorator(ISpell wrappedSpell) : base(wrappedSpell)
    {
    }

    public override void Cast(Vector3 origin, Vector2 direction)
    {
        Debug.Log("Poison spell effect applied.");

        base.Cast(origin, direction);
    }
}