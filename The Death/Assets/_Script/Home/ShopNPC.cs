using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopNPC : MonoBehaviour
{
    [SerializeField] private ShopManager shopManager;

    public GameObject text;
    private bool isPlayerInside = false;
    private bool isShopOpen = false;

    private void Start()
    {
        text.gameObject.SetActive(false);
/*        shopManager = GameObject.FindGameObjectWithTag("ShopManager").GetComponent<ShopManager>();
*/    }

    private void Update()
    {
        if (isPlayerInside && Input.GetKeyDown(KeyCode.F) && !isShopOpen)
        {
            Debug.Log("Da an F");
            shopManager.SetCurrentShopNPC(this);
            shopManager.ShopOpen();
            isShopOpen = true;
        }

        if (isPlayerInside && Input.GetKeyDown(KeyCode.Escape))
        {
            shopManager.ShopClose();
            isShopOpen = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInside = true;
            text.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInside = false;
            text.gameObject.SetActive(false);
            shopManager.ShopClose();
            isShopOpen = false;
        }
    }
}
