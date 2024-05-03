using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText; // Reference to the timer text
    [SerializeField] TextMeshProUGUI midnightText; // Reference to the "Midnight" text
    [SerializeField] float remainingTime; // Initial remaining time

    public AudioSource losingSoundEffect;

    // Update is called once per frame
    void Update()
    {
        // If there's still time left
        if (remainingTime > 0)
        {
            // Decrease the remaining time by deltaTime
            remainingTime -= Time.deltaTime;

            // If remaining time is less than or equal to 21 seconds, change timer text color
            if (remainingTime <= 21)
            {
                timerText.color = new Color(225f / 255f, 80f / 255f, 61f / 255f); // Change timer text color
            }
        }
        // If time is up
        else if (remainingTime <= 0)
        {
            remainingTime = 0; // Ensure remainingTime is not negative
            timerText.color = new Color(225f / 255f, 80f / 255f, 61f / 255f); // Change timer text color
            timerText.text = "00:00"; // Set timer text to 00:00
            midnightText.color = new Color(225f / 255f, 80f / 255f, 61f / 255f); // Change timer text color
            midnightText.text = "Midnight!"; // Change "Midnight" text


            if (losingSoundEffect != null)
            {
                losingSoundEffect.Play();
            }

            SceneManager.LoadScene("End_Bad"); // Load the "End_Bad" scene
        }

        // Calculate minutes and seconds from remaining time
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);

        // Update the timer text
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}