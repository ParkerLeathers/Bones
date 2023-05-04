using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScript : MonoBehaviour
{
    void Start()
    {
        
    }


    void Update()
    {
        if (InputManager.GetKey(InputManager.InputName.Button1))
            SceneManager.LoadScene("DDS");
    }
}
