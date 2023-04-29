using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelesteBossHandler : MonoBehaviour
{
    [Header("GameObjects")]
    [SerializeField] private GameObject player;

    [Header("Stats")]
    [SerializeField] private float MAX_DISTANCE;

    [Header("Components")]
    private BossHandler bossHandler;

    [Header("Waypoints")]
    [SerializeField] private GameObject[] wayPointObjs;


    private bool started = false;
    private Queue<Vector2> points = new();
    void Awake()
    {
        bossHandler = GetComponent<BossHandler>();
        loadQueue();
        StartMoving();
    }

    private void loadQueue() {

        foreach (GameObject i in wayPointObjs)
            points.Enqueue(i.transform.position);
    }

    void Update()
    {
        if (points.Count == 0)
            loadQueue();
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
