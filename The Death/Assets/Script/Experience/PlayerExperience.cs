using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class PlayerExperience : MonoBehaviour
{
    [SerializeField]
    int currentExperience, maxExperience = 300, currentLevel = 1;

    public ExperienceBar experienceBar;
    public TMP_Text levelText;

    private void Update()
    {
        UpdateLevelText(); // C?p nh?t n?i dung m?c c?p ban ??u khi b?t ??u
    }

    private void OnEnable()
    {
        ExperienceManager.Instance.OnExperienceChange += HandleExperience;
    }

    private void OnDisable()
    {
        ExperienceManager.Instance.OnExperienceChange -= HandleExperience;
    }



    private void HandleExperience(int newExperience)
    {
        currentExperience += newExperience;
        if (currentExperience >= maxExperience)
        {
            LevelUp();
            experienceBar.SetMaxExperience(maxExperience);
            experienceBar.SetExperience(currentExperience);
        }
        else
        {
            experienceBar.SetExperience(currentExperience); // C?p nh?t thanh trình ?? kinh nghi?m.
        }
    }

    private void LevelUp()
    {
        currentLevel++;
        currentExperience = 0;
        maxExperience += 100;
    }

    private void UpdateLevelText()
    {
        levelText.text = "Level: " + currentLevel.ToString(); // C?p nh?t n?i dung c?a Text
    }
}
