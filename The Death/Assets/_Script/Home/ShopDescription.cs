using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopDescription : MonoBehaviour
{
    [SerializeField]
    private Image skillImage;
    [SerializeField]
    private TMP_Text skillName;
    [SerializeField]
    private TMP_Text skillCost;
    [SerializeField]
    private Slider skillSlider;
    [SerializeField]
    private Button skillButton;

    [SerializeField]
    private TMP_Text upgradeValueCurrentText;
    [SerializeField]
    private TMP_Text upgradeValueNextText;

    public void Awake()
    {
        ResetDescription();
    }

    public void ResetDescription()
    {
        skillImage.gameObject.SetActive(false);
        skillName.text = "";
        skillCost.text = "";
        skillSlider.gameObject.SetActive(false);
        skillButton.gameObject.SetActive(false);
        upgradeValueCurrentText.text = "";
        upgradeValueNextText.text = "";
    }

    public void SetDescription(Sprite img, string name, string cost, float sliderValue, UnityEngine.Events.UnityAction buttonAction, string upgradeLevelValueText, string nextUpgradeValueText)
    {
        skillImage.gameObject.SetActive(true);
        skillImage.sprite = img;
        skillName.text = name;
        skillCost.text = cost;
        skillSlider.gameObject.SetActive(true);
        skillSlider.value = sliderValue;
        skillButton.gameObject.SetActive(buttonAction != null);
        skillButton.onClick.RemoveAllListeners();
        if (buttonAction != null)
        {
            skillButton.onClick.AddListener(buttonAction);
        }
        upgradeValueCurrentText.text = "" + upgradeLevelValueText;
        upgradeValueNextText.text = "" + nextUpgradeValueText;
    }
}