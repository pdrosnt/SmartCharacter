using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class Brain : MonoBehaviour
{
    
    public CharacterManager characterManager;
    public BrainSensores sensoresManager;
    public InputHandler inputHandler;
    public BrainMovement movementManager;
    public Transform target;
    protected void Awake() {
        
        Debug.Log("awake 1");

        if(inputHandler == null){
            inputHandler = new InputHandler();
        }
        
        characterManager = GetComponent<CharacterManager>();

        if(sensoresManager == null){
        sensoresManager = new BrainSensores();}
        sensoresManager.brain = this;
        sensoresManager.characterTransform = transform;

        sensoresManager.AdjustRaycastHorizontalOffset(characterManager.GetComponent<Collider>());

        ColisionSensor colisionSensor = GetComponent<ColisionSensor>();
        if(colisionSensor != null){sensoresManager.colisionSensor = colisionSensor;}


        if(movementManager == null){movementManager = new BrainMovement();}
        movementManager.brain = this;

    }

    private void Update() {

        sensoresManager.UpdateSensors();

        React();

    }

    public void React()
    {


    }


}
