using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class HolyManager : MonoBehaviour
{

    public static HolyManager Instance;

    public int holyCount;
    public int maxHolyCount = 10;
    public TextMeshProUGUI holyText;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("Multiple instances of HolyManager found!");
            Destroy(gameObject);
        }
    }

    void Update()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        holyText.text = holyCount.ToString()+ "/" + maxHolyCount.ToString();
    }
}