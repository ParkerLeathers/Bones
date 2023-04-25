using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberatorScript : MonoBehaviour
{

    [Header("Stats")]
    [SerializeField]
    private float inverseSpeed;
    [Header("Prefabs")]
    [SerializeField]
    private GameObject bombPrefab;


    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void Bombard(float seconds) {

        StartCoroutine(BombardRoutine());


        IEnumerator BombardRoutine() {
            float initTime = Time.realtimeSinceStartup;
            while (Time.realtimeSinceStartup - initTime < seconds) {
                yield return new WaitForSeconds(inverseSpeed);
                Destroy(Instantiate(bombPrefab, new Vector3(Random.Range(-6.4f, 6.4f), 6, -2), Quaternion.identity), 4); //good spot for data locality change
            }
        }
    }
}
