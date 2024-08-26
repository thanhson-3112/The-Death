using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExperience : MonoBehaviour
{
    private int currentExperience, maxExperience = 1000, currentLevel = 1;

    public int _currentExperience { get => currentExperience; }
    public int _maxExperience { get => maxExperience; }
    public int _currentLevel { get => currentLevel; }

    private PlayerPower playerPower;

    private void Awake()
    {
        playerPower = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPower>();

    }

    protected virtual void Update()
    {
        
    }

    protected virtual void OnEnable()
    {
        LootManager.Instance.OnExperienceChange += HandleExperience;
    }

    protected virtual void OnDisable()
    {
        LootManager.Instance.OnExperienceChange -= HandleExperience;
    }

    protected virtual void HandleExperience(int newExperience)
    {
        currentExperience += newExperience + playerPower.playerCurrentExperienceBonus;
        if (currentExperience >= maxExperience)
        {
            LevelUp();
        }
    }

    protected virtual void LevelUp()
    {
        currentLevel++;
        currentExperience = 0;
        maxExperience += 1000;
    }
}
