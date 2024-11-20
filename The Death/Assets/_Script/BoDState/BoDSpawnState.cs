using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoDSpawnState : BaseState
{
    private BoDStateMachine SM;
    private Animator anim;
    private bool spawning = true;

    public BoDSpawnState(BoDStateMachine stateMachine, Animator animator)
    {
        SM = stateMachine;
        anim = animator;
    }

    public override void Enter()
    {
        base.Enter();
        spawning = true;
        anim.SetTrigger("BoDCastSpell");
        SoundFxManager.instance.PlaySoundFXClip(SM.castSpellSound, SM.transform, 0.5f);
        SM.StartCoroutine(Spawner());
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
            SoundFxManager.instance.StopAudio(SM.castSpellSound);
            SM.NextState();
            spawning = false; 
        }
    }

    private IEnumerator Spawner()
    {
        WaitForSeconds wait = new WaitForSeconds(SM.bossSpawnRate);

        while (spawning)
        {
            anim.SetTrigger("BoDCastSpell");
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
