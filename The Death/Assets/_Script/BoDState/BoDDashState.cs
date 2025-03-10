using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BoDDashState : BaseState
{
    private BoDStateMachine SM;
    public int DashNumber = 3;
    private Animator anim;

    public BoDDashState(BoDStateMachine stateMachine, Animator animator, int dashNumber = 3)
    {
        DashNumber = dashNumber;
        SM = stateMachine;
        anim = animator;
    }

    public override void Enter()
    {
        base.Enter();
        anim.SetTrigger("BoDAttack");
        SM.StartCoroutine(Dash());

    }

    IEnumerator Dash()
    {
        for (int i = 0; i < DashNumber; i++)
        {
            Vector3 targetDirection = (SM.target.position - SM.transform.position).normalized;
            if (targetDirection.x > 0)
            {
                SM.transform.localScale = new Vector3(-Mathf.Abs(SM.transform.localScale.x), SM.transform.localScale.y, SM.transform.localScale.z);
            }
            else
            {
                SM.transform.localScale = new Vector3(Mathf.Abs(SM.transform.localScale.x), SM.transform.localScale.y, SM.transform.localScale.z);
            }
            SM.GetTarget();
            SM.gameObject.GetComponent<Rigidbody2D>().velocity = targetDirection * SM.dashSpeed;

            anim.SetTrigger("BoDAttack");
            SoundFxManager.instance.PlaySoundFXClip(SM.dashAttackSound, SM.transform, 0.5f);
            yield return new WaitForSeconds(2f);
        }

        SM.NextState();
    }

    public override void UpdateLogic()
        {
            base.UpdateLogic();
            SM.GetTarget();
        }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("BoDTakeHit"))
        {
            anim.SetTrigger("BoDAttack");
        }
    }

    public override void Exit()
    {
        base.Exit();
        SM.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }
}
