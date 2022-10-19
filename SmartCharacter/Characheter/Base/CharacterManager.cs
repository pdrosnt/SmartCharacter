using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [HideInInspector]
    public Rigidbody rb;
    [HideInInspector]
    public Collider movementCollider;
    [HideInInspector]
    public Animator anim;

    public CharacterEngine characterEngine;
    public CharacterAnimator animatorManager;
    public CharacterMovement movementManager;
    public CharacterSensores sensoresManager;

    private void Awake()
    {

        if (characterEngine == null) { 
            
            characterEngine = new CharacterEngine();
            characterEngine.behaviourStack = new List<CharacterBehaviour>();
            
        }
        
        characterEngine.characterManager = this;

        if (animatorManager == null) { animatorManager = new CharacterAnimator(); }
        animatorManager.characterManager = this;

        if (movementManager == null) { movementManager = new CharacterMovement(); }
        movementManager.characterManager = this;

        if (sensoresManager == null) { sensoresManager = new CharacterSensores(); }
        sensoresManager.characterManager = this;


        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {

            rb = gameObject.AddComponent<Rigidbody>();

            movementManager.ConstrainRotation(true, true, true);

            rb.useGravity = true;

        }

        movementCollider = GetComponent<Collider>();
        if (movementCollider == null)
        {

            movementCollider = gameObject.AddComponent<CapsuleCollider>();

            Vector3 centerOffset = new Vector3(0, .5f, 0);

            gameObject.GetComponent<CapsuleCollider>().center += centerOffset;

        }

        anim = GetComponent<Animator>();
        if(anim == null){ anim = GetComponentInChildren<Animator>();}
        if(anim != null){ 
            
            animatorManager.animator = anim;
            
            anim.applyRootMotion = false;
            
        }

    }

    private void Update()
    {

        if (characterEngine.Work())
        {   
            sensoresManager.DetectWithBehaviour();
            animatorManager.AnimateWithBehaviour();            
            movementManager.MoveWithBehaviour();
            
        }else{

            animatorManager.ResetMoveParametersToZero();

        }

    }

}
