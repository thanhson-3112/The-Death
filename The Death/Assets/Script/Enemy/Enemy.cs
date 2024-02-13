using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    public Transform target;
    public float speed = 3f;
    public float rotateSpeed = 0.25f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
   
    void Update()
    {
        if (!target)
        {
            GetTarget();
        }
        else
        {
            anim.SetTrigger("SkeletonRun");
            RotateTowardsTarget();
        }
    }

    private void RotateTowardsTarget()
    {
        Vector3 targetDirection = target.position - transform.position;
        if (targetDirection.x > 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }


    private void GetTarget()
    {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Destroy(collision.gameObject);
            Time.timeScale = 0;
            target = null;
        }
        
    }
}
