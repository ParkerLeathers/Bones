using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelestePlayerStateSlip : CelestePlayerStateWall {
    public CelestePlayerStateSlip(CelestePlayerStateMachine stateMachine) : base(stateMachine) {}

    private float speed = 3f;

    public override void InitProperties() {
        canClimb = false;
        helpless = true;
    }

    public override void BeginStateWall() {
    }


    public override void UpdateStateWall() {
        rb.velocity = new Vector2(0, -speed);
    }
}
