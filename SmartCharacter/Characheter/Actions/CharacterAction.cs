using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum ActionType
{

    none,
    InteractWith,
    GoToTarget,
    AttackTarget
}

[Serializable]
public class CharacterAction
{
    public void ActionByType(CharacterManager characterManager, ActionType actionType)
    {

        if (actionType == ActionType.none)
        {

        }

        if (actionType == ActionType.GoToTarget)
        {
            GoToTarget(characterManager);
        }

        if (actionType == ActionType.AttackTarget)
        {
            Attack(characterManager);
        }

        if (actionType == ActionType.InteractWith)
        {
            Interact(characterManager);
        }
    }

    public void GoToTarget(CharacterManager characterManager)
    {
        if (characterManager.sensoresManager.isInTarget)
        {

            characterManager.characterEngine.finishTask = true;

        }

    }

    public void Attack(CharacterManager characterManager)
    {
        Damagable damagable = characterManager.characterEngine.behaviourStack[0].target.GetComponent<Damagable>();

        Attacker attacker = characterManager.GetComponent<Attacker>();

        if (damagable != null)
        {
            if (attacker != null)
            {
                attacker.Attack(damagable);

            }else
            if (characterManager.sensoresManager.isInTarget)
            {
                if(characterManager.characterEngine.firstFrame){

                damagable.SimpleDamage(characterManager.characterEngine.behaviourStack[0].z);

                }
            }
        }

        characterManager.characterEngine.finishTask = true;

    }

    public void Interact(CharacterManager characterManager)
    {
        InteractableCharacter interactable = characterManager.characterEngine.behaviourStack[0].target.GetComponent<InteractableCharacter>();

        if (characterManager.sensoresManager.isInTarget)
        {
            if (interactable != null)
            {
                interactable.BeInteracted();

                interactable = null;
            }

            characterManager.characterEngine.finishTask = true;
        }
    }

    public void Spaw()
    {

    }

    public void Destroy()
    {


    }
}
