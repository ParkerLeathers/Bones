using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputManager {


    public enum InputName {
        Button1,
        Button2,
        Button3
    }

    private static readonly Dictionary<InputName, KeyCode> map = new();

    static InputManager() {


        map[InputName.Button1] = KeyCode.Z;
        map[InputName.Button2] = KeyCode.X;
        map[InputName.Button3] = KeyCode.Z;


    }

    public static bool GetKeyDown(InputName key) {
        if (Input.GetKeyDown(map[key]))
                return true;
        return false;
    }

    public static bool GetKey(InputName key) {
        if (Input.GetKey(map[key]))
                return true;
        return false;
    }

    public static bool GetKeyUp(InputName key) {
        if (Input.GetKeyUp(map[key]))
                return true;
        return false;
    }

    public static void Set(InputName key, KeyCode code) {
        map[key] = code;
    }
}
