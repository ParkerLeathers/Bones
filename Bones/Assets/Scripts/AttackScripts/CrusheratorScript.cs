using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrusheratorScript : MonoBehaviour
{

    [Header("Stats")]
    [SerializeField]
    private float speed;
    [Header("Objects")]
    [SerializeField]
    private GameObject bottom;
    [SerializeField]
    private GameObject top;
    [SerializeField]
    private GameObject left;
    [SerializeField]
    private GameObject right;
    [SerializeField]
    private GameObject fightBox;

    private enum State {
        Crushing,
        Uncrushing,
        MonoCrushing,
        Idle
    }

    private State state;
    void Start()
    {
        state = State.Idle;
    }
    
    void Update()
    {
        switch (state) {
            case State.Crushing:
                CrushingUpdate();
                break;
            case State.Uncrushing:
                UncrushingUpdate();
                break;
            case State.MonoCrushing:
                MonoCrushingUpdate();
                break;
            case State.Idle:
                IdleUpdate();
                break;
            default:
                IdleUpdate();
                break;
        }
    }

    public void Uncrush() {
        state = State.Uncrushing;
    }
    
    public void Idlize() {
        state = State.Idle;
    }

    public void Crush() {
        state = State.Crushing;
    }

    public void MonoCrush() {
        state = State.MonoCrushing;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other == null || other.gameObject == null)
            return;

        if (other.tag == "FightBox") {
            UncrushingUpdate();
            if (state == State.MonoCrushing)
                Uncrush();
            else
                Idlize();
        }
    }

    private void CrushingUpdate() {
        MonoCrushingUpdate();
    }

    private void UncrushingUpdate() {
        if (bottom.transform.localScale.y > 1)
            bottom.transform.localScale -= new Vector3(0.0f, speed * Time.deltaTime, 0.0f);
        else
            bottom.transform.localScale = new Vector3(bottom.transform.localScale.x, 1, bottom.transform.localScale.z);
        if (top.transform.localScale.y > 1)
            top.transform.localScale -= new Vector3(0.0f, speed * Time.deltaTime, 0.0f);
        else
            top.transform.localScale = new Vector3(top.transform.localScale.x, 1, top.transform.localScale.z);
        if (left.transform.localScale.x > 1)
            left.transform.localScale -= new Vector3(speed * Time.deltaTime, 0.0f, 0.0f);
        else
            left.transform.localScale = new Vector3(1, left.transform.localScale.y, left.transform.localScale.z);
        if (right.transform.localScale.x > 1)
            right.transform.localScale -= new Vector3(speed * Time.deltaTime, 0.0f, 0.0f);
        else
            right.transform.localScale = new Vector3(1, right.transform.localScale.y, right.transform.localScale.z);
    }

    private void MonoCrushingUpdate() {
        bottom.transform.localScale += new Vector3(0.0f, speed * Time.deltaTime, 0.0f);
        top.transform.localScale += new Vector3(0.0f, speed * Time.deltaTime, 0.0f);
        left.transform.localScale += new Vector3(speed * Time.deltaTime, 0.0f, 0.0f);
        right.transform.localScale += new Vector3(speed * Time.deltaTime, 0.0f, 0.0f);
        //currently can create weird behavior with it stopping early ( since first hit stops it ), and likely overlaps
        //idea for future: make movement relative to distance
    }

    private void IdleUpdate() { }
}
