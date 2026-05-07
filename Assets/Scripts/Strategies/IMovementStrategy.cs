using UnityEngine;

public interface IMovementStrategy
{
    Vector2 GetMovementDirection(Vector2 currentPosition, Vector2 targetPosition);
}