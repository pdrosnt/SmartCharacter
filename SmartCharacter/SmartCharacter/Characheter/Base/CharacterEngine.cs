using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class CharacterEngine
{
    public bool finishTask = false;
    public float timer = 0;
    public bool stateChanged = false;
    public bool firstFrame = true;

    public CharacterManager characterManager;

    CharacterAction actions = new CharacterAction();

    public bool loop = false;

    [SerializeField]
    public List<CharacterBehaviour> behaviourStack;

    public bool Work()
    {
        stateChanged = false;

        if (behaviourStack.Count > 0)
        {
            timer += Time.deltaTime;

            Transform target;

            if (behaviourStack[0].target == null)
            {
                behaviourStack.RemoveAt(0);

                timer = 0;

                return false;
            }

            target = behaviourStack[0].target.transform;

            actions.ActionByType(characterManager, behaviourStack[0].action);

            firstFrame = false;

            if (timer > behaviourStack[0].stateTimerData.maxTime && behaviourStack[0].stateTimerData.maxTime > 0) { finishTask = true; }

            bool minTimeLock = false;

            if (behaviourStack[0].stateTimerData.minTime > 0)
            {
                minTimeLock = true;

                if (timer > behaviourStack[0].stateTimerData.minTime)
                {
                    minTimeLock = false;
                }
            }


            if (finishTask && !minTimeLock)
            {
                CharacterBehaviour temp = behaviourStack[0];

                behaviourStack.RemoveAt(0);

                if (loop) { behaviourStack.Add(temp); }

                finishTask = false;

                timer = 0;

                stateChanged = true;

                firstFrame = true;

                return false;

            }

            return true;
        }

        return false;
    }

    public void StackBehaviour(CharacterBehaviour behaviour)
    {

        behaviourStack.Add(behaviour);

    }

    public void InsertBehaviour(CharacterBehaviour behaviour, int index)
    {

        if (index < behaviourStack.Count)
        {

            behaviourStack.Insert(index, behaviour);

        }
        else
        {

            behaviourStack.Add(behaviour);
        }

    }

    public void OvewriteBehaviour(CharacterBehaviour behaviour)
    {

        if (behaviourStack.Count > 0)
        {

            behaviourStack.RemoveAt(0);

            behaviourStack.Insert(0, behaviour);

            finishTask = false;

            timer = 0;

            stateChanged = true;

            firstFrame = true;

        }
        else
        {

            behaviourStack.Add(behaviour);


        }

    }



}
