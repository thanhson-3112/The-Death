using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoDStateMachine : StateMachine
{
    public BoDDashState dashState;
    public BoDMovingState movingState;
    public BoDSpellState spellState;
    public BoDAttackState attackState;

    List<BaseState> randomStates;
    public Transform target;
    
    public float moveSpeed = 5f;
    public float dashSpeed = 30f;

    private Animator anim;
    BaseState LastState;
    BaseState LastTwoState;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        movingState = new BoDMovingState(this, anim);
        dashState = new BoDDashState(this, anim);
        spellState = new BoDSpellState(this);
        attackState = new BoDAttackState(this, anim);

        randomStates = new List<BaseState>() { movingState, dashState, spellState};
    }

    new void Start()
    {
        base.Start();
    }

    new public void Update()
    {
        base.Update();
        Death();
    }

    public void NextState()
    {
        ChangeState(RandomState());
    }

    BaseState RandomState()
    {
        int ran = Random.Range(0, randomStates.Count);
        while (randomStates[ran] == LastState || randomStates[ran] == LastTwoState)
        {
            ran = Random.Range(0, randomStates.Count);
        }
        LastTwoState = LastState;
        LastState = randomStates[ran];
        return randomStates[ran];
    }

    protected override BaseState GetInitialState()
    {
        LastState = movingState;
        LastTwoState = movingState; 
        return movingState;
    }

    public void GetTarget()
    {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    public void Death()
    {
       
    }
}
