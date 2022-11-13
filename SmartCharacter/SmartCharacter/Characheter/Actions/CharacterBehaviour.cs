using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct AnimatorStateData{

    public string animatorStateName;

    public bool hasRootMotion;
}

public struct InstantiateDara
{

    public GameObject toInstatiate;

    public Vector3 intantiatePosition;

}

[Serializable]
public struct StateTimerData{

    public float maxTime;

    public float minTime;
}

[Serializable]
public struct MovementStateData{

    public Vector3 localDirection;
}

[Serializable]
public struct CharacterBehaviour{
    
    public ActionType action;

    public Transform target;

    public AnimatorStateData animatorStateData;

    public MovementStateData movementStateData;

    public StateTimerData stateTimerData;

    [Tooltip("depending on the behaviour type this is damage/animator property/interaction index")]
    public int z;


}
