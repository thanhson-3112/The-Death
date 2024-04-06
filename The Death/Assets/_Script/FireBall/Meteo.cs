using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Meteo : MonoBehaviour
{
    [Range(1, 100)]
    [SerializeField] private float speed = 20f;

    [Range(1, 10)]
    [SerializeField] private float lifeTime = 3f;

    private float baseDamage = 70f;
    private float currentDamage;

    private Rigidbody2D rb;
    public GameObject explosionPrefab;
    private Animator anim;

    private GameObject targetEnemy; 
    private Vector2 targetPosition; 

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Destroy(gameObject, lifeTime);

        currentDamage = baseDamage;

        FindTargetEnemy();
    }

    private void FindTargetEnemy()
    {
        targetEnemy = GameObject.FindGameObjectWithTag("Enemy");

        if (targetEnemy != null)
        {
            targetPosition = targetEnemy.transform.position;
        }
    }

    private void FixedUpdate()
    {
        if (targetEnemy != null)
        {
            Vector2 moveDirection = (targetPosition - (Vector2)transform.position).normalized;

            // Flip ??i t??ng theo tr?c X n?u c?n
            if (moveDirection.x < 0f)
            {
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }

            rb.velocity = moveDirection * speed;
        }
    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Skeleton skeletonEnemy = collision.GetComponent<Skeleton>();
            if (skeletonEnemy != null)
            {
                skeletonEnemy.EnemyTakeDamage(currentDamage);
            }

            Goblin goblinEnemy = collision.GetComponent<Goblin>();
            if (goblinEnemy != null)
            {
                goblinEnemy.EnemyTakeDamage(currentDamage);
            }

            Archer archerEnemy = collision.GetComponent<Archer>();
            if (archerEnemy != null)
            {
                archerEnemy.EnemyTakeDamage(currentDamage);
            }

            Flying flyingEnemy = collision.GetComponent<Flying>();
            if (flyingEnemy != null)
            {
                flyingEnemy.EnemyTakeDamage(currentDamage);
            }

            BoDLifeController BoDEnemy = collision.GetComponent<BoDLifeController>();
            if (BoDEnemy != null)
            {
                BoDEnemy.EnemyTakeDamage(currentDamage);
            }

            Destroy(gameObject);
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }
    }
}
