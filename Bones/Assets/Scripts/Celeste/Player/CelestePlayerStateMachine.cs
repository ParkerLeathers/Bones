using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelestePlayerStateMachine : MonoBehaviour
{
    public CelestePlayer player;

    private bool started = false;

    /*
     * State Machine
    */

    public CelestePlayerState playerState;

    public CelestePlayerStateIdle stateIdle { get; private set; }
    public CelestePlayerStateWalking stateWalking { get; private set; }
    public CelestePlayerStateJump stateJump { get; private set; }
    public CelestePlayerStateHelpless stateHelpless { get; private set; }
    public CelestePlayerStateDash stateDash { get; private set; }
    public CelestePlayerStateCling stateCling { get; private set; }
    public CelestePlayerStateClimb stateClimb { get; private set; }
    public CelestePlayerStateSlip stateSlip { get; private set; }


    public void ChangeState(CelestePlayerState playerState) {
        this.playerState = playerState;
        this.playerState.BeginStateBase();
    }

    public void Begin() {
        started = true;

        ChangeState(stateIdle);
    }

    void Start()
    {
        stateIdle = new CelestePlayerStateIdle(this);
        stateWalking = new CelestePlayerStateWalking(this);
        stateJump = new CelestePlayerStateJump(this);
        stateHelpless = new CelestePlayerStateHelpless(this);
        stateDash = new CelestePlayerStateDash(this);
        stateCling = new CelestePlayerStateCling(this);
        stateClimb = new CelestePlayerStateClimb(this);
        stateSlip = new CelestePlayerStateSlip(this);
    }

    void FixedUpdate()
    {
        if (!started)
            return;

        playerState.UpdateStateBase();
    }
}
