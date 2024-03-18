using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIIventoryPage : MonoBehaviour
{
    [SerializeField] private UIInventoryItem itemPrefab;
    [SerializeField] private RectTransform contentPanel;

    List<UIInventoryItem> listOfUIItems = new List<UIInventoryItem>();

    private Vector3 initialScale;

    public void InitializeInventoryUI(int inventorysize)
    {
        initialScale = itemPrefab.transform.localScale;

        for (int i = 0; i < inventorysize; i++)
        {
            UIInventoryItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
            uiItem.transform.SetParent(contentPanel);
            // luu ti le cua item
            uiItem.transform.localScale = initialScale;
            listOfUIItems.Add(uiItem);
        }
    }

    public void Show()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0f;
    }
    public void Hide()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
}
