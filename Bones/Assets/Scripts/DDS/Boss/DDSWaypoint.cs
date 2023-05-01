using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DDSWaypoint : MonoBehaviour
{

    [Header("Waypoints")]
    public GameObject[] wayPointObjs;

#if UNITY_EDITOR
    void Update()
    {
        foreach(GameObject i in wayPointObjs)
            Debug.DrawLine(transform.position, i.transform.position, Color.white, 0.01f);
    }
#endif
}
