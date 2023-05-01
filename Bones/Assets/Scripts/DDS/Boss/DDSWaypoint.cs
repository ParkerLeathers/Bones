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
        foreach (GameObject i in wayPointObjs) {

            Vector2 pos1 = transform.position;
            Vector2 pos2 = i.transform.position;
            Vector2 up = Vector2.up / 6;
            Vector2 right = Vector2.right / 6;
            Debug.DrawLine(pos1, pos2, Color.white, 0.01f);
            Debug.DrawLine( pos1 + ( pos2 - pos1) * 5 / 6 + up + right, pos1 + (pos2 - pos1) * 5 / 6 - up - right);
            Debug.DrawLine(pos1 + (pos2 - pos1) * 5 / 6 + up - right, pos1 + (pos2 - pos1) * 5 / 6 - up + right);
            //Debug.DrawLine((Vector2)i.transform.position - Vector2.up/3 - Vector2.right/3 - (Vector2) transform.position / 6, (Vector2)i.transform.position + Vector2.up/3 + Vector2.right/3 -(Vector2)transform.position / 6);
        }
    }
#endif
}
