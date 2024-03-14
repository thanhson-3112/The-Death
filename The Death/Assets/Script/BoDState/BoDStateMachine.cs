using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoDStateMachine : StateMachine
{
    public BoDIdle idleState;
    public BoDMovingState movingState;

    List<BaseState> randomStates;
    public Transform target;
    


    public float BoDSpeed = 5f;
    public float RushSpeed = 15f;

    private Animator anim;
    BaseState LastState;
    BaseState LastTwoState;

    private void Awake()
    {
        idleState = new BoDIdle(this);
        movingState = new BoDMovingState(this);

        randomStates = new List<BaseState>() { idleState, movingState};
        /*target = GameObject.FindGameObjectWithTag("Player").transform;*/
    }

    new void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
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
            ran = Random.Range(0, randomStates.Count - 1);
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
