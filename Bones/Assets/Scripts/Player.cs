using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 3f;

    [Header("Components")]
    private Rigidbody2D rb;

    [Header("Game Objects")]
    [SerializeField]
    private GameObject crusherator;

    [Header("Prefabs")]
    [SerializeField]
    private GameObject bombPrefab;
    [SerializeField]
    private GameObject dedPrefab;
    
    [Header("Misc")]
    public int points;

    private CrusheratorScript crusheratorScript;
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        crusheratorScript = crusherator.GetComponent<CrusheratorScript>();
    }

    private void Update() {

        /*
         * Inputs
        */ 
        if (Input.GetKeyDown("1"))
            crusheratorScript.MonoCrush();
        else if (Input.GetKeyDown("2"))
            crusheratorScript.Crush();
        else if(Input.GetKeyDown("3"))
            Destroy(Instantiate(bombPrefab, new Vector3(Random.Range(-6.4f, 6.4f), 15, -2), Quaternion.identity), 60);

        if (Input.GetKeyDown(KeyCode.J))
            SceneManager.LoadScene("Celeste");
    }
    void FixedUpdate() {
        rb.MovePosition(transform.position + new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * Time.fixedDeltaTime * speed);


    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other == null || other.gameObject == null)
            return;

        switch (other.tag) {
            case "Projectile":
                Destroy(Instantiate(dedPrefab, new Vector3(0, 2, -3), Quaternion.identity), 15);
                break;
            default:
                break;
        }
    }
}
