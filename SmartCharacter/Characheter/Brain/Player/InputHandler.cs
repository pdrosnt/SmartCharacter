using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class InputHandler
{
    public Vector2 directions;
    public Vector2 mouseDelta;
    public InputButton shift = new InputButton();
    public InputButton space = new InputButton();
    public InputButton capsLock = new InputButton();
    public InputButton mouseL = new InputButton();
    public InputButton mouseR = new InputButton(); 


    public void UpdateWithPlayerInputs() {

        shift.UpdateButton(Input.GetKey(KeyCode.LeftShift));

        space.UpdateButton(Input.GetKey(KeyCode.Space));

        capsLock.UpdateButton(Input.GetKey(KeyCode.CapsLock));

        mouseL.UpdateButton(Input.GetKey(KeyCode.Mouse0));

        mouseR.UpdateButton(Input.GetKey(KeyCode.Mouse1));

        directions.x = Input.GetAxisRaw("Horizontal");
        directions.y = Input.GetAxisRaw("Vertical");

        mouseDelta.x = Input.GetAxis("Mouse Y");
        mouseDelta.y = Input.GetAxis("Mouse X");

    } 

 

}

