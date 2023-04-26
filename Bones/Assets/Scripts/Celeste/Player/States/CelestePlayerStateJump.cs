using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelestePlayerStateJump : CelestePlayerStateAerial {




    public CelestePlayerStateJump(CelestePlayerStateMachine stateMachine) : base(stateMachine) { }
    
    public override void InitProperties() {
        canDash = true;
    }

    public override void BeginStateAerial() {

    }

    public override void UpdateStateAerial() {

    }
}
