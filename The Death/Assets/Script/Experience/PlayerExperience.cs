using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExperience : MonoBehaviour
{
    [SerializeField] protected internal int currentExperience, maxExperience = 300, currentLevel = 1;

    
    protected virtual void Update()
    {
        
    }
    protected virtual void OnEnable()
    {
        ExperienceManager.Instance.OnExperienceChange += HandleExperience;
    }

    protected virtual void OnDisable()
    {
        ExperienceManager.Instance.OnExperienceChange -= HandleExperience;
    }

    protected virtual void HandleExperience(int newExperience)
    {
        currentExperience += newExperience;
        if (currentExperience >= maxExperience)
        {
            LevelUp();
        }
    }

    protected virtual void LevelUp()
    {
        currentLevel++;
        currentExperience = 0;
        maxExperience += 100;
    }
}
