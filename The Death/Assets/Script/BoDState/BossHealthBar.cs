using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BossHealthBar : MonoBehaviour
{
    public Slider healthBar;
    public GameObject fillArea;
    public GameObject borderArea;
    [SerializeField] TextMeshProUGUI textName;

    public void Start()
    {
        fillArea.SetActive(false);
        borderArea.SetActive(false);
        textName.text = "";

    }

    public void SetMaxHealth(float health)
    {
        fillArea.SetActive(true);
        borderArea.SetActive(true);
        textName.text = "Bringer Of Death";
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
        textName.text = "";
    }
}
