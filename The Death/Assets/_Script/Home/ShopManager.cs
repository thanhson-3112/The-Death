using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private Button upgradeDamageButton;
    [SerializeField] private Button upgradeArmorButton;
    [SerializeField] private Button upgradeHealthButton;

    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private PlayerGold playerGold;

    [SerializeField] private Slider damageSlider;
    [SerializeField] private Slider armorSlider;
    [SerializeField] private Slider healthSlider;

    private int maxUpgradeLevel = 4; // M?i thanh ch? c� th? ??y 4 v?ch

    private ShopNPC currentShopNPC;

    private void Start()
    {
        // G?n s? ki?n cho c�c button
        upgradeDamageButton.onClick.AddListener(() => UpgradeStat("Damage", 100, damageSlider));
        upgradeArmorButton.onClick.AddListener(() => UpgradeStat("Armor", 200, armorSlider));
        upgradeHealthButton.onClick.AddListener(() => UpgradeStat("MaxHealth", 300, healthSlider));

        // C?p nh?t UI v�ng
        UpdateGoldUI();

        // Ban ??u ?n UI shop
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

    private void UpgradeStat(string statName, int requiredGold, Slider upgradeSlider)
    {
        if (playerGold.goldTotal < requiredGold)
        {
            Debug.Log("Kh�ng ?? v�ng.");
            return;
        }

        if (upgradeSlider.value >= maxUpgradeLevel)
        {
            Debug.Log("?� ??t c?p t?i ?a cho " + statName);
            return;
        }

        // Tr? v�ng
        playerGold.goldTotal -= requiredGold;

        // N�ng c?p ch? s? t??ng ?ng
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
        }

        // T?ng gi� tr? c?a Slider
        upgradeSlider.value += 1;

        // C?p nh?t UI v�ng
        UpdateGoldUI();
    }

    private void UpdateGoldUI()
    {
        goldText.text = "Gold: " + playerGold.goldTotal.ToString();
    }
}
