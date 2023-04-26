using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelestePlayerStateCling : CelestePlayerStateWall {
    public CelestePlayerStateCling(CelestePlayerStateMachine stateMachine) : base(stateMachine) {}

    public override void InitProperties() {
        canClimb = true;
        helpless = false;
    }

    public override void BeginStateWall() {
    }

    public override void UpdateStateWall() {

        if (Input.GetAxisRaw("Vertical") != 0)
            stateMachine.ChangeState(stateMachine.stateClimb);
    }
}
