using System.Collections.Generic;
using UnityEngine;

public static class BehaviourFabric
{
    public static CharacterBehaviour CreateBehaviour(Transform target,Vector3 velocity, float maxTime, float minTime){
     
        CharacterBehaviour behaviour = new CharacterBehaviour();

        behaviour.movementStateData.localDirection.x = velocity.x;
        behaviour.movementStateData.localDirection.y = velocity.y;
        behaviour.movementStateData.localDirection.z = velocity.z;

        behaviour.action = ActionType.none;

        behaviour.target = target;

        return behaviour;
    }

}
