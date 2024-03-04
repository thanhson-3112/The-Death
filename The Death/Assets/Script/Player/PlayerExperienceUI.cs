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
        experienceBar.SetMaxExperience(maxExperience);
        experienceBar.SetExperience(currentExperience);
    }

    private void UpdateLevelText()
    {
        levelText.text = "Level: " + currentLevel.ToString();
    }
}
