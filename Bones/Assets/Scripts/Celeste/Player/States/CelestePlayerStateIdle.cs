using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelestePlayerStateIdle : CelestePlayerStateGrounded {
    
    public CelestePlayerStateIdle(CelestePlayerStateMachine stateMachine) : base(stateMachine) {
        
    }


    public override void InitProperties() {
        canJump = true;
        stateMachine.player.asc.ChangeAnimationState("Idle");
    }

    public override void BeginStateGround() {
        
    }


    public override void UpdateStateGround() {
        if (rb.velocity.x != 0)
            stateMachine.ChangeState(stateMachine.stateWalking);
    }
}
