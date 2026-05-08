using System;

public static class EnemyEvents
{
    public static event Action OnEnemyDied;

    public static void RaiseEnemyDied()
    {
        OnEnemyDied?.Invoke();
    }
}