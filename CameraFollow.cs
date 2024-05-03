using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform playerTarget;
    [SerializeField] private Transform bossTarget;
    [SerializeField] private float smoothSpeed = 0.125f;
    [SerializeField] private Vector3 offset;

    private Transform currentTarget;

    private void Start()
    {
        // Set the initial target to the player
        currentTarget = playerTarget;
    }

    private void LateUpdate()
    {
        if (currentTarget != null)
        {
            Vector3 desiredPosition = currentTarget.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, transform.position.z);
        }
    }

    // Function to set the target to follow
    public void SetTarget(Transform newTarget)
    {
        currentTarget = newTarget;
    }
}
