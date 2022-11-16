using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class AiStateBase
{
    protected readonly AiMovement _aiMovement;
    
    public AiStateBase(AiMovement aiMovement)
    {
        _aiMovement = aiMovement;
    }
    
    public virtual void OnEnter() {}
    public virtual void OnExit() {}

    public virtual void OnUpdate() {}
}
