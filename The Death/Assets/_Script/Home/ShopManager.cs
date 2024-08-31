using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;

    [Header("Button")]
    [SerializeField] private Button upgradeDamageButton;
    [SerializeField] private Button upgradeArmorButton;
    [SerializeField] private Button upgradeMaxHealthButton;
    [SerializeField] private Button upgradeHealthRegenButton;
    [SerializeField] private Button upgradeSpeedButton;
    [SerializeField] private Button upgradePickRadiusButton;
    [SerializeField] private Button upgradeCritChanceButton;
    [SerializeField] private Button upgradeAbilityHasteButton;
    [SerializeField] private Button upgradeExperienceBonusButton;
    [SerializeField] private Button upgradeProjectilesButton;
    [SerializeField] private Button upgradeGoldBonusButton;

    [Header("Cost")]
    [SerializeField] private TextMeshProUGUI damageCostText;
    [SerializeField] private TextMeshProUGUI armorCostText;
    [SerializeField] private TextMeshProUGUI maxHealthCostText;
    [SerializeField] private TextMeshProUGUI healthRegenCostText;
    [SerializeField] private TextMeshProUGUI speedCostText;
    [SerializeField] private TextMeshProUGUI pickRadiusCostText;
    [SerializeField] private TextMeshProUGUI critChanceCostText;
    [SerializeField] private TextMeshProUGUI abilityHasteCostText;
    [SerializeField] private TextMeshProUGUI experienceBonusCostText;
    [SerializeField] private TextMeshProUGUI projectilesCostText;
    [SerializeField] private TextMeshProUGUI goldBonusCostText;

    [Header("Slider")]
    [SerializeField] private Slider damageSlider;
    [SerializeField] private Slider armorSlider;
    [SerializeField] private Slider maxHealthSlider;
    [SerializeField] private Slider healthRegenSlider;
    [SerializeField] private Slider speedSlider;
    [SerializeField] private Slider pickRadiusSlider;
    [SerializeField] private Slider critChanceSlider;
    [SerializeField] private Slider abilityHasteSlider;
    [SerializeField] private Slider experienceBonusSlider;
    [SerializeField] private Slider projectilesSlider;
    [SerializeField] private Slider goldBonusSlider;

    // M?ng ch?a giá nâng c?p cho t?ng c?p ??
    private int[] damageUpgradeCosts = { 100, 110, 120, 130, 140 };
    private int[] armorUpgradeCosts = { 120, 130, 140, 150, 160, 170 };
    private int[] maxHealthUpgradeCosts = { 150, 160, 170, 180, 190 };
    private int[] healthRegenUpgradeCosts = { 90, 100, 110, 120, 130 };
    private int[] speedUpgradeCosts = { 80, 90, 100, 110 };
    private int[] pickRadiusUpgradeCosts = { 70, 80, 90 };
    private int[] critChanceUpgradeCosts = { 130, 140, 150, 160 };
    private int[] abilityHasteUpgradeCosts = { 100, 110, 120, 130 };
    private int[] experienceBonusUpgradeCosts = { 200, 220, 240, 260, 280 };
    private int[] projectilesUpgradeCosts = { 300, 350 };
    private int[] goldBonusUpgradeCosts = { 150, 160, 170 };

    // Các bi?n ?? l?u tr? c?p ?? hi?n t?i c?a m?i ch? s?
    public int currentDamageLevel;
    public int currentArmorLevel;
    public int currentMaxHealthLevel;
    public int currentHealthRegenLevel;
    public int currentSpeedLevel;
    public int currentPickRadiusLevel;
    public int currentCritChanceLevel;
    public int currentAbilityHasteLevel;
    public int currentExperienceBonusLevel;
    public int currentProjectilesLevel;
    public int currentGoldBonusLevel;

    private ShopNPC currentShopNPC;
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private PlayerGold playerGold;

    [SerializeField] private ShopDescription shopDescription;

    [Header("Skill Parents")]
    [SerializeField] private GameObject damageSkillParent;
    [SerializeField] private GameObject armorSkillParent;
    [SerializeField] private GameObject maxHealthSkillParent;
    [SerializeField] private GameObject healthRegenSkillParent;
    [SerializeField] private GameObject speedSkillParent;
    [SerializeField] private GameObject pickRadiusSkillParent;
    [SerializeField] private GameObject critChanceSkillParent;
    [SerializeField] private GameObject abilityHasteSkillParent;
    [SerializeField] private GameObject experienceBonusSkillParent;
    [SerializeField] private GameObject projectilesSkillParent;
    [SerializeField] private GameObject goldBonusSkillParent;

    private void Awake()
    {
        if (ShopManager.instance != null) Debug.LogError("Only 1 ScoreManager allow");
        ShopManager.instance = this;
    }

    private void Start()
    {
        playerGold = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerGold>();
        LoadUpgradeValues();

        // Gán s? ki?n click cho các nút nâng c?p
        upgradeDamageButton.onClick.AddListener(() => UpgradeStat("Damage", ref damageSlider, damageCostText, damageUpgradeCosts.Length));
        upgradeArmorButton.onClick.AddListener(() => UpgradeStat("Armor", ref armorSlider, armorCostText, armorUpgradeCosts.Length));
        upgradeMaxHealthButton.onClick.AddListener(() => UpgradeStat("MaxHealth", ref maxHealthSlider, maxHealthCostText, maxHealthUpgradeCosts.Length));
        upgradeHealthRegenButton.onClick.AddListener(() => UpgradeStat("HealthRegen", ref healthRegenSlider, healthRegenCostText, healthRegenUpgradeCosts.Length));
        upgradeSpeedButton.onClick.AddListener(() => UpgradeStat("Speed", ref speedSlider, speedCostText, speedUpgradeCosts.Length));
        upgradePickRadiusButton.onClick.AddListener(() => UpgradeStat("PickRadius", ref pickRadiusSlider, pickRadiusCostText, pickRadiusUpgradeCosts.Length));
        upgradeCritChanceButton.onClick.AddListener(() => UpgradeStat("CritChance", ref critChanceSlider, critChanceCostText, critChanceUpgradeCosts.Length));
        upgradeAbilityHasteButton.onClick.AddListener(() => UpgradeStat("AbilityHaste", ref abilityHasteSlider, abilityHasteCostText, abilityHasteUpgradeCosts.Length));
        upgradeExperienceBonusButton.onClick.AddListener(() => UpgradeStat("ExperienceBonus", ref experienceBonusSlider, experienceBonusCostText, experienceBonusUpgradeCosts.Length));
        upgradeProjectilesButton.onClick.AddListener(() => UpgradeStat("Projectiles", ref projectilesSlider, projectilesCostText, projectilesUpgradeCosts.Length));
        upgradeGoldBonusButton.onClick.AddListener(() => UpgradeStat("GoldBonus", ref goldBonusSlider, goldBonusCostText, goldBonusUpgradeCosts.Length));

        // Gán s? ki?n cho các nút nâng c?p
        AssignUpgradeButtonListeners();

        // Gán s? ki?n cho các Skill Parent
        AssignSkillParentListeners();
        // C?p nh?t UI v?i các giá tr? ban ??u
        UpdateGoldUI();
        UpdateCostTexts();

        // Ban ??u ?n UI c?a shop
        gameObject.SetActive(false);
    }

    private void AssignUpgradeButtonListeners()
    {
        AssignListener(upgradeDamageButton, "Damage", damageUpgradeCosts, damageSlider, damageCostText);
        AssignListener(upgradeArmorButton, "Armor", armorUpgradeCosts, armorSlider, armorCostText);
        AssignListener(upgradeMaxHealthButton, "MaxHealth", maxHealthUpgradeCosts, maxHealthSlider, maxHealthCostText);
        AssignListener(upgradeHealthRegenButton, "HealthRegen", healthRegenUpgradeCosts, healthRegenSlider, healthRegenCostText);
        AssignListener(upgradeSpeedButton, "Speed", speedUpgradeCosts, speedSlider, speedCostText);
        AssignListener(upgradePickRadiusButton, "PickRadius", pickRadiusUpgradeCosts, pickRadiusSlider, pickRadiusCostText);
        AssignListener(upgradeCritChanceButton, "CritChance", critChanceUpgradeCosts, critChanceSlider, critChanceCostText);
        AssignListener(upgradeAbilityHasteButton, "AbilityHaste", abilityHasteUpgradeCosts, abilityHasteSlider, abilityHasteCostText);
        AssignListener(upgradeExperienceBonusButton, "ExperienceBonus", experienceBonusUpgradeCosts, experienceBonusSlider, experienceBonusCostText);
        AssignListener(upgradeProjectilesButton, "Projectiles", projectilesUpgradeCosts, projectilesSlider, projectilesCostText);
        AssignListener(upgradeGoldBonusButton, "GoldBonus", goldBonusUpgradeCosts, goldBonusSlider, goldBonusCostText);
    }

    private void AssignListener(Button upgradeButton, string statName, int[] upgradeCosts, Slider upgradeSlider, TextMeshProUGUI costText)
    {
        upgradeButton.onClick.AddListener(() =>
        {
            SetShopDescription(statName, upgradeSlider, upgradeCosts, costText, upgradeButton);
        });
    }

    private void AssignSkillParentListeners()
    {
        AssignSkillParentListener(damageSkillParent, "Damage", damageSlider, damageUpgradeCosts, damageCostText, upgradeDamageButton);
        AssignSkillParentListener(armorSkillParent, "Armor", armorSlider, armorUpgradeCosts, armorCostText, upgradeArmorButton);
        AssignSkillParentListener(maxHealthSkillParent, "MaxHealth", maxHealthSlider, maxHealthUpgradeCosts, maxHealthCostText, upgradeMaxHealthButton);
        AssignSkillParentListener(healthRegenSkillParent, "HealthRegen", healthRegenSlider, healthRegenUpgradeCosts, healthRegenCostText, upgradeHealthRegenButton);
        AssignSkillParentListener(speedSkillParent, "Speed", speedSlider, speedUpgradeCosts, speedCostText, upgradeSpeedButton);
        AssignSkillParentListener(pickRadiusSkillParent, "PickRadius", pickRadiusSlider, pickRadiusUpgradeCosts, pickRadiusCostText, upgradePickRadiusButton);
        AssignSkillParentListener(critChanceSkillParent, "CritChance", critChanceSlider, critChanceUpgradeCosts, critChanceCostText, upgradeCritChanceButton);
        AssignSkillParentListener(abilityHasteSkillParent, "AbilityHaste", abilityHasteSlider, abilityHasteUpgradeCosts, abilityHasteCostText, upgradeAbilityHasteButton);
        AssignSkillParentListener(experienceBonusSkillParent, "ExperienceBonus", experienceBonusSlider, experienceBonusUpgradeCosts, experienceBonusCostText, upgradeExperienceBonusButton);
        AssignSkillParentListener(projectilesSkillParent, "Projectiles", projectilesSlider, projectilesUpgradeCosts, projectilesCostText, upgradeProjectilesButton);
        AssignSkillParentListener(goldBonusSkillParent, "GoldBonus", goldBonusSlider, goldBonusUpgradeCosts, goldBonusCostText, upgradeGoldBonusButton);
    }

    private void AssignSkillParentListener(GameObject skillParent, string statName, Slider upgradeSlider, int[] upgradeCosts, TextMeshProUGUI costText, Button upgradeButton)
    {
        Button skillParentButton = skillParent.GetComponent<Button>();
        if (skillParentButton != null)
        {
            skillParentButton.onClick.AddListener(() =>
            {
                SetShopDescription(statName, upgradeSlider, upgradeCosts, costText, upgradeButton);
            });
        }
    }

    private void SetShopDescription(string statName, Slider slider, int[] upgradeCosts, TextMeshProUGUI costText, Button upgradeButton)
    {
        int currentLevel = (int)slider.value;
        Sprite skillSprite = upgradeButton != null ? upgradeButton.image.sprite : null;
        string skillName = statName;
        string skillCost = currentLevel < upgradeCosts.Length ? upgradeCosts[currentLevel].ToString() : "Max";
        float sliderValue = slider.value;
        UnityEngine.Events.UnityAction buttonAction = upgradeButton != null ? upgradeButton.onClick.Invoke : null;

        shopDescription.SetDescription(skillSprite, skillName, skillCost, sliderValue, buttonAction);
    }

    public void SetCurrentShopNPC(ShopNPC shopNPC)
    {
        currentShopNPC = shopNPC;
    }

    public void ShopOpen()
    {
        gameObject.SetActive(true);
    }

    public void ShopClose()
    {
        gameObject.SetActive(false);
        currentShopNPC = null;
    }

    private void UpgradeStat(string statName, ref Slider upgradeSlider, TextMeshProUGUI costText, int maxUpgradeLevel)
    {
        int currentLevel = (int)upgradeSlider.value;
        int currentUpgradeCost = 0;

        // L?y giá nâng c?p hi?n t?i d?a trên c?p ??
        switch (statName)
        {
            case "Damage":
                if (currentLevel < damageUpgradeCosts.Length)
                    currentUpgradeCost = damageUpgradeCosts[currentLevel];
                break;
            case "Armor":
                if (currentLevel < armorUpgradeCosts.Length)
                    currentUpgradeCost = armorUpgradeCosts[currentLevel];
                break;
            case "MaxHealth":
                if (currentLevel < maxHealthUpgradeCosts.Length)
                    currentUpgradeCost = maxHealthUpgradeCosts[currentLevel];
                break;
            case "HealthRegen":
                if (currentLevel < healthRegenUpgradeCosts.Length)
                    currentUpgradeCost = healthRegenUpgradeCosts[currentLevel];
                break;
            case "Speed":
                if (currentLevel < speedUpgradeCosts.Length)
                    currentUpgradeCost = speedUpgradeCosts[currentLevel];
                break;
            case "PickRadius":
                if (currentLevel < pickRadiusUpgradeCosts.Length)
                    currentUpgradeCost = pickRadiusUpgradeCosts[currentLevel];
                break;
            case "CritChance":
                if (currentLevel < critChanceUpgradeCosts.Length)
                    currentUpgradeCost = critChanceUpgradeCosts[currentLevel];
                break;
            case "AbilityHaste":
                if (currentLevel < abilityHasteUpgradeCosts.Length)
                    currentUpgradeCost = abilityHasteUpgradeCosts[currentLevel];
                break;
            case "ExperienceBonus":
                if (currentLevel < experienceBonusUpgradeCosts.Length)
                    currentUpgradeCost = experienceBonusUpgradeCosts[currentLevel];
                break;
            case "Projectiles":
                if (currentLevel < projectilesUpgradeCosts.Length)
                    currentUpgradeCost = projectilesUpgradeCosts[currentLevel];
                break;
            case "GoldBonus":
                if (currentLevel < goldBonusUpgradeCosts.Length)
                    currentUpgradeCost = goldBonusUpgradeCosts[currentLevel];
                break;
        }

        if (playerGold.goldTotal < currentUpgradeCost)
        {
            Debug.Log("Khong du vang.");
            return;
        }

        if (currentLevel >= maxUpgradeLevel)
        {
            Debug.Log("Da dat cap toi da cho " + statName);
            return;
        }

        // Tr? vàng
        playerGold.goldTotal -= currentUpgradeCost;

        // Nâng c?p ch? s? t??ng ?ng
        switch (statName)
        {
            case "Damage":
                PlayerPower.instance.playerBaseDamage += 1;
                currentDamageLevel = currentLevel + 1;
                break;
            case "Armor":
                PlayerPower.instance.playerBaseArmor += 5;
                currentArmorLevel = currentLevel + 1;
                break;
            case "MaxHealth":
                PlayerPower.instance.playerBaseMaxHealth += 10;
                currentMaxHealthLevel = currentLevel + 1;
                break;
            case "HealthRegen":
                PlayerPower.instance.playerBaseHealthRegen += 0.5f;
                currentHealthRegenLevel = currentLevel + 1;
                break;
            case "Speed":
                PlayerPower.instance.playerBaseSpeed += 0.1f;
                currentSpeedLevel = currentLevel + 1;
                break;
            case "PickRadius":
                PlayerPower.instance.playerBasePickRadius += 0.5f;
                currentPickRadiusLevel = currentLevel + 1;
                break;
            case "CritChance":
                PlayerPower.instance.playerBaseCritChance += 0.02f;
                currentCritChanceLevel = currentLevel + 1;
                break;
            case "AbilityHaste":
                PlayerPower.instance.playerBaseAbilityHaste += 0.05f;
                currentAbilityHasteLevel = currentLevel + 1;
                break;
            case "ExperienceBonus":
                PlayerPower.instance.playerBaseExperienceBonus += 1;
                currentExperienceBonusLevel = currentLevel + 1;
                break;
            case "Projectiles":
                PlayerPower.instance.playerBaseProjectiles += 1;
                currentProjectilesLevel = currentLevel + 1;
                break;
            case "GoldBonus":
                PlayerPower.instance.playerBaseGoldBonus += 1;
                currentGoldBonusLevel = currentLevel + 1;
                break;
        }

        // T?ng giá tr? slider
        upgradeSlider.value += 1;

        // C?p nh?t UI
        UpdateGoldUI();
        UpdateCostTexts();
    }

    private void LoadUpgradeValues()
    {
        damageSlider.value = currentDamageLevel;
        armorSlider.value = currentArmorLevel;
        maxHealthSlider.value = currentMaxHealthLevel;
        healthRegenSlider.value = currentHealthRegenLevel;
        speedSlider.value = currentSpeedLevel;
        pickRadiusSlider.value = currentPickRadiusLevel;
        critChanceSlider.value = currentCritChanceLevel;
        abilityHasteSlider.value = currentAbilityHasteLevel;
        experienceBonusSlider.value = currentExperienceBonusLevel;
        projectilesSlider.value = currentProjectilesLevel;
        goldBonusSlider.value = currentGoldBonusLevel;
    }

    private void UpdateGoldUI()
    {
        goldText.text = playerGold.goldTotal.ToString();
    }

    private void UpdateCostTexts()
    {
        // Ki?m tra n?u c?p ?? hi?n t?i không v??t quá gi?i h?n c?a m?ng giá nâng c?p
        if (currentDamageLevel < damageUpgradeCosts.Length)
            damageCostText.text = damageUpgradeCosts[currentDamageLevel].ToString();
        else
            damageCostText.text = "Max";

        if (currentArmorLevel < armorUpgradeCosts.Length)
            armorCostText.text = armorUpgradeCosts[currentArmorLevel].ToString();
        else
            armorCostText.text = "Max";

        if (currentMaxHealthLevel < maxHealthUpgradeCosts.Length)
            maxHealthCostText.text = maxHealthUpgradeCosts[currentMaxHealthLevel].ToString();
        else
            maxHealthCostText.text = "Max";

        if (currentHealthRegenLevel < healthRegenUpgradeCosts.Length)
            healthRegenCostText.text = healthRegenUpgradeCosts[currentHealthRegenLevel].ToString();
        else
            healthRegenCostText.text = "Max";

        if (currentSpeedLevel < speedUpgradeCosts.Length)
            speedCostText.text = speedUpgradeCosts[currentSpeedLevel].ToString();
        else
            speedCostText.text = "Max";

        if (currentPickRadiusLevel < pickRadiusUpgradeCosts.Length)
            pickRadiusCostText.text = pickRadiusUpgradeCosts[currentPickRadiusLevel].ToString();
        else
            pickRadiusCostText.text = "Max";

        if (currentCritChanceLevel < critChanceUpgradeCosts.Length)
            critChanceCostText.text = critChanceUpgradeCosts[currentCritChanceLevel].ToString();
        else
            critChanceCostText.text = "Max";

        if (currentAbilityHasteLevel < abilityHasteUpgradeCosts.Length)
            abilityHasteCostText.text = abilityHasteUpgradeCosts[currentAbilityHasteLevel].ToString();
        else
            abilityHasteCostText.text = "Max";

        if (currentExperienceBonusLevel < experienceBonusUpgradeCosts.Length)
            experienceBonusCostText.text = experienceBonusUpgradeCosts[currentExperienceBonusLevel].ToString();
        else
            experienceBonusCostText.text = "Max";

        if (currentProjectilesLevel < projectilesUpgradeCosts.Length)
            projectilesCostText.text = projectilesUpgradeCosts[currentProjectilesLevel].ToString();
        else
            projectilesCostText.text = "Max";

        if (currentGoldBonusLevel < goldBonusUpgradeCosts.Length)
            goldBonusCostText.text = goldBonusUpgradeCosts[currentGoldBonusLevel].ToString();
        else
            goldBonusCostText.text = "Max";
    }

    //Save game
    public virtual void FromJson(string jsonString)
    {
        GameData obj = JsonUtility.FromJson<GameData>(jsonString);
        if (obj == null) return;
        this.currentDamageLevel = obj.currentDamageLevel;
        this.currentArmorLevel = obj.currentArmorLevel;
        this.currentMaxHealthLevel = obj.currentMaxHealthLevel;
        this.currentHealthRegenLevel = obj.currentHealthRegenLevel;
        this.currentSpeedLevel = obj.currentSpeedLevel;
        this.currentPickRadiusLevel = obj.currentPickRadiusLevel;
        this.currentCritChanceLevel = obj.currentCritChanceLevel;
        this.currentAbilityHasteLevel = obj.currentAbilityHasteLevel;
        this.currentExperienceBonusLevel = obj.currentExperienceBonusLevel;
        this.currentProjectilesLevel = obj.currentProjectilesLevel;
        this.currentGoldBonusLevel = obj.currentGoldBonusLevel;

        LoadUpgradeValues();

        // C?p nh?t UI sau khi giá tr? ?ã ???c load
        UpdateCostTexts();
        UpdateGoldUI();
    }

}