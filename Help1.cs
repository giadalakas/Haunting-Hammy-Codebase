using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Help1 : MonoBehaviour
{
    public GameObject uiObject;

    private void Start()
    {
        // Ensure UI object is initially disabled
        uiObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Activate the UI object
            uiObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
    if (other.CompareTag("Player"))
        {
            // Check if the uiObject reference is not null before trying to deactivate it
            if (uiObject != null)
            {
                // Deactivate the UI object
                uiObject.SetActive(false);
            }
            else
            {
                Debug.LogWarning("UI object reference is null. Make sure it's assigned in the inspector.");
            }
        }
    }

}
