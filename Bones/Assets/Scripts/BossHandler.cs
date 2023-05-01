using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHandler : MonoBehaviour {
    [Header("Stats")]
    [SerializeField] private float inverseSpeed;
    [SerializeField] private float speed;
    [SerializeField] private bool setSpeed = false;

    private Vector2 target;
    private float distance;
    private Rigidbody2D rb;

    [HideInInspector] public bool done = false;
    [HideInInspector] public bool paused = false;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        target = transform.position;
    }

    void Start() {
        
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, target) < 0.001)
            done = true;
        else
            done = false;
        if (!paused)
            if (setSpeed)
                transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
            else
                transform.position = Vector2.MoveTowards(transform.position, target, distance * Time.deltaTime / inverseSpeed);
    }

    public void Pause() {
        paused = true;
    }

    public void UnPause() {
        paused = false;
    }

    public void MoveTo(Vector2 pos) {
        target = pos;
        distance = Vector2.Distance(transform.position, target);
    }
}
