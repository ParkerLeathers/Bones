using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    [Header("GameObjects")]
    [SerializeField] private CelestePlayer player;

    [Header("Components")]
    private BoxCollider2D bc;
    private BoxCollider2D pbc;


    void Awake()
    {
        bc = GetComponent<BoxCollider2D>();
        pbc = player.GetComponent<BoxCollider2D>();
    }

    
    void Update()
    {
        bc.enabled = (pbc.bounds.min.y) > (bc.bounds.max.y);
    }
}
