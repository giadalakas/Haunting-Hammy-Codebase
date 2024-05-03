using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Clock : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI clockText;
    float currentTime;
    [SerializeField] TextMeshProUGUI pmText; // Reference to the "Midnight" text

    public AudioSource losingSoundEffect;

    bool soundPlayed = false;
    bool midnightReached = false;

    // Start is called before the first frame update
    void Start()
    {
        // Start the clock at 11:58:00
        currentTime = 11 * 3600 + 58 * 60;
        UpdateClockText();
    }

    void Update()
    {
        if (!midnightReached)
        {
            // Update time only if midnight has not been reached
            currentTime += Time.deltaTime;

            // Check if the time has reached 12:00:00
            if (currentTime >= 12 * 3600 && !soundPlayed)
            {
                losingSoundEffect.Play();
                soundPlayed = true; // Set the flag to true to indicate that the sound has been played
            }

            // Delay for a few seconds before loading the scene
            if (soundPlayed && currentTime >= 12 * 3600 + losingSoundEffect.clip.length)
            {
                SceneManager.LoadScene("End_Bad");
            }
        }

        UpdateClockText();
    }


    void UpdateClockText()
    {
        // Calculate hours, minutes, and seconds
        int hours = (int)(currentTime / 3600) % 12;
        int minutes = (int)(currentTime / 60) % 60;
        int seconds = (int)currentTime % 60;

        // Check if it's exactly midnight
        bool isMidnight = (hours == 0 && minutes == 0 && seconds == 0);

        // If it's midnight, display "12:00:00" and "midnight"
        if (isMidnight)
        {
            midnightReached = true; 
            clockText.text = "12:00:00\nMidnight!";
            pmText.text = "";
            StartCoroutine(StayOnScreenForSeconds(4f));
        }
        else
        {
            // Format time as HH:MM:SS
            string timeString = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
            // Update UI Text
            clockText.text = timeString;
        }

        // Check if it's 11:59:40
        if (hours == 11 && minutes == 59 && seconds == 40)
        {
            // Change color to the specified color (225, 80, 61)
            pmText.color = new Color(225f / 255f, 80f / 255f, 61f / 255f);
            clockText.color = new Color(225f / 255f, 80f / 255f, 61f / 255f);
        }
    }

    IEnumerator StayOnScreenForSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene("End_Bad");
    }
}