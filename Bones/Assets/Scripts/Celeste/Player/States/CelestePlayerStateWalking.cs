using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelestePlayerStateWalking : CelestePlayerStateGrounded {

    [Header("Constants")]
    private readonly float MAX_ANIMATION_SPEED = 5f;
    private readonly float ANIMATION_MODIFIER = 0.3f;

    public CelestePlayerStateWalking(CelestePlayerStateMachine stateMachine) : base(stateMachine) {}


    public override void InitProperties() {
        canJump = true;
    }

    public override void BeginStateGround() {
        
    }


    public override void UpdateStateGround() {

        stateMachine.player.asc.ChangeAnimationState("Walking", System.Math.Abs(rb.velocity.x) * ANIMATION_MODIFIER >= MAX_ANIMATION_SPEED ? MAX_ANIMATION_SPEED : System.Math.Abs(rb.velocity.x) * ANIMATION_MODIFIER);


        if (rb.velocity.x == 0)
            stateMachine.ChangeState(stateMachine.stateIdle);
    }
}
