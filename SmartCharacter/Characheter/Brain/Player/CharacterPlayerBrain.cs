using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPlayerBrain : Brain
{
    BehaviourFabric behaviourFabric = new BehaviourFabric();

    public Transform camTransform;

    public bool rotationLockedWithCamera = false;


    protected new void Awake()
    {
        base.Awake();

        Debug.Log("awake 2");

        characterManager = GetComponent<CharacterManager>();
        if (characterManager == null)
        {
            characterManager = gameObject.AddComponent<CharacterManager>();
        }

        target = new GameObject().transform;

        target.name = "player movement target";

        if (camTransform == null)
        { camTransform = Camera.main.transform; }

    }

    private void Update()
    {
        sensoresManager.UpdateSensors();

        Cursor.lockState = CursorLockMode.Locked;

        inputHandler.UpdateWithPlayerInputs();

        UpdateMoveInputsRelativeToCamera();

        Vector3 velocity = movementManager.GetVelocity();

        characterManager.characterEngine.OvewriteBehaviour(behaviourFabric.CreateBehaviour(target.transform, velocity,0, 0));

    }

    public void UpdateMoveInputsRelativeToCamera()
    {

        Vector3 camFoward = camTransform.forward;

        Vector3 camRight = camTransform.right;

        Vector3 targetDirection = Vector3.zero;

        camFoward.y = 0;
        camRight.y = 0;

        camFoward.Normalize();
        camRight.Normalize();

        if (rotationLockedWithCamera)
        {

            targetDirection = camFoward;

            target.transform.position = transform.position + targetDirection;

        }
        else
        {
            targetDirection = inputHandler.directions.x * camRight;
            targetDirection += inputHandler.directions.y * camFoward;

            targetDirection *= 2;

            targetDirection += transform.forward;

            target.transform.position = Vector3.Lerp(target.transform.position, transform.position + targetDirection,1f);

        }


        Vector3 direction = Vector3.zero;

        Vector3 up = transform.up;

        Vector3 foward = -(transform.position - target.transform.position);

        Vector3 right = Vector3.Cross(foward, up);

        foward.y = 0;
        right.y = 0;


        foward.Normalize();
        right.Normalize();

        Vector3 moveVectorHandler = Vector3.zero;

        float angle = Vector3.SignedAngle(foward, camFoward, transform.up);

        direction.x = inputHandler.directions.x;
        direction.z = inputHandler.directions.y;

        direction = Quaternion.AngleAxis(angle, transform.up) * direction;

        direction.Normalize();

        inputHandler.directions.x = direction.x;
        inputHandler.directions.y = direction.z;
    }


}
