using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardController : MonoBehaviour
{
    public delegate void InputHandler(Inputs e);

    public event InputHandler InputEvent;    

    // Use this for initialization
    private void Update()
    {        
        if (InputEvent != null)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                InputEvent(Inputs.MoveLeft);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                InputEvent(Inputs.MoveRight);
            }
            if (Input.GetKey(KeyCode.Space))
            {
                InputEvent(Inputs.Jump);
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                InputEvent(Inputs.AimUp);
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                InputEvent(Inputs.Crouch);
            }
            if (Input.GetKey(KeyCode.Q))
            {
                InputEvent(Inputs.ThrowAnArrow);
            }
            if (Input.GetKey(KeyCode.Alpha1))
            {
                InputEvent(Inputs.SwitchToBow1);
            }
            if (Input.GetKey(KeyCode.KeypadEnter) || Input.GetKey(KeyCode.Return))
            {
                InputEvent(Inputs.Pause);
            }
        }
    }
}
