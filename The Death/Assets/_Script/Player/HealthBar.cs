using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthBar;
    public GameObject fillArea;
    public GameObject borderArea;

    public void SetMaxHealth(float health)
    {
        fillArea.SetActive(true);
        borderArea.SetActive(true);
        healthBar.maxValue = health;
        healthBar.value = health;
    }

    public void SetHealth(float health)
    {
        healthBar.value = health;
    }

    public void SetHealthBar()
    {
        fillArea.SetActive(false);
        borderArea.SetActive(false);
    }
}
