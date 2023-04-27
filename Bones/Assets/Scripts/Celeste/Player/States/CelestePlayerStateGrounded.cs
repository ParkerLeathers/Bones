using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CelestePlayerStateGrounded : CelestePlayerState
{
    

    [Header("Components")]
    protected Rigidbody2D rb;

    [Header("Constants")]
    private readonly int JUMP_FORCE = 5;
    private readonly int SPEED = 5;

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

        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * SPEED, rb.velocity.y);

        if (rb.velocity.x < 0)
            stateMachine.player.sr.flipX = true;
        else if (rb.velocity.x > 0)
            stateMachine.player.sr.flipX = false;

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
            rb.AddForce(new Vector2(0, JUMP_FORCE), ForceMode2D.Impulse);
            stateMachine.ChangeState(stateMachine.stateJump);
            return true;
        }
        return false;
    }

    public abstract void BeginStateGround();
    public abstract void UpdateStateGround();
}
