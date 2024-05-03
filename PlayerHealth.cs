using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private Vector3 checkpointPos;
    public float health;
    public float maxHealth;
    public Animator animator;
    [SerializeField] private AudioSource damageSoundEffect;
    [SerializeField] private Image heart1;
    [SerializeField] private Image heart2;
    [SerializeField] private Image heart3;

    private void Start()
    {
        maxHealth = health;
        checkpointPos = transform.position;
    }

    public void Respawn()
    {
        UpdateUI();
        transform.position = checkpointPos;
    }

    public void TakeDamage()
    {
        health--;
        animator.SetBool("Hurt", true);
        StartCoroutine(PlayCoroutine());

        UpdateUI();

        damageSoundEffect.Play();

        if (health <= 0)
        {
            health = maxHealth;
            Respawn();
        }
    }
    IEnumerator PlayCoroutine()
    {
        yield return new WaitForSeconds(0.25f);
        animator.SetBool("Hurt", false);

    }
    private void UpdateUI()
    {
        heart1.enabled = health >= 1;
        heart2.enabled = health >= 2;
        heart3.enabled = health >= 3;
    }

    public void UpdateCheckpoint(Vector2 pos)
    {
        checkpointPos = pos;
    }
}