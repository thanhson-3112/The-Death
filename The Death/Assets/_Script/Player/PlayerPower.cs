using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerPower : MonoBehaviour
{
    public static PlayerPower instance;

    // player dameage
    [Header("Damage")]
    public float playerBaseDamage = 70f;
    public float playerCurrentDamage;
    public TextMeshProUGUI damageText;

    // Giap
    [Header("Armor")]
    public float playerBaseArmor = 70f;
    public float playerCurrentArmor;
    public TextMeshProUGUI armorText;

    // mau toi da
    [Header("MaxHealth")]
    public float playerBaseMaxHealth = 30f;
    public float playerCurrentMaxHealth;

    // Hoi mau
    [Header("HealthRegen")]
    public float playerBaseHealthRegen = 0.1f;
    public float playerCurrentHealthRegen;

    // toc do
    [Header("Speed")]
    public float playerBaseSpeed = 20f;
    public float playerCurrentSpeed;
    public TextMeshProUGUI speedText;

    // ban kinh nhat do
    [Header("PickRadius")]
    public float playerBasePickRadius = 5f;
    public float playerCurrentPickRadius;
    public TextMeshProUGUI pickRadiusText;

    // chi mang
    [Header("CritChance")]
    public float playerBaseCritChance = 10;
    public float playerCurrentCritChance;
    public TextMeshProUGUI critChanceText;

    // thoi gian hoi chieu
    [Header("AbilityHaste")]
    public float playerBaseAbilityHaste = 1f;
    public float playerCurrentAbilityHaste;
    public TextMeshProUGUI abilityHasteText;

    // kinh nghiem
    [Header("ExperienceBonus")]
    public int playerBaseExperienceBonus = 10;
    public int playerCurrentExperienceBonus;
    public TextMeshProUGUI experienceBonusText;

    // tia dan
    [Header("Projectiles")]
    public int playerBaseProjectiles = 1;
    public int playerCurrentProjectiles;
    public TextMeshProUGUI projectilesText;

    // tang vang nhat duoc
    [Header("GoldBonus")]
    public int playerBaseGoldBonus = 1;
    public int playerCurrentGoldBonus;
    public TextMeshProUGUI goldBonusText;


    private void Awake()
    {
        if (PlayerPower.instance != null) Debug.LogError("Only 1 ScoreManager allow");
        PlayerPower.instance = this;
    }

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
        playerCurrentExperienceBonus = playerBaseExperienceBonus; // kinh nghiem xong
        playerCurrentProjectiles = playerBaseProjectiles; // tia dan xong
        playerCurrentGoldBonus = playerBaseGoldBonus; // vang xong
    }

    void Update()
    {
        damageText.text = "Damage: " + playerCurrentDamage.ToString();
        armorText.text = "Armor: " + playerCurrentArmor.ToString();
        speedText.text = "Speed: " + playerCurrentSpeed.ToString();
        pickRadiusText.text = "Pick Radius: " + playerCurrentPickRadius.ToString();
        critChanceText.text = "Crit Chance: " + playerCurrentCritChance.ToString() + "%";
        abilityHasteText.text = "Ability Haste: " + playerCurrentAbilityHaste.ToString();
        experienceBonusText.text = "XP Bonus: " + playerCurrentExperienceBonus.ToString() + "%";
        projectilesText.text = "Projectiles: " + playerCurrentProjectiles.ToString();
        goldBonusText.text = "Gold Bonus: " + playerCurrentGoldBonus.ToString() + "%";
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


    // save game
    public virtual void FromJson(string jsonString)
    {
        GameData obj = JsonUtility.FromJson<GameData>(jsonString);
        if (obj == null) return;
        this.playerBaseDamage= obj.playerBaseDamage;
        this.playerBaseArmor = obj.playerBaseArmor;
        this.playerBaseMaxHealth = obj.playerBaseMaxHealth;
        this.playerBaseHealthRegen = obj.playerBaseHealthRegen;
        this.playerBaseSpeed = obj.playerBaseSpeed;
        this.playerBasePickRadius = obj.playerBasePickRadius;
        this.playerBaseCritChance = obj.playerBaseCritChance;
        this.playerBaseAbilityHaste = obj.playerBaseAbilityHaste;
        this.playerBaseExperienceBonus = obj.playerBaseExperienceBonus;
        this.playerBaseProjectiles = obj.playerBaseProjectiles;
        this.playerBaseGoldBonus = obj.playerBaseGoldBonus;
    }
}
