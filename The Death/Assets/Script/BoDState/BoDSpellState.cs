using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoDSpellState : BaseState
{
    private BoDStateMachine SM;
    private Animator anim;
    private bool spawning = true;

    public BoDSpellState(BoDStateMachine stateMachine, Animator animator) : base("Spell", stateMachine)
    {
        SM = stateMachine;
        anim = animator;
    }

    public override void Enter()
    {
        base.Enter();
        spawning = true;
        SM.StartCoroutine(Spawner());
        anim.SetTrigger("BoDCastSpell");
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("BoDTakeHit"))
        {
            anim.SetTrigger("BoDRun");
            SM.NextState();
            spawning = false; 
        }
    }

    private IEnumerator Spawner()
    {
        WaitForSeconds wait = new WaitForSeconds(SM.bossSpawnRate);

        while (spawning)
        {
            yield return wait;
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        int rand = Random.Range(0, SM.enemyPrefabs.Length);
        GameObject enemyToSpawn = SM.enemyPrefabs[rand];

        // V? trí ng?u nhiên quanh boss
        Vector3 spawnPosition = SM.transform.position + Random.insideUnitSphere * SM.spawnRadius;
        spawnPosition.z = 0f;

        GameObject spawnedEnemy = GameObject.Instantiate(enemyToSpawn, spawnPosition, Quaternion.identity);
    }

    public override void Exit()
    {
        base.Exit();
        SM.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        spawning = false;
    }
}
