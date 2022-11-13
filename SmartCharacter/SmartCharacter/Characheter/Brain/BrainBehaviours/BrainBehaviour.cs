using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public enum AIState
{
    idle,
    followingPath,
    combat,
    doingTask
}

[Serializable]
public class BrainBehaviour
{
    public List<CharacterStats> brain_traits;
    public Transform currentTarget;
    PathFind pathFind = new PathFind();
    public PathPoint[] path;
    public List<Transform> path_transforms;
    public CharacterBehaviour[] GetAiBehaviour()
    {
        List<CharacterBehaviour> behaviours = new List<CharacterBehaviour>();

        return behaviours.ToArray();
    }

    public void fillPath(Transform transform){
        path = pathFind.CreatePathPoints();
      
    }

    void OnDrawGizmos()
    {

        foreach(PathPoint p in path)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(p.gridPosition, 1);
        }
        
         
    }

}
