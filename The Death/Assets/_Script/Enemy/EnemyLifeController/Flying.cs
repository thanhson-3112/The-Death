using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flying : EnemyLifeBase
{
    [SerializeField] protected float flyingMaxHealth = 200f;
    [SerializeField] protected float flyingHealth;
    public float flyingDamage = 5f;

 

    public override void Start()
    {
        flyingHealth = flyingMaxHealth;
        enemyMaxHealth = flyingMaxHealth;
        enemyHealth = flyingHealth;
        enemyDamage = flyingDamage;

        base.Start();
    }

    void Update()
    {
    }
    
    public override void EnemyDie()
    {
        base.EnemyDie();

        anim.SetBool("FlyingDeath", true);

        Destroy(gameObject, 0.75f);
    }

}
