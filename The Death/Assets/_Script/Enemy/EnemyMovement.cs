using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected Animator anim;

    public Transform target;

    [SerializeField] private float enemySpeed = 3f;
    public float _enemySpeed { get => enemySpeed; set => enemySpeed = value; }

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
            /*anim.SetTrigger("enemyRun");*/
            RotateTowardsTarget();
        }
        enemySpeed = _enemySpeed;
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
        transform.position = Vector2.MoveTowards(transform.position, target.position, enemySpeed * Time.deltaTime);
    }


    private void GetTarget()
    {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
        
    }
}