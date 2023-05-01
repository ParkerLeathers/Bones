using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDSPlayer : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float SPEED = 3;

    [Header("Components")]
    private Rigidbody2D rb;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        rb.MovePosition(transform.position + new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * Time.fixedDeltaTime * SPEED);
    }
}
