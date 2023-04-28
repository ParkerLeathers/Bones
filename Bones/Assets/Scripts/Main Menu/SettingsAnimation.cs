using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsAnimation : MonoBehaviour
{
    [SerializeField] private float ANIMATION_SPEED;

    void Start()
    {
        
    }

    void Update()
    {
        transform.Rotate(new Vector3(0, 0, -ANIMATION_SPEED) * Time.deltaTime);
    }
}
