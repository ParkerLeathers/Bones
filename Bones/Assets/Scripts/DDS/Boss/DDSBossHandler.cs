using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDSBossHandler : MonoBehaviour
{

    [Header("GameObjects")]
    [SerializeField] private GameObject point;

    private GameObject previousPoint;

    [Header("Components")]
    private BossHandler bossHandler;

    void Awake() 
    {
        bossHandler = GetComponent<BossHandler>();
        previousPoint = point;
    }

    
    void Update()
    {
        if (bossHandler.done) {
            DDSWaypoint pointScript = point.GetComponent<DDSWaypoint>();
            GameObject[] wayPointObjs = pointScript.wayPointObjs;
            if (wayPointObjs.Length == 1) {
                previousPoint = point;
                point = wayPointObjs[0];
            } else {
                GameObject[] potentialPoints = new GameObject[wayPointObjs.Length];
                int potLoc = 0; // now this is spatial partition
                foreach (GameObject i in wayPointObjs) // according to some random guy online this compiles in such a way that it still practices spatial partitioning. cool
                    if (!i.Equals(previousPoint)) {
                        potentialPoints[potLoc] = i;
                        potLoc += 1;
                    }
                previousPoint = point;
                point = potentialPoints[Random.Range(0, potLoc)];
            }
            bossHandler.MoveTo(point.transform.position);
        }
    }
}
