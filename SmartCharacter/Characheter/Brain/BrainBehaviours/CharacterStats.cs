using UnityEngine;
using UnityEngine.Events;
using System;

[Serializable]
public struct statusBehaviours
{
    public UnityEvent minValueBehaviour;
    public UnityEvent maxValueBehaviour;
    public UnityEvent onAddChangeBehaviour;
    public UnityEvent onSubChangeBehaviour;

}

[Serializable]
public struct status
{   
    public string name;
    public int passiveDecay;
    public int maxValue;
    public int minValue;
    public int value;

    public bool Max_reset;
    public bool Min_reset;

    public statusBehaviours behavioursToTrigger;

}

[Serializable]
public class CharacterStats
{
    public status stat;
    float deltaTime = 0;
    public void ChangeValue()
    {   
        if(deltaTime < 1){deltaTime += Time.deltaTime;return;}
        deltaTime = 0;

        int sum = stat.value + stat.passiveDecay;

        if (sum >= stat.maxValue)
        { 
            Debug.Log("max");
            if(stat.Max_reset)stat.value = stat.minValue;
            else           
                stat.value = stat.maxValue;

            if (stat.behavioursToTrigger.maxValueBehaviour != null)
            {
                stat.behavioursToTrigger.maxValueBehaviour.Invoke();
            }
        }
        else if (sum <= stat.minValue)
        { 
            Debug.Log("min");
            if(stat.Min_reset)stat.value = stat.maxValue;
            else
                stat.value = stat.minValue;

            if (stat.behavioursToTrigger.minValueBehaviour != null)
            {
                stat.behavioursToTrigger.minValueBehaviour.Invoke();               
            }
        }
        else
        {
            stat.value = sum;

            if (stat.passiveDecay > 0)
            {
                if (stat.behavioursToTrigger.onAddChangeBehaviour != null)
                {
                    stat.behavioursToTrigger.onAddChangeBehaviour.Invoke();
                }
            }
            else
            {
                if (stat.behavioursToTrigger.onSubChangeBehaviour != null)
                {
                    stat.behavioursToTrigger.onSubChangeBehaviour.Invoke();

                }
            }
        }
    }
}