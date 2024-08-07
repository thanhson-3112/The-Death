using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Archer : EnemyLifeBase
{
    [SerializeField] protected float archerMaxHealth = 200f;
    [SerializeField] protected float archerHealth;
    public float archerDamage = 5f;

    public override void Start()
    {
        archerHealth = archerMaxHealth;
        enemyMaxHealth = archerMaxHealth;
        enemyHealth = archerHealth;
        enemyDamage = archerDamage;

        base.Start();
    }

    void Update()
    {
    }

    public override void EnemyDie()
    {
        base.EnemyDie();

        anim.SetBool("ArcherDeath", true);
      
    }

}
