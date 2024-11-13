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

    // Danh sach gia tien
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

    // danh sach chi so nang cap
    private int[] damageUpgradeValues = { 1, 3, 5, 7, 10 };
    private int[] armorUpgradeValues = { 5, 10, 15, 20, 25, 30 };
    private int[] maxHealthUpgradeValues = { 10, 20, 30, 40, 50 };
    private float[] healthRegenUpgradeValues = { 0.5f, 1.0f, 1.5f, 2.0f, 2.5f };
    private float[] speedUpgradeValues = { 0.1f, 0.2f, 0.3f, 0.4f };
    private float[] pickRadiusUpgradeValues = { 0.5f, 1.0f, 1.5f };
    private float[] critChanceUpgradeValues = { 0.02f, 0.04f, 0.06f, 0.08f };
    private float[] abilityHasteUpgradeValues = { 0.05f, 0.10f, 0.15f, 0.20f };
    private int[] experienceBonusUpgradeValues = { 1, 2, 3, 4, 5 };
    private int[] projectilesUpgradeValues = { 1, 2 };
    private int[] goldBonusUpgradeValues = { 1, 2, 3 };

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
        upgradeButtons[0].onClick.AddListener(() => UpgradeStat("DAMAGE", ref damageSlider, upgradeCostTexts[0], damageUpgradeCosts.Length));
        upgradeButtons[1].onClick.AddListener(() => UpgradeStat("ARMOR", ref armorSlider, upgradeCostTexts[1], armorUpgradeCosts.Length));
        upgradeButtons[2].onClick.AddListener(() => UpgradeStat("MAX HEALTH", ref maxHealthSlider, upgradeCostTexts[2], maxHealthUpgradeCosts.Length));
        upgradeButtons[3].onClick.AddListener(() => UpgradeStat("HEALTH REGEN", ref healthRegenSlider, upgradeCostTexts[3], healthRegenUpgradeCosts.Length));
        upgradeButtons[4].onClick.AddListener(() => UpgradeStat("SPEED", ref speedSlider, upgradeCostTexts[4], speedUpgradeCosts.Length));
        upgradeButtons[5].onClick.AddListener(() => UpgradeStat("PICK RADIUS", ref pickRadiusSlider, upgradeCostTexts[5], pickRadiusUpgradeCosts.Length));
        upgradeButtons[6].onClick.AddListener(() => UpgradeStat("CRIT CHANCE", ref critChanceSlider, upgradeCostTexts[6], critChanceUpgradeCosts.Length));
        upgradeButtons[7].onClick.AddListener(() => UpgradeStat("ABILITY HASTE", ref abilityHasteSlider, upgradeCostTexts[7], abilityHasteUpgradeCosts.Length));
        upgradeButtons[8].onClick.AddListener(() => UpgradeStat("EXPERIENCE", ref experienceBonusSlider, upgradeCostTexts[8], experienceBonusUpgradeCosts.Length));
        upgradeButtons[9].onClick.AddListener(() => UpgradeStat("PROJECTILES", ref projectilesSlider, upgradeCostTexts[9], projectilesUpgradeCosts.Length));
        upgradeButtons[10].onClick.AddListener(() => UpgradeStat("GOLD", ref goldBonusSlider, upgradeCostTexts[10], goldBonusUpgradeCosts.Length));

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
        AssignSkillParentListener(skillParent[0], upgradeImgs[0], "DAMAGE", damageSlider, damageUpgradeCosts, upgradeCostTexts[0], upgradeButtons[0]);
        AssignSkillParentListener(skillParent[1], upgradeImgs[1], "ARMOR", armorSlider, armorUpgradeCosts, upgradeCostTexts[1], upgradeButtons[1]);
        AssignSkillParentListener(skillParent[2], upgradeImgs[2], "MAX HEALTH", maxHealthSlider, maxHealthUpgradeCosts, upgradeCostTexts[2], upgradeButtons[2]);
        AssignSkillParentListener(skillParent[3], upgradeImgs[3], "HEALTH REGEN", healthRegenSlider, healthRegenUpgradeCosts, upgradeCostTexts[3], upgradeButtons[3]);
        AssignSkillParentListener(skillParent[4], upgradeImgs[4], "SPEED", speedSlider, speedUpgradeCosts, upgradeCostTexts[4], upgradeButtons[4]);
        AssignSkillParentListener(skillParent[5], upgradeImgs[5], "PICK RADIUS", pickRadiusSlider, pickRadiusUpgradeCosts, upgradeCostTexts[5], upgradeButtons[5]);
        AssignSkillParentListener(skillParent[6], upgradeImgs[6], "CRIT CHANCE", critChanceSlider, critChanceUpgradeCosts, upgradeCostTexts[6], upgradeButtons[6]);
        AssignSkillParentListener(skillParent[7], upgradeImgs[7], "ABILITY HASTE", abilityHasteSlider, abilityHasteUpgradeCosts, upgradeCostTexts[7], upgradeButtons[7]);
        AssignSkillParentListener(skillParent[8], upgradeImgs[8], "EXPERIENCE", experienceBonusSlider, experienceBonusUpgradeCosts, upgradeCostTexts[8], upgradeButtons[8]);
        AssignSkillParentListener(skillParent[9], upgradeImgs[9], "PROJECTILES", projectilesSlider, projectilesUpgradeCosts, upgradeCostTexts[9], upgradeButtons[9]);
        AssignSkillParentListener(skillParent[10], upgradeImgs[10], "GOLD", goldBonusSlider, goldBonusUpgradeCosts, upgradeCostTexts[10], upgradeButtons[10]);
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
        string skillCost = currentLevel < upgradeCosts.Length ? upgradeCosts[currentLevel].ToString() : "MAX";
        float sliderValue = slider.value;

        // Hi?n th? giá tr? hi?n t?i và giá tr? ti?p theo
        string currentUpgradeValueText = "" + (currentLevel == 0 ? 0 : GetUpgradeValue(statName, currentLevel - 1).ToString());
        string nextUpgradeValueText = currentLevel < upgradeCosts.Length ? " -> " + GetUpgradeValue(statName, currentLevel).ToString() : "";

        UnityEngine.Events.UnityAction buttonAction = upgradeButton != null ? upgradeButton.onClick.Invoke : null;

        shopDescription.SetDescription(skillSprite, skillName, skillCost, sliderValue, buttonAction, currentUpgradeValueText, nextUpgradeValueText);
    }

    private float GetUpgradeValue(string statName, int level)
{
    switch (statName)
    {
        case "DAMAGE":
            return damageUpgradeValues[Mathf.Clamp(level, 0, damageUpgradeValues.Length - 1)];
        case "ARMOR":
            return armorUpgradeValues[Mathf.Clamp(level, 0, armorUpgradeValues.Length - 1)];
        case "MAX HEALTH":
            return maxHealthUpgradeValues[Mathf.Clamp(level, 0, maxHealthUpgradeValues.Length - 1)];
        case "HEALTH REGEN":
            return healthRegenUpgradeValues[Mathf.Clamp(level, 0, healthRegenUpgradeValues.Length - 1)];
        case "SPEED":
            return speedUpgradeValues[Mathf.Clamp(level, 0, speedUpgradeValues.Length - 1)];
        case "PICK RADIUS":
            return pickRadiusUpgradeValues[Mathf.Clamp(level, 0, pickRadiusUpgradeValues.Length - 1)];
        case "CRIT CHANCE":
            return critChanceUpgradeValues[Mathf.Clamp(level, 0, critChanceUpgradeValues.Length - 1)];
        case "ABILITY HASTE":
            return abilityHasteUpgradeValues[Mathf.Clamp(level, 0, abilityHasteUpgradeValues.Length - 1)];
        case "EXPERIENCE":
            return experienceBonusUpgradeValues[Mathf.Clamp(level, 0, experienceBonusUpgradeValues.Length - 1)];
        case "PROJECTILES":
            return projectilesUpgradeValues[Mathf.Clamp(level, 0, projectilesUpgradeValues.Length - 1)];
        case "GOLD":
            return goldBonusUpgradeValues[Mathf.Clamp(level, 0, goldBonusUpgradeValues.Length - 1)];
        default:
            return 0;
    }
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

        // Gia tien hien tai gan bang gia tri hien tai cua mang Upgr cost
        switch (statName)
        {
            case "DAMAGE":
                if (currentLevel < damageUpgradeCosts.Length)
                    currentUpgradeCost = damageUpgradeCosts[currentLevel];
                break;
            case "ARMOR":
                if (currentLevel < armorUpgradeCosts.Length)
                    currentUpgradeCost = armorUpgradeCosts[currentLevel];
                break;
            case "MAX HEALTH":
                if (currentLevel < maxHealthUpgradeCosts.Length)
                    currentUpgradeCost = maxHealthUpgradeCosts[currentLevel];
                break;
            case "HEALTH REGEN":
                if (currentLevel < healthRegenUpgradeCosts.Length)
                    currentUpgradeCost = healthRegenUpgradeCosts[currentLevel];
                break;
            case "SPEED":
                if (currentLevel < speedUpgradeCosts.Length)
                    currentUpgradeCost = speedUpgradeCosts[currentLevel];
                break;
            case "PICK RADIUS":
                if (currentLevel < pickRadiusUpgradeCosts.Length)
                    currentUpgradeCost = pickRadiusUpgradeCosts[currentLevel];
                break;
            case "CRIT CHANCE":
                if (currentLevel < critChanceUpgradeCosts.Length)
                    currentUpgradeCost = critChanceUpgradeCosts[currentLevel];
                break;
            case "ABILITY HASTE":
                if (currentLevel < abilityHasteUpgradeCosts.Length)
                    currentUpgradeCost = abilityHasteUpgradeCosts[currentLevel];
                break;
            case "EXPERIENCE":
                if (currentLevel < experienceBonusUpgradeCosts.Length)
                    currentUpgradeCost = experienceBonusUpgradeCosts[currentLevel];
                break;
            case "PROJECTILES":
                if (currentLevel < projectilesUpgradeCosts.Length)
                    currentUpgradeCost = projectilesUpgradeCosts[currentLevel];
                break;
            case "GOLD":
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

        // Tru vang 
        playerGold.goldTotal -= currentUpgradeCost;

        // Thuc hien nang cap
        switch (statName)
        {
            case "DAMAGE":
                PlayerPower.instance.playerBaseDamage += damageUpgradeValues[currentLevel];
                currentDamageLevel = currentLevel + 1;
                break;
            case "ARMOR":
                PlayerPower.instance.playerBaseArmor += armorUpgradeValues[currentLevel];
                currentArmorLevel = currentLevel + 1;
                break;
            case "MAX HEALTH":
                PlayerPower.instance.playerBaseMaxHealth += maxHealthUpgradeValues[currentLevel];
                currentMaxHealthLevel = currentLevel + 1;
                break;
            case "HEALTH REGEN":
                PlayerPower.instance.playerBaseHealthRegen += healthRegenUpgradeValues[currentLevel];
                currentHealthRegenLevel = currentLevel + 1;
                break;
            case "SPEED":
                PlayerPower.instance.playerBaseSpeed += speedUpgradeValues[currentLevel];
                currentSpeedLevel = currentLevel + 1;
                break;
            case "PICK RADIUS":
                PlayerPower.instance.playerBasePickRadius += pickRadiusUpgradeValues[currentLevel];
                currentPickRadiusLevel = currentLevel + 1;
                break;
            case "CRIT CHANCE":
                PlayerPower.instance.playerBaseCritChance += critChanceUpgradeValues[currentLevel];
                currentCritChanceLevel = currentLevel + 1;
                break;
            case "ABILITY HASTE":
                PlayerPower.instance.playerBaseAbilityHaste -= abilityHasteUpgradeValues[currentLevel];
                currentAbilityHasteLevel = currentLevel + 1;
                break;
            case "EXPERIENCE":
                PlayerPower.instance.playerBaseExperienceBonus += experienceBonusUpgradeValues[currentLevel];
                currentExperienceBonusLevel = currentLevel + 1;
                break;
            case "PROJECTILES":
                PlayerPower.instance.playerBaseProjectiles += projectilesUpgradeValues[currentLevel];
                currentProjectilesLevel = currentLevel + 1;
                break;
            case "GOLD":
                PlayerPower.instance.playerBaseGoldBonus += goldBonusUpgradeValues[currentLevel];
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

    // Hien thi gia tien
    private void UpdateGoldUI()
    {
        goldText.text = playerGold.goldTotal.ToString();
    }

    // Thay doi hien thi gia tien
    private void UpdateCostTexts()
    {
        // KIem tra cap hien tai khong vuot qua gia tri cua mang nang cap
        if (currentDamageLevel < damageUpgradeCosts.Length)
            upgradeCostTexts[0].text = damageUpgradeCosts[currentDamageLevel].ToString();
        else
            upgradeCostTexts[0].text = "MAX";

        if (currentArmorLevel < armorUpgradeCosts.Length)
            upgradeCostTexts[1].text = armorUpgradeCosts[currentArmorLevel].ToString();
        else
            upgradeCostTexts[1].text = "MAX";

        if (currentMaxHealthLevel < maxHealthUpgradeCosts.Length)
            upgradeCostTexts[2].text = maxHealthUpgradeCosts[currentMaxHealthLevel].ToString();
        else
            upgradeCostTexts[2].text = "MAX";

        if (currentHealthRegenLevel < healthRegenUpgradeCosts.Length)
            upgradeCostTexts[3].text = healthRegenUpgradeCosts[currentHealthRegenLevel].ToString();
        else
            upgradeCostTexts[3].text = "MAX";

        if (currentSpeedLevel < speedUpgradeCosts.Length)
            upgradeCostTexts[4].text = speedUpgradeCosts[currentSpeedLevel].ToString();
        else
            upgradeCostTexts[4].text = "MAX";

        if (currentPickRadiusLevel < pickRadiusUpgradeCosts.Length)
            upgradeCostTexts[5].text = pickRadiusUpgradeCosts[currentPickRadiusLevel].ToString();
        else
            upgradeCostTexts[5].text = "MAX";

        if (currentCritChanceLevel < critChanceUpgradeCosts.Length)
            upgradeCostTexts[6].text = critChanceUpgradeCosts[currentCritChanceLevel].ToString();
        else
            upgradeCostTexts[6].text = "MAX";

        if (currentAbilityHasteLevel < abilityHasteUpgradeCosts.Length)
            upgradeCostTexts[7].text = abilityHasteUpgradeCosts[currentAbilityHasteLevel].ToString();
        else
            upgradeCostTexts[7].text = "MAX";

        if (currentExperienceBonusLevel < experienceBonusUpgradeCosts.Length)
            upgradeCostTexts[8].text = experienceBonusUpgradeCosts[currentExperienceBonusLevel].ToString();
        else
            upgradeCostTexts[8].text = "MAX";

        if (currentProjectilesLevel < projectilesUpgradeCosts.Length)
            upgradeCostTexts[9].text = projectilesUpgradeCosts[currentProjectilesLevel].ToString();
        else
            upgradeCostTexts[9].text = "MAX";

        if (currentGoldBonusLevel < goldBonusUpgradeCosts.Length)
            upgradeCostTexts[10].text = goldBonusUpgradeCosts[currentGoldBonusLevel].ToString();
        else
            upgradeCostTexts[10].text = "MAX";
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

