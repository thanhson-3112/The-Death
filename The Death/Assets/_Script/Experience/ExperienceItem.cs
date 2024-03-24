using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceItem : MonoBehaviour
{
    public float moveSpeed = 30f;
    private Transform playerTransform;
    private bool isMoving = false;
    private int expAmount = 100;
    public float autoMoveDistance = 5f;

    private void Start()
    {
        Destroy(gameObject, 40f);
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isMoving)
        {
            isMoving = true;
            ExperienceManager.Instance.AddExperience(expAmount);
            Destroy(gameObject);
        }
    }

    private void Update()
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
            ExperienceManager.Instance.AddExperience(expAmount);
            Destroy(gameObject);
        }
    }
}
