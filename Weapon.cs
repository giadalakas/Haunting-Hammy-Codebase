using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float shootingZoneRadius = 15f;
    public Animator animator;
    public int fireCount = 0;
    private int maxFireCount = 0; // Maximum number of shots allowed

    public AudioClip throwSound;

    void Start()
    {
        // Check the current level and set maxFireCount accordingly
        string currentSceneName = SceneManager.GetActiveScene().name;
        if (currentSceneName == "Level 1")
        {
            maxFireCount = 16; // Total amount of holy waters in level 1
        }
        else if (currentSceneName == "Level 2")
        {
            maxFireCount = 11; // Total amount of coins in level 2
        }
        else
        {
            Debug.LogError("Unknown level: " + currentSceneName);
        }
    }

    void Update()
    {
        if (IsWithinShootingZone() && IsBossAlive())
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if (HolyManager.Instance.holyCount > 0)
                {
                    animator.SetBool("Throw", true);
                    StartCoroutine(PlayCoroutine());
                }
            }
            else
            {
                animator.SetBool("Throw", false);
            }
        }
    }

    bool IsBossAlive()
    {
        BossGoo boss = FindObjectOfType<BossGoo>();
        return boss != null && boss.currentHealth > 0;
    }

    IEnumerator PlayCoroutine()
    {
        yield return new WaitForSeconds(0.25f);
        if (throwSound != null)
        {
            AudioSource.PlayClipAtPoint(throwSound, transform.position);
        }
        Shoot();
        HolyManager.Instance.holyCount--;

        fireCount++;

        if (fireCount >= maxFireCount)
        {
            BadEnding();
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

    bool IsWithinShootingZone()
    {
        GameObject boss = GameObject.FindGameObjectWithTag("Boss");
        if (boss != null)
        {
            float distanceToBoss = Vector2.Distance(transform.position, boss.transform.position);
            return distanceToBoss <= shootingZoneRadius;
        }
        return false;
    }

    void BadEnding()
    {
        // Check the current level and set maxFireCount accordingly
        string currentSceneName = SceneManager.GetActiveScene().name;
        if (currentSceneName == "Level 1")
        {
            SceneManager.LoadScene("End_NoHoly");
        }
        else if (currentSceneName == "Level 2")
        {
            SceneManager.LoadScene("End_NoCoins");
        }
        else
        {
            Debug.LogError("Unknown level: " + currentSceneName);
        }

    }
}