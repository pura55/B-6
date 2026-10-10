using UnityEngine;

public class WaterPushLimiter : MonoBehaviour
{
    [SerializeField] private float maxPushSpeed = 3f;

    private Vector2 totalPush;
    private int lastPushFrame = -1;

    public void AddPush(Vector2 moveAmount)
    {
        if (lastPushFrame != Time.frameCount)
        {
            totalPush = Vector2.zero;
            lastPushFrame = Time.frameCount;
        }

        totalPush += moveAmount;
    }

    private void LateUpdate()
    {
        if (lastPushFrame != Time.frameCount)
            return;

        Vector2 limitedPush = Vector2.ClampMagnitude(
            totalPush,
            maxPushSpeed * Time.deltaTime
        );

        transform.position += (Vector3)limitedPush;
    }
}