using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldItem : MonoBehaviour
{
    public float moveSpeed = 30f;
    private Transform playerTransform;
    private bool isMoving = false;
    public int goldAmount;

    private void Start()
    {
        Destroy(gameObject, 40f);
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    protected void Update()
    {
        if (!isMoving && Vector3.Distance(transform.position, playerTransform.position) < PlayerPower.instance.playerCurrentPickRadius)
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
            LootManager.Instance.AddGold(goldAmount);
            Destroy(gameObject);
        }
    }
}