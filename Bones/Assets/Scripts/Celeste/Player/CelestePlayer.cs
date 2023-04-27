using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CelestePlayer : MonoBehaviour {
    [Header("Movement")]
    public float speed = 5f;

    [Header("Components")]
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public Collider2D c2;
    [HideInInspector] public AnimationStateChanger asc;
    [HideInInspector] public SpriteMaterialSwapper sms;
    [HideInInspector] public SpriteRenderer sr;

    [HideInInspector]
    public float gravityScale;
    [HideInInspector]
    public bool left;
    
    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        c2 = GetComponent<Collider2D>();
        asc = GetComponent<AnimationStateChanger>();
        sms = GetComponent<SpriteMaterialSwapper>();
        sr = GetComponent<SpriteRenderer>();
        gravityScale = rb.gravityScale;
        left = false;
    }


    void Update() {

        /*
         * Legacy code
        
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
        */

        if (Input.GetKeyDown(KeyCode.J))
            SceneManager.LoadScene("Undertale");
        


    }

    public GameObject GetGround() {
        GameObject[] hits = System.Array.ConvertAll(Physics2D.RaycastAll(transform.position, Vector2.down, c2.bounds.extents.y + 0.05f), item => item.collider.gameObject);
        foreach(GameObject i in hits)
            if (i.CompareTag("Wall"))
                return i;

        //a vector from the bottom left of the collider to the (bottom right - 0.05 y). demon code
        Vector2 sweep = new Vector2(c2.bounds.center.x + c2.bounds.extents.x, c2.bounds.center.y - c2.bounds.extents.y - 0.05f) - (Vector2) c2.bounds.min;
        hits = System.Array.ConvertAll(Physics2D.RaycastAll(c2.bounds.min, sweep, sweep.magnitude), item => item.collider.gameObject);
        foreach (GameObject i in hits)
            if (i.CompareTag("Wall"))
                return i;
        return null;
    }

    public GameObject GetWall() {
        GameObject[] hits = System.Array.ConvertAll(Physics2D.RaycastAll(transform.position, Vector2.right, c2.bounds.extents.x + 0.05f), item => item.collider.gameObject);
        foreach (GameObject i in hits)
            if (i.CompareTag("Wall"))
                return i;

        //a vector from the top right of the collider to the (bottom right + 0.05 x). also demon code
        Vector2 sweep = new Vector2(c2.bounds.center.x + c2.bounds.extents.x + 0.05f, c2.bounds.center.y - c2.bounds.extents.y) - (Vector2) c2.bounds.max;
        hits = System.Array.ConvertAll(Physics2D.RaycastAll(c2.bounds.max, sweep, sweep.magnitude), item => item.collider.gameObject);
        foreach (GameObject i in hits)
            if (i.CompareTag("Wall"))
                return i;

        hits = System.Array.ConvertAll(Physics2D.RaycastAll(transform.position, Vector2.left, c2.bounds.extents.x - 0.05f), item => item.collider.gameObject);
        foreach (GameObject i in hits)
            if (i.CompareTag("Wall"))
                return i;

        //a vector from the top left of the collider to the (bottom left - 0.05 x). also demon code
        sweep = new Vector2(c2.bounds.center.x - c2.bounds.extents.x - 0.05f, c2.bounds.center.y - c2.bounds.extents.y) - new Vector2(c2.bounds.center.x - c2.bounds.extents.x, c2.bounds.max.y);
        hits = System.Array.ConvertAll(Physics2D.RaycastAll(new Vector2(c2.bounds.center.x - c2.bounds.extents.x, c2.bounds.max.y), sweep, sweep.magnitude), item => item.collider.gameObject);
        foreach (GameObject i in hits)
            if (i.CompareTag("Wall"))
                return i;

        return null;
    }
}
