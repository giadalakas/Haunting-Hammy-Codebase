using UnityEngine;

public class Warning : MonoBehaviour
{
    public GameObject warningUI;
    public GameObject[] platformsToTrack;
    private bool isPlayerInside = false;
    private bool isMovingPlatformVisible = false;

    void Start()
    {
        if (warningUI != null)
        {
            warningUI.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = true;
            CheckPlatformVisibility();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = false;
            warningUI.SetActive(false);
        }
    }

    void Update()
    {
        if (warningUI == null || !isPlayerInside)
            return;

        UpdateMovingPlatformVisibility();

        if (!isMovingPlatformVisible)
        {
            warningUI.SetActive(true);
        }
        else
        {
            warningUI.SetActive(false);
        }
    }

    void UpdateMovingPlatformVisibility()
    {
        if (platformsToTrack == null || platformsToTrack.Length == 0)
            return;

        foreach (GameObject platform in platformsToTrack)
        {
            if (platform.CompareTag("MovingPlatform") && IsInCameraView(platform))
            {
                isMovingPlatformVisible = true;
                return;
            }
        }

        isMovingPlatformVisible = false;
    }

    bool IsInCameraView(GameObject obj)
    {
        Vector3 viewportPosition = Camera.main.WorldToViewportPoint(obj.transform.position);
        return (viewportPosition.x > 0 && viewportPosition.x < 1 && viewportPosition.y > 0 && viewportPosition.y < 1);
    }

    void CheckPlatformVisibility()
    {
        if (platformsToTrack == null || platformsToTrack.Length == 0 || !isPlayerInside)
            return;

        bool isAnyPlatformInCameraView = false;

        foreach (GameObject platform in platformsToTrack)
        {
            if (IsInCameraView(platform))
            {
                isAnyPlatformInCameraView = true;
                break;
            }
        }

        if (!isAnyPlatformInCameraView && !isMovingPlatformVisible && !warningUI.activeSelf)
        {
            warningUI.SetActive(true);
        }
        else if ((isAnyPlatformInCameraView || isMovingPlatformVisible) && warningUI.activeSelf)
        {
            warningUI.SetActive(false);
        }
    }
}