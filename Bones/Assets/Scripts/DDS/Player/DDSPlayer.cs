using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DDSPlayer : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float SPEED = 3;
    [SerializeField] private float health = 100;

    [Header("GameObjects")]
    [SerializeField] private Text text;

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

    private void Death() {
        //todo IMPLEMENT
        Debug.Log("big money ukilled him");
    }

    private void UpdateHealth() {
        text.text = "" + (int) health;
        if (health < 1) //i do less than 1 so that way the display never says "0" despite having like 0.5
            Death();
    }

    private void TakeDamage(float dmg) {
        health -= dmg;
        UpdateHealth();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other == null || other.gameObject == null)
            return;

        switch (other.tag) {
            case "Projectile": //death
                BulletHandler bh = other.GetComponent<BulletHandler>();
                if (bh != null && !bh.ally) {
                    TakeDamage(bh.damage);
                    Destroy(other.gameObject); //heads up make sure u dont destroy the skull king before this can run
                }
                break;
            default:
                break;
        }
    }
}
