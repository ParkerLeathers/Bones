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
    Rigidbody2D rb;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start() {
        
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, distance * Time.deltaTime / inverseSpeed);
    }

    public void MoveTo(Vector2 pos) {
        target = pos;
        distance = Vector2.Distance(transform.position, target);
    }
}
