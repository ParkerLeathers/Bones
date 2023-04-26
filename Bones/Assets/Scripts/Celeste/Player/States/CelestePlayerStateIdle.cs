using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelestePlayerStateIdle : CelestePlayerStateGrounded {
    
    public CelestePlayerStateIdle(CelestePlayerStateMachine stateMachine) : base(stateMachine) {
        
    }


    public override void InitProperties() {
        canJump = true;
    }

    public override void BeginStateGround() {
        
    }


    public override void UpdateStateGround() {

    }
}
