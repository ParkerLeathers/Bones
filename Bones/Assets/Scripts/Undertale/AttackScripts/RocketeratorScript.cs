using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketeratorScript : MonoBehaviour
{

    [Header("Prefabs")]
    [SerializeField]
    private GameObject rocketPrefab;
    void Start()
    {
        
    }
    void Update()
    {
        
    }

    public void Rocketerate() {
        Instantiate(rocketPrefab, transform.position, Quaternion.identity).SetActive(true);
    }
}
