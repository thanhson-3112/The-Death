using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportGate : MonoBehaviour
{
    public TMP_Text displayText;

    private bool isPlayerInside = false;

    public void Start()
    {
        displayText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (isPlayerInside && Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Da an F");
            SceneManager.LoadScene(2);
        }

        SpawnPoint.instance.StopSpawnEnemy();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInside = true;
            displayText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInside = false;
            displayText.gameObject.SetActive(false);
        }
    }
}
