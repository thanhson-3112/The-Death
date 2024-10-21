using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    public class Upgrade
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Rarity { get; set; }
        public Sprite Sprite { get; set; }
        public float SpawnRate { get; set; }  // Thêm t? l? xu?t hi?n
    }

    Upgrade[] Upgrades;

    [SerializeField] private int upgradeCount = 3;
    [SerializeField] GameObject upgrade_prefab;
    [SerializeField] GameObject upgradeHorizontalLayout;

    [Header("")]
    [SerializeField] UnlockPlayerSkill unlockMeteo;

    private int[] damageUpgradeValues = { 1, 3, 5, 7, 10 };
    private int[] armorUpgradeValues = { 5, 10, 15, 20, 25, 30 };
    private int[] maxHealthUpgradeValues = { 10, 20, 30, 40, 50 };
    private float[] healthRegenUpgradeValues = { 0.5f, 1.0f, 1.5f, 2.0f, 2.5f };
    private float[] speedUpgradeValues = { 0.1f, 0.2f, 0.3f, 0.4f };
    private float[] pickRadiusUpgradeValues = { 0.5f, 1.0f, 1.5f };
    private float[] critChanceUpgradeValues = { 0.02f, 0.04f, 0.06f, 0.08f };
    private float[] abilityHasteUpgradeValues = { 0.05f, 0.10f, 0.15f, 0.20f };
    private int[] projectilesUpgradeValues = { 1, 2 };

    private int[] meteoUpgradeValues = { 10, 20, 30 };
    private int[] fireSwordUpgradeValues = { 10 ,20, 30 };
    private int[] lightningUpgradeValues = { 10, 20, 30 };
    private int[] fireGunUpgradeValues = { 10, 20, 30 };

    private Dictionary<string, int> upgradeLevels = new Dictionary<string, int>();

    public void Start()
    {
        Upgrades = new Upgrade[]
        {
            new Upgrade{Name = "Damage", Description = "Increase base damage", Rarity = "Common", Sprite = Resources.Load<Sprite>("Skill/Damage"), SpawnRate = 0.1f},
            new Upgrade{Name = "Armor", Description = "Increase base armor", Rarity = "Common", Sprite = Resources.Load<Sprite>("Skill/Armor"), SpawnRate = 0.1f},
            new Upgrade{Name = "Max Health", Description = "Increase max health", Rarity = "Common", Sprite = Resources.Load<Sprite>("Skill/MaxHealth"), SpawnRate = 0.1f},
            new Upgrade{Name = "Health Regen", Description = "Increase health regeneration", Rarity = "Common", Sprite = Resources.Load<Sprite>("Skill/HealthRegen"), SpawnRate = 0.1f},
            new Upgrade{Name = "Speed", Description = "Increase movement speed", Rarity = "Common", Sprite = Resources.Load<Sprite>("Skill/Speed"), SpawnRate = 0.1f},
            new Upgrade{Name = "Pick Radius", Description = "Increase pick radius", Rarity = "Rare", Sprite = Resources.Load<Sprite>("Skill/PickRadius"), SpawnRate = 0.05f},
            new Upgrade{Name = "Crit Chance", Description = "Increase critical hit chance", Rarity = "Rare", Sprite = Resources.Load<Sprite>("Skill/CritChance"), SpawnRate = 0.05f},
            new Upgrade{Name = "Ability Haste", Description = "Increase ability haste", Rarity = "Epic", Sprite = Resources.Load<Sprite>("Skill/AbilityHaste"), SpawnRate = 0.03f},
            new Upgrade{Name = "Projectiles", Description = "Increase number of projectiles", Rarity = "Epic", Sprite = Resources.Load<Sprite>("Skill/Projectiles"), SpawnRate = 0.03f},

            new Upgrade{Name = "Meteo", Description = "Increase number of Meteo", Rarity = "Epic", Sprite = Resources.Load<Sprite>("Skill/Meteo"), SpawnRate = 0.1f},
            new Upgrade{Name = "FireSword", Description = "Increase number of FireSword", Rarity = "Epic", Sprite = Resources.Load<Sprite>("Skill/FireSword"), SpawnRate = 0.03f},
            new Upgrade{Name = "Lightning", Description = "Increase number of Lightning", Rarity = "Rare", Sprite = Resources.Load<Sprite>("Skill/Lightning"), SpawnRate = 0.05f},
            new Upgrade{Name = "FireGun", Description = "Increase number of FireGun", Rarity = "Rare", Sprite = Resources.Load<Sprite>("Skill/FireGun"), SpawnRate = 0.05f},

        };

        foreach (var upgrade in Upgrades)
        {
            upgradeLevels[upgrade.Name] = 0;
        }

        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
    }

    public void ButtonsSet()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(true);

        List<int> availableUpgrades = new List<int>();
        float totalSpawnRate = 0f;
        foreach (var upgrade in Upgrades)
        {
            if (upgradeLevels[upgrade.Name] < GetMaxUpgradeLevel(upgrade.Name))
            {
                totalSpawnRate += upgrade.SpawnRate;
            }
        }

        // Determine available upgrades based on SpawnRate
        for (int i = 0; i < Upgrades.Length; i++)
        {
            if (upgradeLevels[Upgrades[i].Name] < GetMaxUpgradeLevel(Upgrades[i].Name))
            {
                float randomValue = Random.value * totalSpawnRate;

                if (randomValue <= Upgrades[i].SpawnRate)
                {
                    availableUpgrades.Add(i);
                }

                totalSpawnRate -= Upgrades[i].SpawnRate;
            }
        }

        ShuffleList(availableUpgrades);

        // Instantiate upgrade buttons
        while (upgradeHorizontalLayout.transform.childCount < upgradeCount)
        {
            Instantiate(upgrade_prefab, upgradeHorizontalLayout.transform);
        }

        Dictionary<string, Color> rarityColors = new Dictionary<string, Color>
        {
            { "Common", Color.white },
            { "Rare", new Color(0.5f, 1f, 0.5f, 1f) },
            { "Epic", new Color(0.75f, 0.25f, 0.75f, 1f) }
        };

        for (int i = 0; i < upgradeCount && i < availableUpgrades.Count; i++)
        {
            Upgrade upgrade = Upgrades[availableUpgrades[i]];
            GameObject upgradeObject = upgradeHorizontalLayout.transform.GetChild(i).gameObject;
            Button upgradeButton = upgradeObject.GetComponent<Button>();
            upgradeButton.onClick.RemoveAllListeners();
            upgradeButton.onClick.AddListener(() => UpgradeChosen(upgrade.Name));

            upgradeObject.transform.GetChild(1).GetComponent<Image>().color = rarityColors[upgrade.Rarity];
            upgradeObject.transform.GetChild(2).GetComponent<Image>().sprite = upgrade.Sprite;

            int currentLevel = upgradeLevels[upgrade.Name];
            TextMeshProUGUI upgradeTextLevel = upgradeObject.transform.GetChild(4).GetComponent<TextMeshProUGUI>();
            upgradeTextLevel.text = currentLevel >= GetMaxUpgradeLevel(upgrade.Name) - 1 ? "Max" : $"Level: {currentLevel + 1}";

            TextMeshProUGUI upgradeTextValue = upgradeObject.transform.GetChild(6).GetComponent<TextMeshProUGUI>();
            upgradeTextValue.text = $"Value: {GetUpgradeValue(upgrade.Name, currentLevel)}";

            TextMeshProUGUI upgradeTextName = upgradeObject.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
            upgradeTextName.text = upgrade.Name;

            TextMeshProUGUI upgradeTextDescription = upgradeObject.transform.GetChild(5).GetComponent<TextMeshProUGUI>();
            upgradeTextDescription.text = upgrade.Description;
        }

        Time.timeScale = 0;
    }




    private void UpgradeChosen(string upgradeChosen)
    {
        int currentLevel = upgradeLevels[upgradeChosen];

        switch (upgradeChosen)
        {
            case "Damage":
                PlayerPower.instance.playerCurrentDamage += damageUpgradeValues[currentLevel];
                break;
            case "Armor":
                PlayerPower.instance.playerCurrentArmor += armorUpgradeValues[currentLevel];
                break;
            case "Max Health":
                PlayerPower.instance.playerCurrentMaxHealth += maxHealthUpgradeValues[currentLevel];
                break;
            case "Health Regen":
                PlayerPower.instance.playerCurrentHealthRegen += healthRegenUpgradeValues[currentLevel];
                break;
            case "Speed":
                PlayerPower.instance.playerCurrentSpeed += speedUpgradeValues[currentLevel];
                break;
            case "Pick Radius":
                PlayerPower.instance.playerCurrentPickRadius += pickRadiusUpgradeValues[currentLevel];
                break;
            case "Crit Chance":
                PlayerPower.instance.playerCurrentCritChance += critChanceUpgradeValues[currentLevel];
                break;
            case "Ability Haste":
                PlayerPower.instance.playerCurrentAbilityHaste -= abilityHasteUpgradeValues[currentLevel];
                break;
            case "Projectiles":
                PlayerPower.instance.playerCurrentProjectiles += projectilesUpgradeValues[currentLevel];
                break;

            case "Meteo":
                PlayerPower.instance.CurrentMeteoDamage += meteoUpgradeValues[currentLevel];
                UnlockPlayerSkill.Instance.UnlockSkillMeteo();
                break;
            case "FireSword":
                PlayerPower.instance.CurrentFireSwordDamage += fireSwordUpgradeValues[currentLevel];
                UnlockPlayerSkill.Instance.UnlockSkillFireSword();
                break;
            case "Lightning":
                PlayerPower.instance.CurrentLightningDamage += lightningUpgradeValues[currentLevel];
                UnlockPlayerSkill.Instance.UnlockSkillLightning();
                break;
            case "FireGun":
                PlayerPower.instance.CurrentFireGunDamage += fireGunUpgradeValues[currentLevel];
                UnlockPlayerSkill.Instance.UnlockSkillFireGun();
                break;
        }

        upgradeLevels[upgradeChosen]++;
        Time.timeScale = 1;  // Ti?p t?c game sau khi ch?n nâng c?p
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);

    }

    private int GetMaxUpgradeLevel(string upgradeName)
    {
        switch (upgradeName)
        {
            case "Damage":
                return damageUpgradeValues.Length;
            case "Armor":
                return armorUpgradeValues.Length;
            case "Max Health":
                return maxHealthUpgradeValues.Length;
            case "Health Regen":
                return healthRegenUpgradeValues.Length;
            case "Speed":
                return speedUpgradeValues.Length;
            case "Pick Radius":
                return pickRadiusUpgradeValues.Length;
            case "Crit Chance":
                return critChanceUpgradeValues.Length;
            case "Ability Haste":
                return abilityHasteUpgradeValues.Length;
            case "Projectiles":
                return projectilesUpgradeValues.Length;

            case "Meteo":
                return meteoUpgradeValues.Length;
            case "FireSword":
                return fireSwordUpgradeValues.Length;
            case "Lightning":
                return lightningUpgradeValues.Length;
            case "FireGun":
                return fireGunUpgradeValues.Length;
            default:
                return 0;
        }
    }

    private float GetUpgradeValue(string upgradeName, int level)
    {
        switch (upgradeName)
        {
            case "Damage":
                return damageUpgradeValues[level];
            case "Armor":
                return armorUpgradeValues[level];
            case "Max Health":
                return maxHealthUpgradeValues[level];
            case "Health Regen":
                return healthRegenUpgradeValues[level];
            case "Speed":
                return speedUpgradeValues[level];
            case "Pick Radius":
                return pickRadiusUpgradeValues[level];
            case "Crit Chance":
                return critChanceUpgradeValues[level];
            case "Ability Haste":
                return abilityHasteUpgradeValues[level];
            case "Projectiles":
                return projectilesUpgradeValues[level];

            case "Meteo":
                return meteoUpgradeValues[level];
            case "FireSword":
                return fireSwordUpgradeValues[level];
            case "Lightning":
                return lightningUpgradeValues[level];
            case "FireGun":
                return fireGunUpgradeValues[level];
            default:
                return 0;
        }
    }

    private void ShuffleList(List<int> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}
