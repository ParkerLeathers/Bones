using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CelestePlayerStateAerial : CelestePlayerState
{


    [Header("Components")]
    private Collider2D collider;
    private Rigidbody2D rb;

    int speed = 5;


    //grounded state property
    protected bool canDash = true;


    public CelestePlayerStateAerial(CelestePlayerStateMachine stateMachine) : base(stateMachine) {}

    public abstract void InitProperties();

    public override void BeginState() {
        InitProperties();
        
        collider = stateMachine.player.GetComponent<Collider2D>();
        rb = stateMachine.player.GetComponent<Rigidbody2D>();
        rb.gravityScale = stateMachine.player.gravityScale;

        BeginStateAerial();
    }

    public override void UpdateState() {

        if (ExitChecks()) return;

        Debug.Log(rb.velocity);
        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, rb.velocity.y);
        Debug.Log(rb.velocity);

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
