using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JoystickTest : MonoBehaviour
{
    void Update()
    {
        Gamepad gamepad = Gamepad.current;
        if (gamepad != null)
        {
            Vector2 move = gamepad.leftStick.ReadValue();
            Vector2 look = gamepad.rightStick.ReadValue();
            Debug.Log("Left Stick: " + move + " | Right Stick: " + look);
        }
        else
        {
            Debug.Log("No gamepad connected!");
        }
    }
}
