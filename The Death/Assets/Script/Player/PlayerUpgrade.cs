using System.Collections;
using UnityEngine;

public class PlayerUpgrade : PlayerExperience
{
    [SerializeField] UpgradeManager upgradeManager;
    [SerializeField] PlayerLife PlayerLife;

    private int previousLevel;

    public void Start()
    {
        previousLevel = _currentLevel;
    }

    protected override void Update()
    {
        if (_currentLevel > previousLevel)
        {
            Debug.Log("233" + _currentLevel);
            upgradeManager.Start();
            PlayerLife.Heal();
            previousLevel = _currentLevel;

        }

        base.Update();


    }
}
