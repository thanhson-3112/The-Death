using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
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

    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private PlayerGold playerGold;

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

    private int currentDamageUpgradeCost = 100;
    private int currentArmorUpgradeCost = 200;
    private int currentMaxHealthUpgradeCost = 300;
    private int currentHealthRegenUpgradeCost = 150;
    private int currentSpeedUpgradeCost = 120;
    private int currentPickRadiusUpgradeCost = 180;
    private int currentCritChanceUpgradeCost = 250;
    private int currentAbilityHasteUpgradeCost = 140;
    private int currentExperienceBonusUpgradeCost = 220;
    private int currentProjectilesUpgradeCost = 160;
    private int currentGoldBonusUpgradeCost = 200;

    private float damageUpgradeMultiplier = 1.5f;
    private float armorUpgradeMultiplier = 1.4f;
    private float maxHealthUpgradeMultiplier = 1.6f;
    private float healthRegenUpgradeMultiplier = 1.3f;
    private float speedUpgradeMultiplier = 1.2f;
    private float pickRadiusUpgradeMultiplier = 1.5f;
    private float critChanceUpgradeMultiplier = 1.4f;
    private float abilityHasteUpgradeMultiplier = 1.3f;
    private float experienceBonusUpgradeMultiplier = 1.7f;
    private float projectilesUpgradeMultiplier = 1.4f;
    private float goldBonusUpgradeMultiplier = 1.5f;

    private int maxDamageUpgradeLevel = 5;
    private int maxArmorUpgradeLevel = 4;
    private int maxMaxHealthUpgradeLevel = 6;
    private int maxHealthRegenUpgradeLevel = 5;
    private int maxSpeedUpgradeLevel = 4;
    private int maxPickRadiusUpgradeLevel = 3;
    private int maxCritChanceUpgradeLevel = 4;
    private int maxAbilityHasteUpgradeLevel = 3;
    private int maxExperienceBonusUpgradeLevel = 3;
    private int maxProjectilesUpgradeLevel = 2;
    private int maxGoldBonusUpgradeLevel = 4;

    private ShopNPC currentShopNPC;

    private void Start()
    {
        // Assign button click events
        upgradeDamageButton.onClick.AddListener(() => UpgradeStat("Damage", ref currentDamageUpgradeCost, ref damageUpgradeMultiplier, damageSlider, damageCostText, maxDamageUpgradeLevel));
        upgradeArmorButton.onClick.AddListener(() => UpgradeStat("Armor", ref currentArmorUpgradeCost, ref armorUpgradeMultiplier, armorSlider, armorCostText, maxArmorUpgradeLevel));
        upgradeMaxHealthButton.onClick.AddListener(() => UpgradeStat("MaxHealth", ref currentMaxHealthUpgradeCost, ref maxHealthUpgradeMultiplier, maxHealthSlider, maxHealthCostText, maxMaxHealthUpgradeLevel));
        upgradeHealthRegenButton.onClick.AddListener(() => UpgradeStat("HealthRegen", ref currentHealthRegenUpgradeCost, ref healthRegenUpgradeMultiplier, healthRegenSlider, healthRegenCostText, maxHealthRegenUpgradeLevel));
        upgradeSpeedButton.onClick.AddListener(() => UpgradeStat("Speed", ref currentSpeedUpgradeCost, ref speedUpgradeMultiplier, speedSlider, speedCostText, maxSpeedUpgradeLevel));
        upgradePickRadiusButton.onClick.AddListener(() => UpgradeStat("PickRadius", ref currentPickRadiusUpgradeCost, ref pickRadiusUpgradeMultiplier, pickRadiusSlider, pickRadiusCostText, maxPickRadiusUpgradeLevel));
        upgradeCritChanceButton.onClick.AddListener(() => UpgradeStat("CritChance", ref currentCritChanceUpgradeCost, ref critChanceUpgradeMultiplier, critChanceSlider, critChanceCostText, maxCritChanceUpgradeLevel));
        upgradeAbilityHasteButton.onClick.AddListener(() => UpgradeStat("AbilityHaste", ref currentAbilityHasteUpgradeCost, ref abilityHasteUpgradeMultiplier, abilityHasteSlider, abilityHasteCostText, maxAbilityHasteUpgradeLevel));
        upgradeExperienceBonusButton.onClick.AddListener(() => UpgradeStat("ExperienceBonus", ref currentExperienceBonusUpgradeCost, ref experienceBonusUpgradeMultiplier, experienceBonusSlider, experienceBonusCostText, maxExperienceBonusUpgradeLevel));
        upgradeProjectilesButton.onClick.AddListener(() => UpgradeStat("Projectiles", ref currentProjectilesUpgradeCost, ref projectilesUpgradeMultiplier, projectilesSlider, projectilesCostText, maxProjectilesUpgradeLevel));
        upgradeGoldBonusButton.onClick.AddListener(() => UpgradeStat("GoldBonus", ref currentGoldBonusUpgradeCost, ref goldBonusUpgradeMultiplier, goldBonusSlider, goldBonusCostText, maxGoldBonusUpgradeLevel));

        // Update UI with initial values
        UpdateGoldUI();
        UpdateCostTexts();

        // Initially hide the shop UI
        gameObject.SetActive(false);
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

    private void UpgradeStat(string statName, ref int currentUpgradeCost, ref float upgradeMultiplier, Slider upgradeSlider, TextMeshProUGUI costText, int maxUpgradeLevel)
    {
        if (playerGold.goldTotal < currentUpgradeCost)
        {
            Debug.Log("Không ?? vàng.");
            return;
        }

        if (upgradeSlider.value >= maxUpgradeLevel)
        {
            Debug.Log("?ã ??t c?p t?i ?a cho " + statName);
            return;
        }

        // Deduct gold
        playerGold.goldTotal -= currentUpgradeCost;

        // Upgrade the corresponding stat
        switch (statName)
        {
            case "Damage":
                PlayerPower.instance.playerBaseDamage += 1;
                break;
            case "Armor":
                PlayerPower.instance.playerBaseArmor += 5;
                break;
            case "MaxHealth":
                PlayerPower.instance.playerBaseMaxHealth += 10;
                break;
            case "HealthRegen":
                PlayerPower.instance.playerBaseHealthRegen += 0.1f;
                break;
            case "Speed":
                PlayerPower.instance.playerBaseSpeed += 1;
                break;
            case "PickRadius":
                PlayerPower.instance.playerBasePickRadius += 1;
                break;
            case "CritChance":
                PlayerPower.instance.playerBaseCritChance += 2;
                break;
            case "AbilityHaste":
                PlayerPower.instance.playerBaseAbilityHaste += 0.1f;
                break;
            case "ExperienceBonus":
                PlayerPower.instance.playerBaseExperienceBonus += 5;
                break;
            case "Projectiles":
                PlayerPower.instance.playerBaseProjectiles += 1;
                break;
            case "GoldBonus":
                PlayerPower.instance.playerBaseGoldBonus += 1;
                break;
        }

        // Increase the slider value
        upgradeSlider.value += 1;

        // Update the upgrade cost for the next level
        currentUpgradeCost = Mathf.RoundToInt(currentUpgradeCost * upgradeMultiplier);

        // Update UI with new values
        UpdateGoldUI();
        UpdateCostTexts();
    }

    private void UpdateGoldUI()
    {
        goldText.text = "Gold: " + playerGold.goldTotal.ToString();
    }

    private void UpdateCostTexts()
    {
        damageCostText.text = "Cost: " + currentDamageUpgradeCost.ToString();
        armorCostText.text = "Cost: " + currentArmorUpgradeCost.ToString();
        maxHealthCostText.text = "Cost: " + currentMaxHealthUpgradeCost.ToString();
        healthRegenCostText.text = "Cost: " + currentHealthRegenUpgradeCost.ToString();
        speedCostText.text = "Cost: " + currentSpeedUpgradeCost.ToString();
        pickRadiusCostText.text = "Cost: " + currentPickRadiusUpgradeCost.ToString();
        critChanceCostText.text = "Cost: " + currentCritChanceUpgradeCost.ToString();
        abilityHasteCostText.text = "Cost: " + currentAbilityHasteUpgradeCost.ToString();
        experienceBonusCostText.text = "Cost: " + currentExperienceBonusUpgradeCost.ToString();
        projectilesCostText.text = "Cost: " + currentProjectilesUpgradeCost.ToString();
        goldBonusCostText.text = "Cost: " + currentGoldBonusUpgradeCost.ToString();
    }
}
