using System.Collections;
using UnityEngine;
using TMPro;

public class PlayerExperienceUI : PlayerExperience
{
    public ExperienceBar experienceBar;
    public TMP_Text levelText;

    protected override void Update()
    {
        base.Update(); 
        UpdateLevelText();
        experienceBar.SetMaxExperience(_maxExperience);
        experienceBar.SetExperience(_currentExperience);
    }

    private void UpdateLevelText()
    {
        levelText.text = "" + _currentLevel.ToString();
    }
}
