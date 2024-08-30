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
    }

    public void SetDescription(Sprite sprite, string name, string cost, float sliderValue, UnityEngine.Events.UnityAction buttonAction)
    {
        skillImage.gameObject.SetActive(true);
        skillImage.sprite = sprite; // C?p nh?t hình ?nh
        skillName.text = name;
        skillCost.text = cost;
        skillSlider.gameObject.SetActive(true);
        skillSlider.value = sliderValue;
        skillButton.gameObject.SetActive(true);

        // Gán s? ki?n cho nút button
        skillButton.onClick.RemoveAllListeners();
        if (buttonAction != null)
        {
            skillButton.onClick.AddListener(buttonAction);
        }
    }

}
