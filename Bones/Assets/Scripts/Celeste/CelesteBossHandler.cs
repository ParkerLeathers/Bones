using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CelesteBossHandler : MonoBehaviour
{
    [Header("GameObjects")]
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject cutsceneHandler;
    [SerializeField] private GameObject audioSource;

    [Header("Stats")]
    [SerializeField] private float MAX_DISTANCE;

    [Header("Components")]
    private BossHandler bossHandler;
    private CutsceneScript cutsceneScript;
    private AudioScript audioScript;

    [Header("Waypoints")]
    [SerializeField] private GameObject[] wayPointObjs;


    private bool started = false;
    private Queue<Vector2> points = new();
    void Awake()
    {
        bossHandler = GetComponent<BossHandler>();
        cutsceneScript = cutsceneHandler.GetComponent<CutsceneScript>();
        audioScript = audioSource.GetComponent<AudioScript>();
        LoadQueue();
    }

    private void LoadQueue() {

        foreach (GameObject i in wayPointObjs)
            points.Enqueue(i.transform.position);
    }

    void Update()
    {
        if (!started)
            if (cutsceneScript.done)
                StartMoving();
            else
                return;

        if (points.Count == 0)
            LoadQueue();
        if (points.Count > 0 && bossHandler.done)
            Next();

        if (Vector2.Distance(transform.position, player.transform.position) > MAX_DISTANCE)
            bossHandler.Pause();
        else
            bossHandler.UnPause();
    }

    public void StartMoving() {
        audioScript.Play();
        started = true;
    }

    public void Next() {
        bossHandler.MoveTo(points.Dequeue());
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other == null || other.gameObject == null)
            return;

        switch (other.tag) {
            case "Player":
                SceneManager.LoadScene("DDS");
                break;
            default:
                break;
        }
    }

}
