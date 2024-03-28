using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashBar : MonoBehaviour
{
    public Slider dashBar;
    public GameObject fillArea;
    public GameObject borderArea;

    public void SetMaxDash(float dash)
    {
        fillArea.SetActive(true);
        borderArea.SetActive(true);
        dashBar.maxValue = dash;
        dashBar.value = dash;
    }

    public void SetDash(float dash)
    {
        dashBar.value = dash;
    }

/*    public void SetHealthBar()
    {
        fillArea.SetActive(false);
        borderArea.SetActive(false);
    }*/
}
