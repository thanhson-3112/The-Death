using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ExperienceItem : MonoBehaviour
{
    public float moveSpeed = 30f;
    private Transform playerTransform;
    private bool isMoving = false;
    public int expAmount;

    public PlayerPower playerPower;

    private void Start()
    {
        Destroy(gameObject, 40f);
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        playerPower = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPower>();

    }

    protected void Update()
    {
        if (!isMoving && Vector3.Distance(transform.position, playerTransform.position) < playerPower.playerCurrentPickRadius)
        {
            isMoving = true;
        }

        if (isMoving)
        {
            MoveTowardsPlayer();
        }
    }
   
    private void MoveTowardsPlayer()
    {
        Vector3 direction = playerTransform.position - transform.position;
        direction.Normalize();

        transform.Translate(direction * moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, playerTransform.position) < 0.1f)
        {
            LootManager.Instance.AddExperience(expAmount);
            Destroy(gameObject);
        }
    }
}