using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelesteCameraMover : MonoBehaviour
{

    private Vector2Int screenLocation;
    private Camera cam;

    private float w;
    private float h;

    void Start()
    {
        cam = UnityEngine.Camera.main;
        h = cam.orthographicSize * 2;
        w = h * cam.aspect;
        Debug.Log(w);

        screenLocation = new Vector2Int((int) ((transform.position.x + w/2) / w), (int) ((transform.position.y + h/2) / h));
    }


    void Update()
    {

        Vector2Int tempLoc = new Vector2Int((int) ((transform.position.x + w/2)/ w), (int)((transform.position.y + h/2) / h));
        if(screenLocation != tempLoc) {
            cam.transform.position = new Vector3(tempLoc.x * w, tempLoc.y * h, cam.transform.position.z);
            screenLocation = tempLoc;
        }
    }
}
