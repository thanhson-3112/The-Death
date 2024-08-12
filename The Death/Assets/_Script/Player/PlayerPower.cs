using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPower : MonoBehaviour
{
    // player dameage
    public float playerBaseDamage = 70f;
    public float playerCurrentDamage;

    // Giap
    public float playerBaseArmor = 70f;
    public float playerCurrentArmor;

    // mau toi da
    public float playerBaseMaxHealth = 30f;
    public float playerCurrentMaxHealth;

    // Hoi mau
    public float playerBaseHealthRegen = 70f;
    public float playerCurrentHealthRegen;

    // toc do
    public float playerBaseSpeed = 20f;
    public float playerCurrentSpeed;

    // ban kinh nhat do
    public float playerBasePickRadius = 10;
    public float playerCurrentPickRadius;

    // chi mang
    public float playerBaseCrit = 1;
    public float playerCurrentCrit;

    // thoi gian hoi chieu
    public float playerBaseAbilityHaste = 0.2f;
    public float playerCurrentAbilityHaste;

    // kinh nghiem
    public float playerBaseExperience = 1;
    public float playerCurrentExperience;

    // tia dan
    public float playerBaseProjectiles = 1;
    public float playerCurrentProjectiles;

    // tang vang nhat duoc
    public float playerBaseGold = 1;
    public float playerCurrentGold;


    void Start()
    {
        playerCurrentDamage = playerBaseDamage;
        playerCurrentArmor = playerBaseArmor;
        playerCurrentMaxHealth = playerBaseMaxHealth;
        playerCurrentSpeed = playerBaseSpeed;
        playerCurrentPickRadius = playerBasePickRadius;
        playerCurrentCrit = playerBaseCrit;
        playerCurrentAbilityHaste = playerBaseAbilityHaste;
        playerCurrentExperience = playerBaseExperience;
        playerCurrentProjectiles = playerBaseProjectiles;
        playerCurrentGold = playerBaseGold;
    }

    void Update()
    {
        
    }

    public void PlayerDamageUpgrade()
    {
        playerCurrentDamage += 2;
    }

    public void PlayerDamageUpgrade2()
    {
        playerCurrentDamage += 5;
    }

    public void DashUpgrade()
    {
        playerCurrentAbilityHaste += 0.1f;
    }

    public void DashUpgrade2()
    {
        playerCurrentAbilityHaste += 0.1f;
    }
}
