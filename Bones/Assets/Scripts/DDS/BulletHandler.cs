using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHandler : MonoBehaviour
{

    public float speed = 3;
    public float damage = 3;
    public bool ally = false;
    void Start()
    {

    }

    
    void Update()
    {
        transform.position += (Vector3) (speed * Time.deltaTime * (Vector2) (transform.rotation * Vector2.right));
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other == null || other.gameObject == null)
            return;

        switch (other.tag) {
            case "Wall":
                Destroy(this.gameObject);
                break;
            default:
                break;
        }
    }
}
