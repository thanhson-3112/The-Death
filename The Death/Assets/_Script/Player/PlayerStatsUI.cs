using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsUI : MonoBehaviour
{
    public GameObject playerStats;
    private float holdTimer = 1f;
    private bool isPlayerStatsActive = false;

    void Start()
    {
        playerStats.SetActive(false);

    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            holdTimer += Time.deltaTime;


            if (holdTimer >= 1f && !isPlayerStatsActive)
            {
                playerStats.SetActive(true);
                isPlayerStatsActive = true;
            }
        }

        if (Input.GetKeyUp(KeyCode.Tab))
        {
            holdTimer = 0f;
            playerStats.SetActive(false);
            isPlayerStatsActive = false;
        }
    }
}
