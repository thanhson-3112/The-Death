using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoDLifeController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    [SerializeField] protected float BoDMaxHealth = 1000f;
    [SerializeField] protected float BoDHealth;
    public float BoDDamage = 5f;

    public PlayerLife playerLife;
    public BossHealthBar BoDHealthBar;
    private bool isHealthBarVisible = false;

    public List<GameObject> weapons; 
    public GameObject finishLevelObject;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        playerLife = playerObject.GetComponent<PlayerLife>();

        BoDHealthBar = GameObject.FindGameObjectWithTag("BossHealthBar").GetComponent<BossHealthBar>();

        BoDHealth = BoDMaxHealth;
        BoDHealthBar.SetHealthBar();
    }

    void Update()
    {
    }

    public virtual void EnemyTakeDamage(float damage)
    {
        BoDHealth -= damage;
        anim.SetTrigger("BoDTakeHit");
        anim.SetTrigger("BoDRun");
        if (!isHealthBarVisible)
        {
            BoDHealthBar.SetMaxHealth(BoDMaxHealth);
            isHealthBarVisible = true;
        }

        BoDHealthBar.SetHealth(BoDHealth);
        if (BoDHealth <= 0)
        {
            BoDDie();
        }
    }

    void BoDDie()
    {
        rb.GetComponent<Collider2D>().enabled = false;
        rb.bodyType = RigidbodyType2D.Static;

        anim.SetBool("BoDDeath", true);

        int weaponsSpawned = 0;

        // mang de luu chi so cua vu khi da chon
        List<int> indices = new List<int>();

        // Spawn 3 v? khí t? danh sách
        while (weaponsSpawned < 3 && weapons.Count > 0)
        {
            int randomIndex = Random.Range(0, weapons.Count);

            // kiem tra xem chi so cua vu khi da duoc chon chua
            while (indices.Contains(randomIndex))
            {
                randomIndex = Random.Range(0, weapons.Count);
            }

            // lay vu khi tu chi so da chon
            GameObject weaponToSpawn = weapons[randomIndex];

            // SPawn vu khi ngau nhien quanh boss
            Vector2 randomSpawnOffset = Random.insideUnitCircle.normalized * 7f;
            Vector3 spawnPosition = transform.position + (Vector3)randomSpawnOffset;

            // Ki?m tra xem v? trí spawn có b? va ch?m không
            Collider2D[] colliders = Physics2D.OverlapCircleAll(spawnPosition, 0.5f);
            bool positionClear = true;
            foreach (Collider2D collider in colliders)
            {
                if (collider.gameObject.CompareTag("Item"))
                {
                    positionClear = false;
                    break;
                }
            }
            // Neu khong bi va tram thi spawn vu khi tiep theo
            if (positionClear)
            {
                Instantiate(weaponToSpawn, spawnPosition, Quaternion.identity);
                weaponsSpawned++;
                indices.Add(randomIndex); // them chi so da chon vao mang
            }
        }
        // spawn cong dich chuyen khi boss chet
        Instantiate(finishLevelObject, transform.position, Quaternion.identity);
        BoDHealthBar.SetHealthBar();
        Destroy(gameObject, 1f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerLife.TakeDamage(BoDDamage);
        }
    }

}
