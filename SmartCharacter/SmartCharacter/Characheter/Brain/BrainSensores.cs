using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class BrainSensores
{
    public Brain brain;
    public Transform characterTransform;
    public ColisionSensor colisionSensor;

    #region raycast
    [InspectorName("raycast options")]
    public RaycastHit groundFoward;
    public RaycastHit groundBackward;

    public float groundRayVerticalOffset = .5f;
    public float groundRayHorizontalOffset = 1f;
    public float minDistanceToBeGrounded = .5f;
    public float distaceToBeGroundedSlopeCompensation = 2;
    public float groundAngle = 0;
    #endregion

    #region data_debbug 
    public float height;
    public float groud_foward_distance;
    public float groud_center_distance;
    public Vector3 groud_normal_f;
    public Vector3 groud_normal_c;

    public float tempMinDistanceModifier;
    #endregion
    public bool isGrounded = true;
    public LayerMask layerMask;

    public void CastMovementDirectionRay(Vector3 dir,ref RaycastHit hit, float angle)
    {
        if(dir.magnitude == 0)return;

        Vector3 orign = characterTransform.position;

        dir.y = 0;

        Vector3 walkingDirection = characterTransform.forward;

        float rotationAngle = Vector3.SignedAngle(Vector3.forward, dir, characterTransform.up);

        rotationAngle += angle;

        walkingDirection = Quaternion.AngleAxis(rotationAngle, characterTransform.up) * characterTransform.forward;

        orign += walkingDirection * (angle == 0 ? groundRayHorizontalOffset : .0f);
        orign.y += groundRayVerticalOffset;

        Physics.Raycast(orign, -characterTransform.up, out hit, 500f, layerMask);

        Debug.DrawRay(orign, -characterTransform.up * 5, Color.blue);

        float localNormalAngle = Vector3.SignedAngle(characterTransform.forward, dir, characterTransform.up);

        hit.normal = Quaternion.AngleAxis(localNormalAngle, characterTransform.up) * hit.normal;

    }
    public void UpdateSensors()
    {
        var behaviourStack = characterTransform.GetComponent<CharacterManager>().characterEngine.behaviourStack;
        Vector3 inputsDir = behaviourStack.Count>0 ? behaviourStack[0].movementStateData.localDirection : Vector3.zero;

        CastMovementDirectionRay(inputsDir, ref groundFoward, 0.0f);
        CastMovementDirectionRay(inputsDir, ref groundBackward, 180.0f);

        groud_center_distance = groundBackward.distance;
        groud_foward_distance = groundFoward.distance;
        
        groud_normal_c = groundBackward.normal;
        groud_normal_f = groundFoward.normal;

        UpdateIsGrounded();

    }

    public void UpdateIsGrounded()
    {
        tempMinDistanceModifier =  (1 - groundBackward.normal.y) * distaceToBeGroundedSlopeCompensation;

        float tempMinDistanceToBeGrounded = minDistanceToBeGrounded + tempMinDistanceModifier;

        if ((Mathf.Min(groundBackward.distance,groundFoward.distance) <= tempMinDistanceToBeGrounded)) { isGrounded = true; } else { isGrounded = false; }

        CheckTerrainType();

    }

    public void AdjustRaycastHorizontalOffset(Collider collider)
    {

        groundRayHorizontalOffset = collider.bounds.extents.x + 0.1f;
        groundRayVerticalOffset = collider.bounds.extents.y + 0.1f;

    }

    public void CheckTerrainType()
    {
        float distance = groundRayHorizontalOffset;
        height = (groundBackward.point.y - groundFoward.point.y);

        groundAngle = Vector2.SignedAngle(Vector2.right,new Vector2(distance,height));
    }

}
