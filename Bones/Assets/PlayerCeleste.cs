using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCeleste : MonoBehaviour {

    [Header("Components")]
    private Rigidbody2D rb;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() {
        rb.MovePosition(transform.position + new Vector3(Input.GetAxisRaw("Horizontal")) * Time.fixedDeltaTime * speed);
    }
}
