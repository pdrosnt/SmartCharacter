using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class CharacterSensores{

    public CharacterManager characterManager;
    
    public float minDistanceToBeInTarget = 1f;

    public bool ignoreXDistance = false;
    public bool ignoreYDistance = false;
    public bool ignoreZDistance = false;

    public float xDistance;
    public float yDistance;
    public float zDistance;

    public bool isInTarget = false;

    public void  DetectWithBehaviour() {

        if(characterManager.characterEngine.behaviourStack.Count < 1){
            
            isInTarget = false;
            
            return;
            
            }

        TargetDistance();
    }

    public void TargetDistance()
    {
        bool isInTargetx = false;
        bool isInTargety = false;
        bool isInTargetz = false;


        Transform target = characterManager.characterEngine.behaviourStack[0].target.transform;

        xDistance = Mathf.Abs(characterManager.transform.position.x - target.position.x);
         
        if (xDistance < minDistanceToBeInTarget || ignoreXDistance){

            isInTargetx = true;

        }

        yDistance = Mathf.Abs(characterManager.transform.position.y - target.position.y);
         
        if (yDistance < minDistanceToBeInTarget || ignoreYDistance){

            isInTargety = true;

        }

        zDistance = Mathf.Abs(characterManager.transform.position.z - target.position.z);
         
        if (zDistance < minDistanceToBeInTarget || ignoreZDistance){

            isInTargetz = true;

        }

        if(isInTargetx && isInTargety && isInTargetz){isInTarget = true;}else{isInTarget = false;}
    }
}
