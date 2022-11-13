using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class BrainMovement
{
    public Brain brain;
    public float horizontalSpeed = 2;
    public float maxGravity = -9.8f;
    public float minGravity = -0.5f;
    public float gravityAcelleration = 0.1f;
    public float tempGravit = 0;
    public float inAirTime = 0.0f;

    public float slopeMultiplier = 1f;
    public float slope;
    public float jumpForce = 2f;
    public bool jumping = false;
    public float jumpingTime = 0f;
    public float maxJumpingTime = .5f;

    public Vector3 GetProjectedOnPlaneVelocity(Vector2 direction)
    {
        Vector3 velocity = Vector3.zero;

        velocity.x = direction.x;

        velocity.z = direction.y;

        if (brain.sensoresManager.isGrounded)
        {
            if (direction.magnitude > 0)
            {
                slope = brain.sensoresManager.height / brain.sensoresManager.groundRayHorizontalOffset * -1f;

                velocity.y = slope;

                //step boost
                bool step = brain.sensoresManager.groundBackward.normal.y > slope ? true : false;
                if(brain.sensoresManager.groundBackward.normal.y < 0.9f ||  slope < 0.9f){step = false;}
                if(step){
                    velocity.y += brain.sensoresManager.height * -1f;
                }
            }
        }

        return velocity;
    }


    public Vector3 GetVelocity()
    {   
        UpdateInAirVariables();

        Vector3 velocity = Vector3.zero;
        
        velocity.x = brain.inputHandler.directions.x;
        velocity.z = brain.inputHandler.directions.y;

        tempGravit = 0;
        
        if(jumping)
        {
            jumpingTime += Time.deltaTime;
            tempGravit = Mathf.Lerp(jumpForce,minGravity,jumpingTime / maxJumpingTime);

            if(jumpingTime > maxJumpingTime){jumping = false;}


        }else
        if (brain.sensoresManager.isGrounded)
        {

            velocity = GetProjectedOnPlaneVelocity(brain.inputHandler.directions);

            velocity.Normalize();

            velocity.y *= horizontalSpeed;

            if(jumping == false){jumping = brain.inputHandler.space.pressed;}

            jumpingTime = 0;
            tempGravit = 0;

        }
        else
        {
            if(jumping) inAirTime = 0;
           
            tempGravit += Mathf.Lerp(minGravity,maxGravity,inAirTime / gravityAcelleration);

            if(tempGravit > maxGravity)tempGravit = maxGravity;

        }

        velocity.y += tempGravit;
        velocity.x *= horizontalSpeed;
        velocity.z *= horizontalSpeed;

        return velocity;

    }

    private void UpdateInAirVariables()
    {
        if (!brain.sensoresManager.isGrounded)
        {
            inAirTime += Time.deltaTime;
        }
        else
        {
            inAirTime = 0;
        }
    }

    

}
