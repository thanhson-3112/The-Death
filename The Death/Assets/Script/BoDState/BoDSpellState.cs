using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BoDSpellState : BaseState
{
    private BoDStateMachine SM;
    public Transform target;

    public BoDSpellState(BoDStateMachine stateMachine) : base("Spell", stateMachine)
    {
        SM = stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        SM.StartCoroutine(EndState());

    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        SM.GetTarget();
    }

    public override void UpdatePhysics()
    {
        /*anim.SetTrigger("enemyRun");*/
        Vector3 targetDirection = SM.target.position - SM.transform.position;
        if (targetDirection.x > 0)
        {
            SM.transform.localScale = new Vector3(-Mathf.Abs(SM.transform.localScale.x), SM.transform.localScale.y, SM.transform.localScale.z);
        }
        else
        {
            SM.transform.localScale = new Vector3(Mathf.Abs(SM.transform.localScale.x), SM.transform.localScale.y, SM.transform.localScale.z);
        }
        SM.transform.position = Vector2.MoveTowards(SM.transform.position, SM.target.position, SM.moveSpeed * Time.deltaTime);


    }

    public override void Exit()
    {
        base.Exit();
        SM.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }

    IEnumerator EndState()
    {
        yield return new WaitForSeconds(5f);
        SM.NextState();
    }
}