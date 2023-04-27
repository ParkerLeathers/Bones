using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelestePlayerStateDash : CelestePlayerState {


    private Rigidbody2D rb;

    private readonly float seconds = 0.5f;

    private readonly float speed = 10f;

    private Vector2 direction;

    public CelestePlayerStateDash(CelestePlayerStateMachine stateMachine) : base(stateMachine) {}

    public override void BeginState() {
        rb = stateMachine.player.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0;
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        stateMachine.player.sms.Swap("Dash");
    }

    public override void UpdateState() {
        if (timer > seconds) {
            rb.velocity = Vector2.zero;
            stateMachine.ChangeState(stateMachine.stateHelpless);
            return;
        }
        rb.velocity = direction * speed;
    }
}
