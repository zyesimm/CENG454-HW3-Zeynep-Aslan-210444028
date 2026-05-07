using UnityEngine;

public class DirectMovementStrategy : MonoBehaviour, IMovementStrategy
{
    public Vector2 GetMovementDirection(Vector2 currentPosition, Vector2 targetPosition)
    {
        return (targetPosition - currentPosition).normalized;
    }
}