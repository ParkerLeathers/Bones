using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CelestePlayerStateWall : CelestePlayerState {


    [Header("Components")]
    protected Rigidbody2D rb;

    int xSpeed = 5;
    int ySpeed = 5;
    int speed = 5;

    //climb state property
    protected bool canClimb = true;
    protected bool helpless = false;

    protected GameObject wall;

    protected bool left;

    public CelestePlayerStateWall(CelestePlayerStateMachine stateMachine) : base(stateMachine) { }

    public abstract void InitProperties();

    public override void BeginState() {
        InitProperties();

        rb = stateMachine.player.GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;

        rb.velocity = Vector2.zero;

        wall = stateMachine.player.GetWall();
        left = false;
        if (wall.transform.position.x < rb.position.x)
            left = true;

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
        if (left ? Input.GetKey(KeyCode.D) : Input.GetKey(KeyCode.A)) {
            rb.velocity = new Vector2(0, 0f);
            rb.AddForce(new Vector2(left ? xSpeed : -xSpeed, ySpeed), ForceMode2D.Impulse);
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
