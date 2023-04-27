using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CelestePlayerStateAerial : CelestePlayerState
{


    [Header("Components")]
    private Rigidbody2D rb;


    [Header("Constants")]
    private readonly float X_SPEED = 5f;
    private readonly float MAX_ANIMATION_SPEED = 5f;
    private readonly float ANIMATION_MODIFIER = 1f;


    //grounded state property
    protected bool canDash = true;


    public CelestePlayerStateAerial(CelestePlayerStateMachine stateMachine) : base(stateMachine) {}

    public abstract void InitProperties();

    public override void BeginState() {
        InitProperties();

        rb = stateMachine.player.rb;
        rb.gravityScale = stateMachine.player.gravityScale;

        BeginStateAerial();
    }

    public override void UpdateState() {

        if (ExitChecks()) return;

        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * X_SPEED, rb.velocity.y);

        stateMachine.player.asc.ChangeAnimationState("Aerial", System.Math.Abs(rb.velocity.y) * ANIMATION_MODIFIER >= MAX_ANIMATION_SPEED ? MAX_ANIMATION_SPEED : System.Math.Abs(rb.velocity.y) * ANIMATION_MODIFIER);

        if (rb.velocity.x < 0)
            stateMachine.player.sr.flipX = true;
        else if (rb.velocity.x > 0)
            stateMachine.player.sr.flipX = false;

        UpdateStateAerial();
    }
    private bool ExitChecks() {
        if (Cling()) return true;
        if (Land()) return true;

        if (canDash)
            if (Dash()) return true;
        return false;
    }

    private bool Cling() {
        GameObject wall = stateMachine.player.GetWall();
        if (wall != null && (System.Math.Sign(rb.velocity.x) == System.Math.Sign(wall.transform.position.x - rb.position.x) || System.Math.Sign(Input.GetAxisRaw("Horizontal")) == System.Math.Sign(wall.transform.position.x - rb.position.x))) {
            stateMachine.ChangeState(canDash ? stateMachine.stateCling : stateMachine.stateSlip);
            return true;
        }
        return false;
    }

    private bool Land() {
        if (rb.velocity.y <= 0 && stateMachine.player.GetGround() != null) {
            stateMachine.ChangeState(stateMachine.stateIdle);
            return true;
        }
        return false;
    }

    private bool Dash() {
        //do impulse jump
        if (Input.GetKey(KeyCode.Space)) {
            stateMachine.ChangeState(stateMachine.stateDash);
            return true;
        }
        return false;
    }


    public abstract void BeginStateAerial();
    public abstract void UpdateStateAerial();
}
