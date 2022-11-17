using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class AiStateBase
{
    protected readonly AiStateMachine AiStateMachine;
    
    public AiStateBase(AiStateMachine aiStateMachine)
    {
        AiStateMachine = aiStateMachine;
    }
    
    public virtual void OnEnter() {}
    public virtual void OnExit() {}

    public virtual void OnUpdate() {}
}
