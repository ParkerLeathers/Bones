using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CelestePlayerStateWall : CelestePlayerState {


    [Header("Components")]
    protected Rigidbody2D rb;

    //constants
    readonly int JUMP_X_FORCE = 15;
    readonly int JUMP_Y_FORCE = 7;

    //climb state property
    protected bool canClimb = true;
    protected bool helpless = false;

    protected GameObject wall;

    public CelestePlayerStateWall(CelestePlayerStateMachine stateMachine) : base(stateMachine) { }

    public abstract void InitProperties();

    public override void BeginState() {
        InitProperties();

        rb = stateMachine.player.rb;
        rb.gravityScale = 0;

        rb.velocity = Vector2.zero;

        wall = stateMachine.player.GetWall();
        if (wall.transform.position.x < rb.position.x) {
            stateMachine.player.left = true;
            stateMachine.player.sr.flipX = true;
        }
        else {
            stateMachine.player.left = false;
            stateMachine.player.sr.flipX = false;
        }

        BeginStateWall();
    }

    public override void UpdateState() {
        wall = stateMachine.player.GetWall();

        if (ExitChecks()) return;

        UpdateStateWall();
    }

    private bool ExitChecks() {
        if (Drop()) return true;
        if (Kick()) return true;

        if (canClimb)
            if (Climb()) return true;
        return false;
    }

    private bool Drop() {
        if (wall == null) {
            stateMachine.ChangeState(helpless ? stateMachine.stateHelpless : stateMachine.stateJump);
            return true;
        }
        return false;
    }

    private bool Kick() {
        if (stateMachine.player.left ? InputManager.GetKey(InputManager.InputName.Right) : InputManager.GetKey(InputManager.InputName.Left)) {
            rb.velocity = new Vector2(0, 0f);
            rb.AddForce(new Vector2(stateMachine.player.left ? JUMP_X_FORCE : -JUMP_X_FORCE, helpless ? 0f : JUMP_Y_FORCE), ForceMode2D.Impulse);
            stateMachine.ChangeState(helpless ? stateMachine.stateHelpless : stateMachine.stateJump);
            return true;
        }
        return false;
    }

    private bool Climb() {
        if (Input.GetAxisRaw("Vertical") != 0) {
            stateMachine.ChangeState(stateMachine.stateClimb);
            return true;
        }
        return false;
    }

    public abstract void BeginStateWall();
    public abstract void UpdateStateWall();
}
