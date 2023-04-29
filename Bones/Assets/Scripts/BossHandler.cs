using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHandler : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField]
    private float inverseSpeed;

    private Vector2 target;
    private float distance;
    private Rigidbody2D rb;

    public bool done = false;
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
        transform.position = Vector2.MoveTowards(transform.position, target, distance * Time.deltaTime / inverseSpeed);
    }

    public void MoveTo(Vector2 pos) {
        target = pos;
        distance = Vector2.Distance(transform.position, target);
    }
}
