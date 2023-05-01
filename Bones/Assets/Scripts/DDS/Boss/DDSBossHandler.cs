using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDSBossHandler : MonoBehaviour
{

    [Header("GameObjects")]
    [SerializeField] private GameObject point;

    [Header("Components")]
    private BossHandler bossHandler;

    void Awake() 
    {
        bossHandler = GetComponent<BossHandler>();

    }

    
    void Update()
    {
        
    }
}
