using UnityEngine;

public abstract class SpellDecorator : ISpell
{
    protected readonly ISpell wrappedSpell;

    protected SpellDecorator(ISpell wrappedSpell)
    {
        this.wrappedSpell = wrappedSpell;
    }

    public virtual void Cast(Vector3 origin, Vector2 direction)
    {
        wrappedSpell.Cast(origin, direction);
    }
}