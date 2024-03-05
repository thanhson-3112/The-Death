using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UpgradeManager : MonoBehaviour
{
    Upgrade[] Upgrades = new Upgrade[]
    {
        new Upgrade{Name = "Attack Speed", Description = "Attack Speed + 2", Ratity = "Common",  Increase = 10},
        new Upgrade{Name = "Attack Damage", Description = "Attack Damage + 2", Ratity = "Common", Increase = 20},
        new Upgrade{Name = "Attack Damage2", Description = "Attack Damage + 5", Ratity = "Rare",  Increase = 20},
        new Upgrade{Name = "Dash Force", Description = "Dash Force +2", Ratity = "Rare",  Increase = 20},
        new Upgrade{Name = "Dash Force2", Description = "Dash Force +5", Ratity = "Epic",  Increase = 20}

    };

    private int upgradeCount = 3;
    private int previousLevel;

    [SerializeField] GameObject upgrade_prefab;
    [SerializeField] GameObject upgradeHorizontalLayout;
    /*[SerializeField] GameObject upgradeEneble;*/

    private void Start()
    {
        /*previousLevel = _currentLevel;*/
        ButtonsSet();
/*        upgradeEneble.SetActive(false);*/
    }


    /*protected override void Update()
    {
        if (_currentLevel > previousLevel)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            previousLevel = _currentLevel;
        }
    }*/
    public void ButtonsSet()
    {
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

            TextMeshProUGUI upgradeTextName = upgradeObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            upgradeTextName.text = upgrade.Name;

            TextMeshProUGUI upgradeTextDescription = upgradeObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            upgradeTextDescription.text = upgrade.Description;

            /*upgradeObject.transform.GetChild(1).GetComponent<Image>().color = rarityColors[upgrade.Ratity];
            upgradeObject.transform.GetChild(2).GetComponent<Image>().sprite = upgrade.Sprite;*/

            upgradeButton.GetComponent<Image>().color = rarityColors[upgrade.Ratity]; 
            
        }
        Time.timeScale = 0;
    }

    private void UpgradeChosen(string upgradeChosen)
    {
        /*Time.timeScale = 1;*/
        /*transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);*/
        /*upgradeEneble.SetActive(false);*/

        switch (upgradeChosen)
        {
            case "Attack Speed":
                Debug.Log("Attack Speed");
                break;
            case "Attack Damage":
                Debug.Log("Attack Damage");
                break;
            case "Attack Damage2":
                Debug.Log("Attack Damage 2");
                break;
            case "Dash Force":
                Debug.Log("Dash Force");
                break;
            case "Dash Force2":
                Debug.Log("Dash Force");
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
        /*public Sprite Sprite;*/
    } 
}
