using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputManager {


    public enum InputName {
        Button1,
        Button2,
        Button3,
        Up,
        Down,
        Left,
        Right
    }

    private static readonly Dictionary<InputName, List<KeyCode>> map = new();

    static InputManager() {
        List<KeyCode> button1 = new();
        button1.Add(KeyCode.Z);
        List<KeyCode> button2 = new();
        button2.Add(KeyCode.X);
        List<KeyCode> button3 = new();
        button3.Add(KeyCode.C);

        List<KeyCode> up = new();
        up.Add(KeyCode.UpArrow);
        up.Add(KeyCode.W);
        List<KeyCode> down = new();
        down.Add(KeyCode.DownArrow);
        down.Add(KeyCode.S);
        List<KeyCode> left = new();
        left.Add(KeyCode.LeftArrow);
        left.Add(KeyCode.A);
        List<KeyCode> right = new();
        right.Add(KeyCode.RightArrow);
        right.Add(KeyCode.D);

        map[InputName.Button1] = button1;
        map[InputName.Button2] = button2;
        map[InputName.Button3] = button3;

        map[InputName.Up] = up;
        map[InputName.Down] = down;
        map[InputName.Left] = left;
        map[InputName.Right] = right;

    }

    public static bool GetKeyDown(InputName key) {
        foreach (KeyCode i in map[key])
            if (Input.GetKeyDown(i))
                return true;
        return false;
    }

    public static bool GetKey(InputName key) {
        foreach (KeyCode i in map[key])
            if (Input.GetKey(i))
                return true;
        return false;
    }

    public static bool GetKeyUp(InputName key) {
        foreach (KeyCode i in map[key])
            if (Input.GetKeyUp(i))
                return true;
        return false;
    }
}
