using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class BrainSensores
{
    public enum terrainType
    {

        plane,
        upSlope,
        downSlope,
        edge,
        step,
        peak

    }

    public terrainType fowardTerrainType;

    public Brain brain;

    public Transform characterTransform;

    public ColisionSensor colisionSensor;

    #region raycast

    [InspectorName("raycast options")]
    public float averageGroundDistance;
    public float BackFowardDistancesDifference;
    public Vector3 CenterDirectionNormal;
    public Vector3 BackFowardNormalAverage;

    public RaycastHit groundFoward;
    public RaycastHit groundBackward;

    public RaycastHit wallFoward;

    public float groundRayVerticalOffset = 1f;
    public float groundRayHorizontalOffset = 1f;

    public float[] groundDistances = new float[2];
    public Vector3[] groundNormals = new Vector3[2];

    public float minDistanceToBeGrouded = .5f;

    #endregion

    public bool isGrounded = true;

    public LayerMask layerMask;

    public float edgethreshold = 0.05f;
    public bool isInEdge;
    
    public float stepThreshold = 0.05f;
    public bool isInStep;
    public bool isInPlane;
    public bool isInUpSlope;
    public bool isInDownSlope;


    public void CastMovementDirectionRay(Vector3 dir,ref RaycastHit hit, float angle)
    {
        if(dir.magnitude == 0)return;

        Vector3 orign = characterTransform.position;

        dir.y = 0;

        Vector3 walkingDirection = characterTransform.forward;

        float rotationAngle = Vector3.SignedAngle(Vector3.forward, dir, characterTransform.up);

        rotationAngle += angle;

        walkingDirection = Quaternion.AngleAxis(rotationAngle, characterTransform.up) * characterTransform.forward;

        orign += walkingDirection * groundRayHorizontalOffset;
        orign.y += groundRayVerticalOffset;

        Physics.Raycast(orign, -characterTransform.up, out hit, 500f, layerMask);

        Debug.DrawRay(orign, -characterTransform.up, Color.blue);

        float localNormalAngle = Vector3.SignedAngle(characterTransform.forward, dir, characterTransform.up);

        hit.normal = Quaternion.AngleAxis(localNormalAngle, characterTransform.up) * hit.normal;

    }

    public void FowardRaysAverages()
    {
        averageGroundDistance = (groundFoward.distance + groundBackward.distance) / 2;

        BackFowardDistancesDifference = (groundFoward.distance - groundBackward.distance );

        BackFowardNormalAverage = (groundBackward.normal + groundFoward.normal) / 2;

        CenterDirectionNormal = new Vector3(groundFoward.normal.x,groundRayHorizontalOffset*2,BackFowardDistancesDifference).normalized;

    }

    public void UpdateSensors()
    {
        CharacterEngine cEngine =  characterTransform.GetComponent<CharacterManager>().characterEngine;

        Vector3 inputsDir = cEngine.behaviourStack.Count == 0 ? Vector3.zero : cEngine.behaviourStack[0].movementStateData.localDirection;

        CastMovementDirectionRay(inputsDir, ref groundFoward, 0.0f);
        CastMovementDirectionRay(inputsDir, ref groundBackward, 180.0f);

        FowardRaysAverages();

        UpdateIsGrounded();

        groundDistances[0] = groundFoward.distance;
        groundDistances[1] = groundBackward.distance;

        groundNormals[0] = groundFoward.normal;
        groundNormals[1] = groundBackward.normal;
    }

    public void UpdateIsGrounded()
    {
        float tempMinDistanceModifier = 0;

        float tempMinDistanceToBeGrounded = minDistanceToBeGrouded + tempMinDistanceModifier;

        if ((groundBackward.distance <= minDistanceToBeGrouded) || (groundFoward.distance <= minDistanceToBeGrouded)) { isGrounded = true; } else { isGrounded = false; }

        CheckTerrainType();

    }

    public void AdjustRaycastHorizontalOffset(Collider collider)
    {
        groundRayHorizontalOffset = collider.bounds.extents.x + 0.001f;
    }


    public static bool FastApproximately(float a, float b, float threshold)
    {
        return ((a - b) < 0 ? ((a - b) * -1) : (a - b)) <= threshold;
    }

    public bool IsInPeak()
    {

        bool IsInPeak = false;

        if (groundBackward.normal.z < 0 && groundFoward.normal.z > 0) { IsInPeak = true; }

        return IsInPeak;

    }

    public void CheckTerrainType()
    {
        isInStep = false;
        isInEdge = false;
        isInDownSlope = false;
        isInUpSlope = false;


        if (CenterDirectionNormal.z > groundBackward.normal.z + edgethreshold)
        {
            
            isInEdge = true;
                
        }

        if (CenterDirectionNormal.z < groundBackward.normal.z - stepThreshold)
        {
            
            isInStep = true;
                
        }

        if(CenterDirectionNormal.z > 0)
        {
            isInDownSlope = true;
        }

        if(CenterDirectionNormal.z < 0)
        {
            isInUpSlope = true;
        }
    }   

}
