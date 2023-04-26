using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CelestePlayerState
{

    protected CelestePlayerStateMachine stateMachine;

    protected float timer = 0;
    
    public CelestePlayerState(CelestePlayerStateMachine stateMachine) {
        this.stateMachine = stateMachine;
    }

    public void UpdateStateBase() {
        timer += Time.fixedDeltaTime;
        UpdateState();
    }

    public void BeginStateBase() {
        timer = 0;
        BeginState();
    }

    public abstract void UpdateState();

    public abstract void BeginState();
}
