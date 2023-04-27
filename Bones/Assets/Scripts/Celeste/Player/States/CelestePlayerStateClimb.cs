using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelestePlayerStateClimb : CelestePlayerStateWall {

    [Header("Constants")]
    private readonly float CLIMB_SPEED = 5f;
    private readonly float MAX_ANIMATION_SPEED = 5f;
    private readonly float ANIMATION_MODIFIER = 0.5f;

    public CelestePlayerStateClimb(CelestePlayerStateMachine stateMachine) : base(stateMachine) {}

    public override void InitProperties() {
        canClimb = false;
        helpless = false;
    }

    public override void BeginStateWall() {
    }

    public override void UpdateStateWall() {
        rb.velocity = new Vector2(0, Input.GetAxisRaw("Vertical") * CLIMB_SPEED);

        stateMachine.player.asc.ChangeAnimationState("Climbing", System.Math.Abs(rb.velocity.y) * ANIMATION_MODIFIER >= MAX_ANIMATION_SPEED ? MAX_ANIMATION_SPEED : System.Math.Abs(rb.velocity.y) * ANIMATION_MODIFIER);

//        if (Input.GetAxisRaw("Vertical") == 0)
//            stateMachine.ChangeState(stateMachine.stateCling);
    }
}
