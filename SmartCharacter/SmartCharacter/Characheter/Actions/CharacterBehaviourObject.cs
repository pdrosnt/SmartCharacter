using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterBehaviourObject", menuName = "CharacterManager/Behaviour", order = 0)]
public class CharacterBehaviourObject : ScriptableObject {

    public CharacterBehaviour[] behavourScript;
    
}