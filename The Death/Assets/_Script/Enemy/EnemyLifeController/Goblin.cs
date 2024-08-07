using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Goblin : EnemyLifeBase
{
    [SerializeField] protected float goblinMaxHealth = 200f;
    [SerializeField] protected float goblinHealth;
    public float goblinDamage = 5f;



    public override void Start()
    {
        goblinHealth = goblinMaxHealth;
        enemyMaxHealth = goblinMaxHealth;
        enemyHealth = goblinHealth;
        enemyDamage = goblinDamage;

        base.Start();
    }

    void Update()
    {
    }

    public override void EnemyDie()
    {
        base.EnemyDie();

        anim.SetBool("goblinDeath", true);

    }
}
