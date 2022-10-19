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

    public float positiveSlopeMultiplier = 1f;
    public float negativeSlopeMultiplier = 1f;

    public float edgeImpuse = 1f;
    public float maxNegativeSlope = -0.5f;



    public Vector3 GetProjectedOnPlaneVelocity(Vector2 direction)
    {
        Vector3 velocity = Vector3.zero;

        velocity.x = direction.x;

        velocity.z = direction.y;

        if (brain.sensoresManager.isGrounded)
        {
            if (direction.magnitude > 0)
            {
                if (brain.sensoresManager.BackFowardDistancesDifference > 0)
                {

                    velocity.y = (brain.sensoresManager.BackFowardDistancesDifference) * positiveSlopeMultiplier;

                }
                else
                {

                    velocity.y = (brain.sensoresManager.BackFowardDistancesDifference) * negativeSlopeMultiplier;

                }
            }
        }

        return velocity.normalized;
    }


    public Vector3 GetVelocity()
    {
        UpdateInAirVariables();

        Vector3 velocity = GetProjectedOnPlaneVelocity(brain.inputHandler.directions);

        if (brain.sensoresManager.isGrounded)
        {

            tempGravit = (velocity.y);

        }
        else
        {
            tempGravit = -Mathf.Abs(inAirTime * gravityAcelleration);

            if (tempGravit > maxGravity)
            {
                tempGravit = maxGravity;
            }
        }

        velocity.y = tempGravit + minGravity;
        velocity.y *= horizontalSpeed;
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
