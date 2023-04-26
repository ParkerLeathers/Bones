using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelestePlayerStateClimb : CelestePlayerStateWall {

    private float speed = 5f;
    
    public CelestePlayerStateClimb(CelestePlayerStateMachine stateMachine) : base(stateMachine) {}

    public override void InitProperties() {
        canClimb = false;
        helpless = false;
    }

    public override void BeginStateWall() {
    }

    public override void UpdateStateWall() {
        rb.velocity = new Vector2(0, Input.GetAxisRaw("Vertical") * speed);

        if (Input.GetAxisRaw("Vertical") == 0)
            stateMachine.ChangeState(stateMachine.stateCling);
    }
}
