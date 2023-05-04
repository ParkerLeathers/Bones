using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UndertaleBossScript : MonoBehaviour {

    [Header("GameObjects")]
    [SerializeField] private GameObject waveHandler;
    [SerializeField] private GameObject cutsceneHandler;

    [Header("Components")]
    private WaveHandler waveScript;
    private CutsceneScript cutsceneScript;

    private bool started = false;

    void Start()
    {
        waveScript = waveHandler.GetComponent<WaveHandler>();
        cutsceneScript = cutsceneHandler.GetComponent<CutsceneScript>();
    }

    void Update()
    {
        if (!started && cutsceneScript.done) {
            waveScript.StartWaves();
            started = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other == null || other.gameObject == null)
            return;

        if (other.name.Equals("Rocket(Clone)")) {//forgive me but im so low on time
            if (!waveScript.Next())
                SceneManager.LoadScene("Celeste");
            Destroy(other.gameObject);
        }
    }
}
