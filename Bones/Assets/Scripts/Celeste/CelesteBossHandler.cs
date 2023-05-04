using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelesteBossHandler : MonoBehaviour
{
    [Header("GameObjects")]
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject cutsceneHandler;

    [Header("Stats")]
    [SerializeField] private float MAX_DISTANCE;

    [Header("Components")]
    private BossHandler bossHandler;
    private CutsceneScript cutsceneScript;

    [Header("Waypoints")]
    [SerializeField] private GameObject[] wayPointObjs;


    private bool started = false;
    private Queue<Vector2> points = new();
    void Awake()
    {
        bossHandler = GetComponent<BossHandler>();
        cutsceneScript = cutsceneHandler.GetComponent<CutsceneScript>();
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
        started = true;
    }

    public void Next() {
        bossHandler.MoveTo(points.Dequeue());
    }
   
}
