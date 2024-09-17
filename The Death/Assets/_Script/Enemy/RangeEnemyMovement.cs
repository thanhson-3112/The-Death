using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangeEnemyMovement : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected Animator anim;

/*    public Transform target;*/

    [SerializeField] private float enemySpeed = 3f;
    public float _enemySpeed { get => enemySpeed; set => enemySpeed = value; }
    public float rotateSpeed = 0.25f;

    public float distanceToShoot = 20f;
    public float distanceToStop = 3f;

    // Fire
    [Header("FireBall")]
    [SerializeField] private GameObject firePrefab;
    [SerializeField] private GameObject firingPoint;
    [SerializeField] private Transform firing;
    [Range(0.1f, 2f)]
    [SerializeField] private float fireRate = 0.8f;
    private bool canShoot = true;

    [SerializeField] Transform target;
    NavMeshAgent agent;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

/*        target = GameObject.FindGameObjectWithTag("Player").transform;*/
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update()
    {
        if (!target)
        {
            GetTarget();
        }
        else
        {
            anim.SetTrigger("EnemyRun");
            RotateTowardsTarget();

            // Kiểm tra khoảng cách giữa quái và người chơi
            float distanceToPlayer = Vector2.Distance(target.position, transform.position);
            if (distanceToPlayer <= 20f)
            {
                ArcherShoot();
            }
        }
        agent.SetDestination(target.position);
    }

    public void ArcherShoot()
    {
        if (canShoot)
        {
            // Tìm tất cả các người chơi
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject player in players)
            {
                float distance = Vector2.Distance(transform.position, player.transform.position);
                if (distance <= 20f)
                {
                    // Nếu người chơi nằm trong khoảng cách distanceToShoot, tấn công
                    Vector2 direction = player.transform.position - transform.position;
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                    Instantiate(firePrefab, firing.position, Quaternion.Euler(0, 0, angle));
                    anim.SetTrigger("ArcherAttack");
                    canShoot = false;
                    StartCoroutine(ResetShootCooldown());
                    break; 
                }
            }
        }
    }

    private IEnumerator ResetShootCooldown()
    {
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
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
        
    }

    private void GetTarget()
    {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }
}
