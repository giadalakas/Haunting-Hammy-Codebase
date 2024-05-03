using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class BossGoo : MonoBehaviour
{
    public GameObject goo;
    public Transform gooPosition;
    private GameObject player;
    public int maxHealth = 120;
    public int currentHealth;
    public float flashDuration = 0.2f;
    public Color flashColor = Color.red;
    public Animator animator;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private float flashTimer;
    public CameraFollow cameraFollow;
    private float timer;

    public AudioSource gooSoundEffect;
    public AudioSource fightMusicSoundEffect;
    public AudioSource bossDeathSoundEffect;
    public AudioSource backgroundMusic;

    public TextMeshProUGUI healthText; // Reference to the TextMeshPro component for displaying health
    public Slider healthSlider; // Reference to the Slider component for the health bar

    private bool isPlayerInRange = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        currentHealth = maxHealth;
        UpdateHealth(); // Initialize health text and health bar
    }

    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance < 15 && !isPlayerInRange)
        {
            isPlayerInRange = true;
            Debug.Log("Music starting");
            fightMusicSoundEffect.Play();

            if (backgroundMusic != null)
            {
                backgroundMusic.Pause();
            }
        }

        if (isPlayerInRange)
        {
            timer += Time.deltaTime;

            if (timer > 2)
            {
                timer = 0;
                shoot();
            }
        }

        if (flashTimer > 0)
        {
            flashTimer -= Time.deltaTime;

            if (flashTimer <= 0)
            {
                animator.SetBool("Hurt", false);
            }
        }

    }

    void shoot()
    {
        Instantiate(goo, gooPosition.position, Quaternion.identity);
        gooSoundEffect.Play();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
            UpdateHealth();
        }
        else
        {
            FlashRed();
            UpdateHealth(); // Update health text and health bar if the boss takes damage
        }

    }

    void Die()
    {
        if (bossDeathSoundEffect != null)
        {
            bossDeathSoundEffect.Play();

            if (fightMusicSoundEffect != null)
            {
                fightMusicSoundEffect.Stop();
            }
        }

        // Move the camera to focus on the boss
        if (cameraFollow != null)
        {
            cameraFollow.SetTarget(transform); // Set the camera's target to the boss's position
        }
        animator.SetBool("Dead", true);
        StartCoroutine(PlayCoroutine());
    }

    IEnumerator PlayCoroutine()
    {
        yield return new WaitForSeconds(1.75f);
        Destroy(gameObject);
        string currentSceneName = SceneManager.GetActiveScene().name;
        if (currentSceneName == "Level 1")
        {
            SceneManager.LoadScene("Transition");
        }
        else if (currentSceneName == "Level 2")
        {
            SceneManager.LoadScene("End_Good");
        }
    }

    void FlashRed()
    {
        animator.SetBool("Hurt", true);
        flashTimer = flashDuration;
    }

    void UpdateHealth()
    {
        // Ensure current health doesn't go below zero for display purposes
        int displayedHealth = Mathf.Max(currentHealth, 0);

        // Update the health text to display the current health value
        if (healthText != null)
        {
            healthText.text = displayedHealth.ToString() + " / " + maxHealth.ToString();
        }

        // Update the value of the health slider based on the current health value
        if (healthSlider != null)
        {
            healthSlider.value = displayedHealth;
            Debug.Log("Current Health: " + displayedHealth + ", Slider Value: " + healthSlider.value);
        }
    }
}