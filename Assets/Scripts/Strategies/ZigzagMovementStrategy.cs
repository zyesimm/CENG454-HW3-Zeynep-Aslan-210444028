using UnityEngine;

public class ZigzagMovementStrategy : MonoBehaviour, IMovementStrategy
{
    [SerializeField] private float zigzagFrequency = 4f;
    [SerializeField] private float zigzagStrength = 0.6f;

    public Vector2 GetMovementDirection(Vector2 currentPosition, Vector2 targetPosition)
    {
        Vector2 directDirection = (targetPosition - currentPosition).normalized;
        Vector2 perpendicular = new Vector2(-directDirection.y, directDirection.x);

        float zigzagOffset = Mathf.Sin(Time.time * zigzagFrequency) * zigzagStrength;

        return (directDirection + perpendicular * zigzagOffset).normalized;
    }
}