using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceBar : MonoBehaviour
{
    public Slider experienceBar;
    public GameObject fillArea;
    public GameObject borderArea;

    public void SetMaxExperience(int experience)
    {
        fillArea.SetActive(true);
        borderArea.SetActive(true);
        experienceBar.maxValue = experience;
        experienceBar.value = 0; 
    }

    public void SetExperience(int experience)
    {
        experienceBar.value = experience;
    }
}
