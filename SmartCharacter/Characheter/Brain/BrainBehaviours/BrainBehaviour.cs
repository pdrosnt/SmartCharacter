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
    PathFind pathfinder = new PathFind();

    public LayerMask ground;
    public List<CharacterStats> brain_traits;
    public Transform currentTarget;
    public PathPoint[] path;
    public List<Transform> path_transforms;
    public CharacterBehaviour[] GetAiBehaviour(Transform transform)
    {
        List<CharacterBehaviour> behaviours = new List<CharacterBehaviour>();
        if (path.Length == 0)
        {
            Debug.Log("0");
        
                Debug.Log("2");
                if (currentTarget != null)
                {
                    pathfinder.layerMask = ground;
                    Debug.Log("3");
                    path = pathfinder.GetPath(Vector3Int.FloorToInt(transform.position), Vector3Int.FloorToInt(currentTarget.position), 3000);

                    for (int i = 0; i < path.Length; i++)
                    {
                        Debug.Log(i);
                        PathPoint p = path[i];
                        GameObject temp = new GameObject();
                        Transform t = temp.transform;
                        t.position = p.position;
                        path_transforms.Add(t);
                        behaviours.Add(BehaviourFabric.CreateBehaviour(t, Vector3.forward, 0, 0));
                    }
                }
        
        }

        return behaviours.ToArray();
    }

}
