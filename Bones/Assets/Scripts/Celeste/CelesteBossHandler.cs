using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelesteBossHandler : MonoBehaviour
{
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

    }

    public void StartMoving() {
        started = true;
    }

    public void Next() {
        bossHandler.MoveTo(points.Dequeue());
    }
   
}
