using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UndertaleBossScript : MonoBehaviour {

    [Header("GameObjects")]
    [SerializeField] private GameObject waveHandler;
    [SerializeField] private GameObject cutsceneHandler;
    [SerializeField] private GameObject audioSource;
    [SerializeField] private GameObject musicAudioSource;

    [Header("Components")]
    private WaveHandler waveScript;
    private CutsceneScript cutsceneScript;
    private AudioScript audioScript;
    private AudioScript musicAudioScript;

    [Header("AudioClips")]
    [SerializeField] AudioClip damage;

    private bool started = false;

    void Start()
    {
        waveScript = waveHandler.GetComponent<WaveHandler>();
        cutsceneScript = cutsceneHandler.GetComponent<CutsceneScript>();
        audioScript = audioSource.GetComponent<AudioScript>();
        musicAudioScript = musicAudioSource.GetComponent<AudioScript>();
    }

    void Update()
    {
        if (!started && cutsceneScript.done) {
            musicAudioScript.Play();
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
            audioScript.Play(damage);
            Destroy(other.gameObject);
        }
    }
}
