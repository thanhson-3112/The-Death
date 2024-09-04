using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeManager : MonoBehaviour
{
    public class Upgrade
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Ratity { get; set; }
        public float Increase { get; set; }
        public Sprite Sprite { get; set; }
    }

    Upgrade[] Upgrades;

    [SerializeField] private int upgradeCount = 3;

    [SerializeField] GameObject upgrade_prefab;
    [SerializeField] GameObject upgradeHorizontalLayout;

    [Header("")]
    [SerializeField] PlayerPower fireBallDamage;
    [SerializeField] PlayerAttack playerAttackSpeed;
    [SerializeField] UnlockMeteo unlockMeteo;

    private PlayerPower playerPower;

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

    // L?u c?p ?? hi?n t?i c?a t?ng nâng c?p
    private Dictionary<string, int> upgradeLevels = new Dictionary<string, int>();

    public void Start()
    {
        playerPower = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPower>();

        Upgrades = new Upgrade[]
        {
            new Upgrade{Name = "Damage", Description = "Increase base damage", Ratity = "Common",  Increase = 10, Sprite = Resources.Load<Sprite>("Skill/Damage")},
            new Upgrade{Name = "Armor", Description = "Increase base armor", Ratity = "Common", Increase = 20, Sprite = Resources.Load<Sprite>("Skill/Armor")},
            new Upgrade{Name = "Max Health", Description = "Increase max health", Ratity = "Rare",  Increase = 20, Sprite = Resources.Load<Sprite>("Skill/MaxHealth")},
            new Upgrade{Name = "Health Regen", Description = "Increase health regeneration", Ratity = "Rare",  Increase = 20, Sprite = Resources.Load<Sprite>("Skill/HealthRegen")},
            new Upgrade{Name = "Speed", Description = "Increase movement speed", Ratity = "Epic",  Increase = 20, Sprite = Resources.Load<Sprite>("Skill/Speed")},
            new Upgrade{Name = "Pick Radius", Description = "Increase pick radius", Ratity = "Epic",  Increase = 20, Sprite = Resources.Load<Sprite>("Skill/PickRadius")},
            new Upgrade{Name = "Crit Chance", Description = "Increase critical hit chance", Ratity = "Epic",  Increase = 20, Sprite = Resources.Load<Sprite>("Skill/CritChance")},
            new Upgrade{Name = "Ability Haste", Description = "Increase ability haste", Ratity = "Epic",  Increase = 20, Sprite = Resources.Load<Sprite>("Skill/AbilityHaste")},
            new Upgrade{Name = "Experience", Description = "Increase experience gain", Ratity = "Epic",  Increase = 20, Sprite = Resources.Load<Sprite>("Skill/Experience")},
            new Upgrade{Name = "Projectiles", Description = "Increase number of projectiles", Ratity = "Epic",  Increase = 1, Sprite = Resources.Load<Sprite>("Skill/Projectiles")},
            new Upgrade { Name = "Gold", Description = "Increase gold gain", Ratity = "Epic", Increase = 1, Sprite = Resources.Load<Sprite>("Skill/Gold") }
        };

        // Kh?i t?o c?p ?? cho t?ng nâng c?p
        foreach (var upgrade in Upgrades)
        {
            upgradeLevels[upgrade.Name] = 0;
        }

        ButtonsSet();
    }

    public void ButtonsSet()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(true);

        List<int> availableUpgrades = new List<int>();
        for (int i = 0; i < Upgrades.Length; i++)
        {
            availableUpgrades.Add(i);
        }
        ShuffleList(availableUpgrades);

        while (upgradeHorizontalLayout.transform.childCount < upgradeCount)
        {
            Instantiate(upgrade_prefab, upgradeHorizontalLayout.transform);
        }

        Dictionary<string, Color> rarityColors = new Dictionary<string, Color>();
        rarityColors.Add("Common", new Color(1, 1, 1, 1));
        rarityColors.Add("Rare", new Color(0.5f, 1, 0.5f, 1));
        rarityColors.Add("Epic", new Color(0.75f, 0.25f, 0.75f, 1));

        for (int i = 0; i < upgradeCount; i++)
        {
            Upgrade upgrade = Upgrades[availableUpgrades[i]];

            GameObject upgradeObject = upgradeHorizontalLayout.transform.GetChild(i).gameObject;
            Button upgradeButton = upgradeObject.GetComponent<Button>();
            upgradeButton.onClick.AddListener(() => { UpgradeChosen(upgrade.Name); });

            TextMeshProUGUI upgradeTextName = upgradeObject.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
            upgradeTextName.text = upgrade.Name;

            TextMeshProUGUI upgradeTextDescription = upgradeObject.transform.GetChild(4).GetComponent<TextMeshProUGUI>();
            upgradeTextDescription.text = upgrade.Description;

            upgradeObject.transform.GetChild(1).GetComponent<Image>().color = rarityColors[upgrade.Ratity];
            upgradeObject.transform.GetChild(2).GetComponent<Image>().sprite = upgrade.Sprite;

            Debug.Log("Sprite path: " + "Skill/" + upgrade.Sprite);
        }

        Time.timeScale = 0;
    }

    private void UpgradeChosen(string upgradeChosen)
    {
        int currentLevel = upgradeLevels[upgradeChosen];  // L?y c?p ?? hi?n t?i
        Time.timeScale = 1;
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);

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
            case "Experience":
                PlayerPower.instance.playerCurrentExperienceBonus += experienceBonusUpgradeValues[currentLevel];
                break;
            case "Projectiles":
                PlayerPower.instance.playerCurrentProjectiles += projectilesUpgradeValues[currentLevel];
                break;
            case "Gold":
                PlayerPower.instance.playerCurrentGoldBonus += goldBonusUpgradeValues[currentLevel];
                break;
            case "Meteo":
                unlockMeteo.UnlockSkillMeteo();
                break;
            default:
                break;
        }

        upgradeLevels[upgradeChosen]++;  // T?ng c?p ?? sau khi nâng c?p
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
