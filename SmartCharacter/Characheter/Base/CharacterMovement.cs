using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class CharacterMovement
{
    public CharacterManager characterManager;

    public void MoveWithBehaviour(){

        Transform target = characterManager.characterEngine.behaviourStack[0].target.transform;
        
        RigidbodyVelocity(target);

        RotateRB(target);

    }

    public void RigidbodyVelocity(Transform target)
    {

        Rigidbody rigidbody = characterManager.rb;

        Vector3 inputDirection = characterManager.characterEngine.behaviourStack[0].movementStateData.localDirection;

        Vector3 up = rigidbody.transform.up.normalized; 

        Vector3 backward = (rigidbody.position - target.position).normalized;

        Vector3 right = Vector3.Cross(backward,up);

        backward.y = 0;

        right.y = 0;

        rigidbody.velocity = Vector3.zero;

        Vector3 moveVectorHandler = Vector3.zero;

        moveVectorHandler = (inputDirection.x * right);

        moveVectorHandler += (inputDirection.y * up);

        moveVectorHandler += (inputDirection.z * -backward);

        rigidbody.velocity = moveVectorHandler;

    }

    public void RotateRB(Transform target, bool rotateX = false){

        Rigidbody rigidbody = characterManager.rb;

        rigidbody.transform.LookAt(target);

        Vector3 eulerRotation = rigidbody. transform.rotation.eulerAngles;
       
        if(!rotateX){
        eulerRotation.x = 0;
        }

        eulerRotation.z = 0;

        rigidbody.transform.eulerAngles = eulerRotation;

    }


    public void ConstrainRotation(bool x = false, bool y = false, bool z = false)
    {

        Rigidbody rigidbody = characterManager.rb;

        rigidbody.constraints = RigidbodyConstraints.None;

        if (x)
        {
            rigidbody.constraints = RigidbodyConstraints.FreezeRotationX;
            if(y){rigidbody.constraints |= RigidbodyConstraints.FreezeRotationY;}
            if(z){rigidbody.constraints |= RigidbodyConstraints.FreezeRotationZ;}
        }
        else if (y)
        {
            rigidbody.constraints = RigidbodyConstraints.FreezeRotationY;
            if(z){rigidbody.constraints |= RigidbodyConstraints.FreezeRotationZ;}

        }
        else if (z)
        {
            rigidbody.constraints = RigidbodyConstraints.FreezeRotationZ;
        }

        rigidbody.useGravity = false;

    }
  
}
