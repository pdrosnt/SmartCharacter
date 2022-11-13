using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableCharacter : MonoBehaviour
{
    CharacterManager characterManager;

    public List<CharacterBehaviour> behavioursToStack;

    private void Awake()
    {

        characterManager = GetComponent<CharacterManager>();
        if (characterManager == null)
        { characterManager = gameObject.AddComponent<CharacterManager>(); }

    }

    public void BeInteracted(int behaviourIndex = 0)
    {
        if (behavioursToStack.Count > 0)
        {
            if(behavioursToStack.Count < behaviourIndex)
            {
                CharacterBehaviour behaviourToPerform = behavioursToStack[behaviourIndex];

                characterManager.characterEngine.InsertBehaviour(behaviourToPerform,0);

            }else{

                CharacterBehaviour behaviourToPerform = behavioursToStack[behavioursToStack.Count - 1];

                characterManager.characterEngine.InsertBehaviour(behaviourToPerform,0);

            }

        }

    }
}
