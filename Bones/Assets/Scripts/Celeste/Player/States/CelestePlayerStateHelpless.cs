using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelestePlayerStateHelpless : CelestePlayerStateAerial
{
    public CelestePlayerStateHelpless(CelestePlayerStateMachine stateMachine) : base(stateMachine) {}

    public override void InitProperties() {
        canDash = false;
    }

    public override void BeginStateAerial() {
    }


    public override void UpdateStateAerial() {
    }
}
