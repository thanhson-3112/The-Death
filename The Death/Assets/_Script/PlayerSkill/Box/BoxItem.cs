using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoxItem : MonoBehaviour
{
    public float moveSpeed = 30f;
    private Transform playerTransform;
    private bool isMoving = false;
    public float autoMoveDistance = 5f;
    public BoxItemSO itemData;  // Thêm thông tin v? v?t ph?m

    private PlayerLife playerLife;

    private void Start()
    {
        Destroy(gameObject, 40f);
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        playerLife = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLife>();
    }

    protected void Update()
    {
        if (!isMoving && Vector3.Distance(transform.position, playerTransform.position) < autoMoveDistance)
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
            ApplyItemEffect();  // G?i hàm t?ng s?c m?nh
            Destroy(gameObject);
        }
    }

    private void ApplyItemEffect()
    {
        switch (itemData.boxItemType)
        {
            case BoxItemType.Health:
                playerLife.health += 10f;
                break;
            case BoxItemType.Armor:
                PlayerPower.instance.playerCurrentArmor += 5f;
                break;
            case BoxItemType.Speed:
                PlayerPower.instance.playerCurrentSpeed += 1f;
                break;
        }
    }
}
