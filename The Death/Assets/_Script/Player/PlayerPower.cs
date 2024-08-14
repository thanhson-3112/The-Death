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
    public float playerBaseHealthRegen = 0.1f;
    public float playerCurrentHealthRegen;

    // toc do
    public float playerBaseSpeed = 20f;
    public float playerCurrentSpeed;

    // ban kinh nhat do
    public float playerBasePickRadius = 5f;
    public float playerCurrentPickRadius;

    // chi mang
    public float playerBaseCritChance = 10;
    public float playerCurrentCritChance;

    // thoi gian hoi chieu
    public float playerBaseAbilityHaste = 1f;
    public float playerCurrentAbilityHaste;

    // kinh nghiem
    public int playerBaseExperience = 10;
    public int playerCurrentExperience;

    // tia dan
    public int playerBaseProjectiles = 1;
    public int playerCurrentProjectiles;

    // tang vang nhat duoc
    public int playerBaseGold = 1;
    public int playerCurrentGold;


    public void Start()
    {
        playerCurrentDamage = playerBaseDamage; // sat thuong xong
        playerCurrentArmor = playerBaseArmor; // giap xong
        playerCurrentMaxHealth = playerBaseMaxHealth; 
        playerCurrentHealthRegen = playerBaseHealthRegen; // hoi mau xong
        playerCurrentSpeed = playerBaseSpeed; // toc do xong
        playerCurrentPickRadius = playerBasePickRadius; // ban kinh nhat do xong
        playerCurrentCritChance = playerBaseCritChance; // ti le chi mang xong
        playerCurrentAbilityHaste = playerBaseAbilityHaste; // thoi gian tac dung xong
        playerCurrentExperience = playerBaseExperience; // kinh nghiem xong
        playerCurrentProjectiles = playerBaseProjectiles; // tia dan xong
        playerCurrentGold = playerBaseGold; // vang xong
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
