using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueStart : MonoBehaviour
{
    public GameObject uiObject;
    private bool dialogueTriggered = false;

    private void Start()
    {
        // Ensure UI object is initially disabled
        uiObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !dialogueTriggered)
        {
            // Activate the UI object
            uiObject.SetActive(true);
            // Set the flag to true to indicate that the dialogue has been triggered
            dialogueTriggered = true;
        }
    }
}
