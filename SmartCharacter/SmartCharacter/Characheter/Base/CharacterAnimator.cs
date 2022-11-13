using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class CharacterAnimator
{
    public CharacterManager characterManager;

    public Animator animator;

    string lastAnimationPlayed;

    int lastLayerIndex = 0;

    public void SyncMoveParametersWithInputs()
    {

        Vector3 inputsDirection = characterManager.characterEngine.behaviourStack[0].movementStateData.localDirection;

        animator.SetFloat("Horizontal", inputsDirection.x, 0.1f, Time.deltaTime);
        animator.SetFloat("Vertical", inputsDirection.z, 0.1f, Time.deltaTime);

    }

    public void ResetMoveParametersToZero()
    {
        if (animator == null) return;
        
        animator.SetFloat("Horizontal", 0.0f, 0.1f, Time.deltaTime);
        animator.SetFloat("Vertical", 0.0f, 0.1f, Time.deltaTime);

    }

    public void AnimateWithBehaviour()
    {
        if (animator == null) return;

        SyncMoveParametersWithInputs();

        string animatorStateName = characterManager.characterEngine.behaviourStack[0].animatorStateData.animatorStateName;

        int animatorStateID = Animator.StringToHash(animatorStateName);

        for (int i = 0; i < characterManager.anim.layerCount; i++)
        {
            if (characterManager.characterEngine.stateChanged)
            {

                if (characterManager.anim.HasState(i, animatorStateID))
                {
                    characterManager.anim.CrossFade(animatorStateName, .1f);

                    lastAnimationPlayed = animatorStateName;

                    lastLayerIndex = i;

                }
            }
        }



    }

}