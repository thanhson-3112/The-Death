using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private UIIventoryPage inventoryUI;

    public int inventorySize = 10;

    public void Start()
    {
        inventoryUI.InitializeInventoryUI(inventorySize);
    }


    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if(inventoryUI.isActiveAndEnabled == false)
            {
                inventoryUI.Show();
                
            }
            else
            {
                inventoryUI.Hide();
                
            }
        }
    }
}

        

