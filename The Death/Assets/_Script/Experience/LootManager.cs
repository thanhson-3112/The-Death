using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootManager : MonoBehaviour
{
    public static LootManager Instance;

    public delegate void ExperienceChangeHandler(int amount);
    public event ExperienceChangeHandler OnExperienceChange;

    public delegate void GoldChangeHandler(int amount);
    public event ExperienceChangeHandler OnGoldChange;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void AddExperience(int amount)
    {
        OnExperienceChange?.Invoke(amount);
    }

    public void AddGold(int amount)
    {
        OnGoldChange?.Invoke(amount);
    }
}
