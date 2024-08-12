using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UpgradeManager : MonoBehaviour
{
    Upgrade[] Upgrades;

    [SerializeField]private int upgradeCount = 3;

    [SerializeField] GameObject upgrade_prefab;
    [SerializeField] GameObject upgradeHorizontalLayout;

    [Header("")]
    [SerializeField] PlayerPower fireBallDamage;
    [SerializeField] PlayerAttack playerAttackSpeed;
    [SerializeField] UnlockMeteo unlockMeteo;

    private PlayerPower playerPower;

    public void Start()
    {
        playerPower = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPower>();


        Upgrades = new Upgrade[]
        {
        new Upgrade{Name = "Attack Speed", Description = "Attack Speed +0.1", Ratity = "Common",  Increase = 10, Sprite = Resources.Load<Sprite>("Upgrade_Card/Attack_Speed")},
        new Upgrade{Name = "Attack Damage", Description = "Attack Damage +2", Ratity = "Common", Increase = 20, Sprite = Resources.Load<Sprite>("Upgrade_Card/Attack_Damage")},
        new Upgrade{Name = "Attack Damage 2", Description = "Attack Damage +5", Ratity = "Rare",  Increase = 20, Sprite = Resources.Load<Sprite>("Upgrade_Card/Attack_Damage")},
        new Upgrade{Name = "Dash Force", Description = "Dash Force +1", Ratity = "Rare",  Increase = 20, Sprite = Resources.Load<Sprite>("Upgrade_Card/Dash_Force")},
        new Upgrade{Name = "Dash Force 2", Description = "Dash Force +3", Ratity = "Epic",  Increase = 20, Sprite = Resources.Load<Sprite>("Upgrade_Card/Dash_Force")},
        new Upgrade{Name = "Meteo", Description = "Meteo +3", Ratity = "Epic",  Increase = 1, Sprite = Resources.Load<Sprite>("Upgrade_Card/Meteo")}


        };
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


            Debug.Log("Sprite path: " + "Upgrade_Card/" + upgrade.Sprite);
        }
    
        Time.timeScale = 0;
    }

    private void UpgradeChosen(string upgradeChosen)
    {
        Time.timeScale = 1;
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);


        switch (upgradeChosen)
        {
            case "Attack Speed":
                Debug.Log("Attack Speed");
                playerAttackSpeed.UpgradeAttackSpeed();
                break;
            case "Attack Damage":
                Debug.Log("Attack Damage");
                fireBallDamage.PlayerDamageUpgrade();
                break;
            case "Attack Damage 2":
                Debug.Log("Attack Damage 2");
                fireBallDamage.PlayerDamageUpgrade2();
                break;
            case "Dash Force":
                Debug.Log("Dash Force");
                playerPower.DashUpgrade();
                break;
            case "Dash Force 2":
                Debug.Log("Dash Force 2");
                playerPower.DashUpgrade2();
                break;
            case "Meteo":
                Debug.Log("Meteo");
                unlockMeteo.UnlockSkillMeteo();
                break;
        }
    }

    private void ShuffleList(List<int> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex = Random.Range(i, list.Count);
            int temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }



    public class Upgrade
    {
        public string Name { get; set;}
        public string Description { get; set;}
        public string Ratity { get; set;}
        public float Increase {get; set;}
        public Sprite Sprite { get; set; }
    } 
}
