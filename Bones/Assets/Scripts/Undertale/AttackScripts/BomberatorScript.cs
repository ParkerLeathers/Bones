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
    [Header("GameObjects")]
    [SerializeField] private GameObject audioSource;
    [Header("AudioClips")]
    [SerializeField] private AudioClip bomberate;
    [Header("Components")]
    private AudioScript audioScript;
    void Start()
    {
        audioScript = audioSource.GetComponent<AudioScript>();
    }

    
    void Update()
    {
        
    }

    public void Bombard(float seconds) {

        audioScript.Play(bomberate);

        StartCoroutine(BombardRoutine());


        IEnumerator BombardRoutine() {
            float initTime = Time.realtimeSinceStartup;
            while (Time.realtimeSinceStartup - initTime < seconds) {
                yield return new WaitForSeconds(inverseSpeed);
                Destroy(Instantiate(bombPrefab, new Vector3(Random.Range(-6.4f, 6.4f), 6, -2), Quaternion.identity), 4);
            }
        }
    }
}
