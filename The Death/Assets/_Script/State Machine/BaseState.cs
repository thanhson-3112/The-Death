using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseState 
{
    public StateMachine stateMachine;

    protected float stateDuration = 3f;

    public virtual void Enter() { }
    public virtual void UpdateLogic() { }
    public virtual void UpdatePhysics() { }
    public virtual void Exit() { }

}
