using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    
    [Header("Movement")]
    public float speed;

    [Header("Rotation")]
    public float maxRotSpeed;
    public float rotAcceleration;
    public float deltaRotWeight;
    private float rotVel;

    [Header("Game Objects")]
    public GameObject player;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rotVel = 0;
    }

    
    void FixedUpdate()
    {
        float angle = Vector3.SignedAngle(transform.right, player.transform.position - transform.position, Vector3.one);
        float tarRotVel = angle * deltaRotWeight;

        rotVel += rotAcceleration * (tarRotVel - rotVel) * Time.fixedDeltaTime;

        if (Mathf.Abs(rotVel) > maxRotSpeed)
            rotVel = maxRotSpeed * Mathf.Sign(rotVel);

        transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + rotVel * Time.fixedDeltaTime);

        transform.position += transform.right * speed * Time.fixedDeltaTime;
    }
}
