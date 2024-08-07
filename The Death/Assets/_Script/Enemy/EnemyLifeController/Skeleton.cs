using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Skeleton : EnemyLifeBase
{
    [SerializeField] protected float skeletonMaxHealth = 100f;
    [SerializeField] protected float skeletonHealth;
    public float skeletonDamage = 1f;


    public override void Start()
    {
        skeletonHealth = skeletonMaxHealth;
        enemyMaxHealth = skeletonMaxHealth;
        enemyHealth = skeletonHealth;
        enemyDamage = skeletonDamage;

        base.Start();
    }

    void Update()
    {
    }

    public override void EnemyDie()
    {
        base.EnemyDie();

        anim.SetBool("SkeletonDeath", true);

    }

}
