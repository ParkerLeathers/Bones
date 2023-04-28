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
    [SerializeField]
    private GameObject bomberator;

    [Header("Prefabs")]
    [SerializeField]
    private GameObject dedPrefab;
    
    [Header("Misc")]
    public int points;

    private CrusheratorScript crusheratorScript;
    private BomberatorScript bomberatorScript;
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        crusheratorScript = crusherator.GetComponent<CrusheratorScript>();
        bomberatorScript = bomberator.GetComponent<BomberatorScript>();
    }

    private void Update() {

        if (Input.GetKeyDown(KeyCode.J))
            SceneManager.LoadScene("Celeste");
    }

    void FixedUpdate() {
        //movement
        if(!UniversalData.fakeDeath) //if you havent died yet (fallen for me trap)
            rb.MovePosition(transform.position + new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * Time.fixedDeltaTime * speed);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other == null || other.gameObject == null)
            return;

        switch (other.tag) {
            case "Projectile": //death
                Destroy(Instantiate(dedPrefab, new Vector3(0, 2, -3), Quaternion.identity), 15);
                SceneManager.LoadScene("GameOver");
                break;
            default:
                break;
        }
    }
}
