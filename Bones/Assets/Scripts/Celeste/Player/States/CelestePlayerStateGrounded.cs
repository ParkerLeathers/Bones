using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CelestePlayerStateGrounded : CelestePlayerState
{
    

    [Header("Components")]
    private Rigidbody2D rb;

    int speed = 5;

    //grounded state property
    protected bool canJump = true;

    protected GameObject ground;

    public CelestePlayerStateGrounded(CelestePlayerStateMachine stateMachine) : base(stateMachine) {}

    public abstract void InitProperties();

    public override void BeginState() {
        InitProperties();

        rb = stateMachine.player.GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;

        BeginStateGround();
    }

    public override void UpdateState() {
        ground = stateMachine.player.GetGround();

        if (ExitChecks()) return;
        
        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, rb.velocity.y);
        UpdateStateGround();
    }

    private bool ExitChecks() {
        if(Fall()) return true;

        if (canJump)
            if (Jump()) return true;
        return false;
    }

    private bool Fall() {
        if (ground == null) {
            stateMachine.ChangeState(stateMachine.stateJump);
            return true;
        }
        return false;
    }

    private bool Jump() {
        if (Input.GetKey(KeyCode.W)) {
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce(new Vector2(0, speed), ForceMode2D.Impulse);
            stateMachine.ChangeState(stateMachine.stateJump);
            return true;
        }
        return false;
    }

    public abstract void BeginStateGround();
    public abstract void UpdateStateGround();
}
