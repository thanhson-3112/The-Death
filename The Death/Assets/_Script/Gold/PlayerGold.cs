using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGold : MonoBehaviour
{
    [SerializeField] private int goldTotal;


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
        GoldUp();
    }

    protected virtual void GoldUp()
    {
        goldTotal++;
    }
}
