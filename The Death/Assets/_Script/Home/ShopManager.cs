using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;

    [Header("Sprite")]
    [SerializeField] private List<Sprite> upgradeImgs;

    [Header("Button")]
    [SerializeField] private List<Button> upgradeButtons;

    [Header("Cost")]
    [SerializeField] private List<TextMeshProUGUI> upgradeCostTexts;

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

    [Header("Slider value level")]
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
    [SerializeField] private List<GameObject> skillParent;

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
        upgradeButtons[0].onClick.AddListener(() => UpgradeStat("Damage", ref damageSlider, upgradeCostTexts[0], damageUpgradeCosts.Length));
        upgradeButtons[1].onClick.AddListener(() => UpgradeStat("Armor", ref armorSlider, upgradeCostTexts[1], armorUpgradeCosts.Length));
        upgradeButtons[2].onClick.AddListener(() => UpgradeStat("MaxHealth", ref maxHealthSlider, upgradeCostTexts[2], maxHealthUpgradeCosts.Length));
        upgradeButtons[3].onClick.AddListener(() => UpgradeStat("HealthRegen", ref healthRegenSlider, upgradeCostTexts[3], healthRegenUpgradeCosts.Length));
        upgradeButtons[4].onClick.AddListener(() => UpgradeStat("Speed", ref speedSlider, upgradeCostTexts[4], speedUpgradeCosts.Length));
        upgradeButtons[5].onClick.AddListener(() => UpgradeStat("PickRadius", ref pickRadiusSlider, upgradeCostTexts[5], pickRadiusUpgradeCosts.Length));
        upgradeButtons[6].onClick.AddListener(() => UpgradeStat("CritChance", ref critChanceSlider, upgradeCostTexts[6], critChanceUpgradeCosts.Length));
        upgradeButtons[7].onClick.AddListener(() => UpgradeStat("AbilityHaste", ref abilityHasteSlider, upgradeCostTexts[7], abilityHasteUpgradeCosts.Length));
        upgradeButtons[8].onClick.AddListener(() => UpgradeStat("ExperienceBonus", ref experienceBonusSlider, upgradeCostTexts[8], experienceBonusUpgradeCosts.Length));
        upgradeButtons[9].onClick.AddListener(() => UpgradeStat("Projectiles", ref projectilesSlider, upgradeCostTexts[9], projectilesUpgradeCosts.Length));
        upgradeButtons[10].onClick.AddListener(() => UpgradeStat("GoldBonus", ref goldBonusSlider, upgradeCostTexts[10], goldBonusUpgradeCosts.Length));

        // Gán s? ki?n cho các Skill Parent
        AssignSkillParentListeners();

        // C?p nh?t UI v?i các giá tr? ban ??u
        UpdateGoldUI();
        UpdateCostTexts();

        // Ban ??u ?n UI c?a shop
        gameObject.SetActive(false);
    }

    private void AssignSkillParentListeners()
    {
        AssignSkillParentListener(skillParent[0], upgradeImgs[0], "Damage", damageSlider, damageUpgradeCosts, upgradeCostTexts[0], upgradeButtons[0]);
        AssignSkillParentListener(skillParent[1], upgradeImgs[1], "Armor", armorSlider, armorUpgradeCosts, upgradeCostTexts[1], upgradeButtons[1]);
        AssignSkillParentListener(skillParent[2], upgradeImgs[2], "MaxHealth", maxHealthSlider, maxHealthUpgradeCosts, upgradeCostTexts[2], upgradeButtons[2]);
        AssignSkillParentListener(skillParent[3], upgradeImgs[3], "HealthRegen", healthRegenSlider, healthRegenUpgradeCosts, upgradeCostTexts[3], upgradeButtons[3]);
        AssignSkillParentListener(skillParent[4], upgradeImgs[4], "Speed", speedSlider, speedUpgradeCosts, upgradeCostTexts[4], upgradeButtons[4]);
        AssignSkillParentListener(skillParent[5], upgradeImgs[5], "PickRadius", pickRadiusSlider, pickRadiusUpgradeCosts, upgradeCostTexts[5], upgradeButtons[5]);
        AssignSkillParentListener(skillParent[6], upgradeImgs[6], "CritChance", critChanceSlider, critChanceUpgradeCosts, upgradeCostTexts[6], upgradeButtons[6]);
        AssignSkillParentListener(skillParent[7], upgradeImgs[7], "AbilityHaste", abilityHasteSlider, abilityHasteUpgradeCosts, upgradeCostTexts[7], upgradeButtons[7]);
        AssignSkillParentListener(skillParent[8], upgradeImgs[8], "ExperienceBonus", experienceBonusSlider, experienceBonusUpgradeCosts, upgradeCostTexts[8], upgradeButtons[8]);
        AssignSkillParentListener(skillParent[9], upgradeImgs[9], "Projectiles", projectilesSlider, projectilesUpgradeCosts, upgradeCostTexts[9], upgradeButtons[9]);
        AssignSkillParentListener(skillParent[10], upgradeImgs[10], "GoldBonus", goldBonusSlider, goldBonusUpgradeCosts, upgradeCostTexts[10], upgradeButtons[10]);
    }

    private void AssignSkillParentListener(GameObject skillParent, Sprite Img, string statName, Slider upgradeSlider, int[] upgradeCosts, TextMeshProUGUI costText, Button upgradeButton)
    {
        Button skillParentButton = skillParent.GetComponent<Button>();
        if (skillParentButton != null)
        {
            skillParentButton.onClick.AddListener(() =>
            {
                SetShopDescription(Img, statName, upgradeSlider, upgradeCosts, costText, upgradeButton);
            });

            upgradeButton.onClick.AddListener(() =>
            {
                SetShopDescription(Img, statName, upgradeSlider, upgradeCosts, costText, upgradeButton);
            });

        }
    }

    private void SetShopDescription(Sprite Img, string statName, Slider slider, int[] upgradeCosts, TextMeshProUGUI costText, Button upgradeButton)
    {
        int currentLevel = (int)slider.value;
        Sprite skillSprite = Img;
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
            upgradeCostTexts[0].text = damageUpgradeCosts[currentDamageLevel].ToString();
        else
            upgradeCostTexts[0].text = "Max";

        if (currentArmorLevel < armorUpgradeCosts.Length)
            upgradeCostTexts[1].text = armorUpgradeCosts[currentArmorLevel].ToString();
        else
            upgradeCostTexts[1].text = "Max";

        if (currentMaxHealthLevel < maxHealthUpgradeCosts.Length)
            upgradeCostTexts[2].text = maxHealthUpgradeCosts[currentMaxHealthLevel].ToString();
        else
            upgradeCostTexts[2].text = "Max";

        if (currentHealthRegenLevel < healthRegenUpgradeCosts.Length)
            upgradeCostTexts[3].text = healthRegenUpgradeCosts[currentHealthRegenLevel].ToString();
        else
            upgradeCostTexts[3].text = "Max";

        if (currentSpeedLevel < speedUpgradeCosts.Length)
            upgradeCostTexts[4].text = speedUpgradeCosts[currentSpeedLevel].ToString();
        else
            upgradeCostTexts[4].text = "Max";

        if (currentPickRadiusLevel < pickRadiusUpgradeCosts.Length)
            upgradeCostTexts[5].text = pickRadiusUpgradeCosts[currentPickRadiusLevel].ToString();
        else
            upgradeCostTexts[5].text = "Max";

        if (currentCritChanceLevel < critChanceUpgradeCosts.Length)
            upgradeCostTexts[6].text = critChanceUpgradeCosts[currentCritChanceLevel].ToString();
        else
            upgradeCostTexts[6].text = "Max";

        if (currentAbilityHasteLevel < abilityHasteUpgradeCosts.Length)
            upgradeCostTexts[7].text = abilityHasteUpgradeCosts[currentAbilityHasteLevel].ToString();
        else
            upgradeCostTexts[7].text = "Max";

        if (currentExperienceBonusLevel < experienceBonusUpgradeCosts.Length)
            upgradeCostTexts[8].text = experienceBonusUpgradeCosts[currentExperienceBonusLevel].ToString();
        else
            upgradeCostTexts[8].text = "Max";

        if (currentProjectilesLevel < projectilesUpgradeCosts.Length)
            upgradeCostTexts[9].text = projectilesUpgradeCosts[currentProjectilesLevel].ToString();
        else
            upgradeCostTexts[9].text = "Max";

        if (currentGoldBonusLevel < goldBonusUpgradeCosts.Length)
            upgradeCostTexts[10].text = goldBonusUpgradeCosts[currentGoldBonusLevel].ToString();
        else
            upgradeCostTexts[10].text = "Max";
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

