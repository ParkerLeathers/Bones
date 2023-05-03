using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [Header("Stats")]
    private static readonly float SPEED = 3;
    private static readonly float MAX_DIST = 0.7f;
    private static readonly float RADIUS = 1.5f;

    [Header("GameObjects")]
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject boss;
    private Transform door1;
    private Transform door2;
    
    private enum State {
        Opening,
        Closing,
        Idle
    }

    private State state;

    private Vector3 pos1;
    private Vector3 pos2;
    private float dist;
    void Start()
    {
        Transform[] children = GetComponentsInChildren<Transform>();
        door1 = children[1];
        door2 = children[2];
        pos1 = door1.position;
        pos2 = door2.position;
        state = State.Idle;
    }

    private void OpenUpdate() {
        dist += SPEED * Time.deltaTime;

        if(dist > MAX_DIST) {
            dist = MAX_DIST;
            return;
        }

        Vector3 vect = (Vector2) (pos2 - pos1).normalized; //the vector3 -> vector2 -> vector3 truncates the z value
        door1.position = pos1 - (vect * dist);
        door2.position = pos2 + (vect * dist);
    }

    private void CloseUpdate() {
        dist -= SPEED * Time.deltaTime;

        if (dist < 0) {
            dist = 0;
            state = State.Idle;
            return;
        }

        Debug.Log("a " + pos1);
        Debug.Log("b " + pos2);
        Vector3 vect = (Vector2)(pos2 - pos1).normalized; //the vector3 -> vector2 -> vector3 truncates the z value
        door1.position = pos1 - (vect * dist);
        door2.position = pos2 + (vect * dist);
    }

    
    void Update()
    {
        if (Vector2.Distance((Vector2)transform.position, player.transform.position) < RADIUS || Vector2.Distance((Vector2)transform.position, (Vector2)boss.transform.position) < RADIUS)
            state = State.Opening;
        else
            state = State.Closing;
        switch (state) {
            case State.Closing:
                CloseUpdate();
                break;
            case State.Opening:
                OpenUpdate();
                break;
        }
    }
}
