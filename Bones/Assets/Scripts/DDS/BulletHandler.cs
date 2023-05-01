using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHandler : MonoBehaviour
{

    public float speed = 3;
    public float damage = 3;
    void Start()
    {

    }

    
    void Update()
    {
        transform.position += (Vector3) (speed * Time.deltaTime * (Vector2) (transform.rotation * Vector2.right));
    }
}
