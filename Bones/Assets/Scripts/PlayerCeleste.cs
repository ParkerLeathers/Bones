using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCeleste : MonoBehaviour {
    [Header("Movement")]
    public float speed = 5f;

    [Header("Components")]
    private Rigidbody2D rb;


    private bool canJump;
    private bool canDash;
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        canJump = true;
        canDash = true;
    }


    void Update() {

        /*
         * Inputs
        */
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && canJump == true) { //change this to get axis raw vertical
            Debug.Log("a");
            rb.AddForce(new Vector2(0, speed), ForceMode2D.Impulse); //change to own speed var, disable double jumps
            canJump = false;
        } else if (transform.position.y < -1.456) { // change this condition to when ground is hit
            canJump = true;
        }

        if (Input.GetKeyDown(KeyCode.Space) && canDash == true) {
            rb.AddForce(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * speed, ForceMode2D.Impulse); //change to own speed var, disable double jumps
            canDash = false;
        } else if (transform.position.y < -1.456) { // change this condition to when ground is hit
            canDash = true;
        }

        if (Input.GetKeyDown(KeyCode.J))
            SceneManager.LoadScene("Undertale");

        rb.position += new Vector2(Input.GetAxisRaw("Horizontal"), 0) * Time.deltaTime * speed;
    }
}
