using UnityEngine;
using System;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;
public class PathFind
{
    int current;
    public LayerMask layerMask;
    List<PathPoint> pathPointsData;
    List<PathPoint> open = new List<PathPoint>();
    public void ClearMemory()
    {
        if (pathPointsData != null)
            {
                pathPointsData.Clear();
            }

            if(open != null)
            {
                open.Clear();
            }

            current = 0;
    }
    public PathPoint[] GetPath(Vector3Int _start, Vector3Int _target,int maxIterations)
    {
        //check if start and end are in the bounds of the map
        /*
        if (global_start.x > map_start.x && global_start.x < map_end.x){Debug.Log("outside of pathfiner bounds x");return null;}
        if (global_start.y > map_start.y && global_start.y < map_end.y){Debug.Log("outside of pathfiner bounds y");return null;}
        if (global_start.z > map_start.z && global_start.z < map_end.z){Debug.Log("outside of pathfiner bounds z");return null;}
        */

        // initialize variables

        if(pathPointsData == null || pathPointsData.Count == 0)
        {
            current = CreatePathPoint(_start);
            pathPointsData[current].parent_index = 0;
            open.Add(pathPointsData[current]);

        }else
        {
            current = pathPointsData.Aggregate((i,j) => i.h > j.h ? j : i).index;
        }

        int[] neighbours = GetNeighbours(pathPointsData[current].position);

        // loop
        int j = 0;
        while (open.Count > 0)
        {
            if (j > maxIterations) {
                Debug.Log("path too long to search more, returning best solution"); 
                break;
            }

            j++;
            current = open[0].index;

            if (Vector3Int.Distance(pathPointsData[current].position,_target) < 2)
            {
                Debug.Log("path finded");

                var r = ReconstructPath(open[0], _start);

                ClearMemory();

                return r;

            }

            open.RemoveAt(0);

            neighbours = GetNeighbours(pathPointsData[current].position);

            pathPointsData[current].closed = true;

            for (int i = 0; i < neighbours.Length; i++)
            {
                int n_index = neighbours[i];

                if (pathPointsData[n_index].walkable && !pathPointsData[n_index].closed)
                {
                    pathPointsData[n_index].parent_index = pathPointsData[current].index;

                    pathPointsData[n_index].h = Vector3Int.Distance(pathPointsData[n_index].position, _target);
                    pathPointsData[n_index].h += Vector3Int.Distance(pathPointsData[n_index].position, pathPointsData[current].position);

                    if (!open.Contains(pathPointsData[n_index]))
                    {
                        //organizing priority
                        bool index_finded = false;

                        for (int x = 0; x < open.Count; x++)
                        {
                            if (pathPointsData[n_index].h < open[x].h)
                            {
                                open.Insert(x, pathPointsData[n_index]);
                                index_finded = true;
                                break;
                            }
                        }
                        //the element has bigger h cost than all in open
                        if (!index_finded) { open.Add(pathPointsData[n_index]); }

                    }
                }
            }
        }

        Debug.Log("path not founded and there is no more options, returning best solution  " + current);

        return ReconstructPath(pathPointsData[current], _start);
    }
    private PathPoint[] ReconstructPath(PathPoint start, Vector3Int target)
    {
        List<PathPoint> _path = new List<PathPoint>();

        PathPoint current = start;
        int j = 1;

        for (int i = 0; i < j; i++)
        {
            if (_path.Contains(current))break;

            if(Vector3Int.Distance(current.position,target) > 2){

                _path.Add(current);
                current = pathPointsData[current.parent_index];
            }

            if(j < 500)j++;
        }

        return _path.ToArray();
    }
    private int CreatePathPoint(Vector3Int position)
    {
        PathPoint pointInMemory = null;

        if(pathPointsData != null){
            pointInMemory = pathPointsData.Find(p => p.position == position);
        }else
        {
            pathPointsData = new List<PathPoint>();
        }

        if (pointInMemory != null)
        {
            if (pointInMemory.position == position)
            {
                return pointInMemory.index;
            }
        }

        PathPoint pathPoint = new PathPoint();

        pathPoint.h = Mathf.Infinity;

        pathPoint.position = position;

        bool collided = Physics.CheckBox(position, Vector3.one * 0.5f, Quaternion.identity, layerMask, QueryTriggerInteraction.Ignore);

        pathPoint.walkable = false;

        if (collided == false) { pathPoint.walkable = Physics.CheckBox(position + Vector3Int.down, Vector3.one * 0.5f, Quaternion.identity, layerMask, QueryTriggerInteraction.Ignore); }

        int i = pathPointsData.Count;

        pathPoint.index = i;

        pathPointsData.Add(pathPoint);

        return i;
    }
    private int[] GetNeighbours(Vector3Int position)
    {
        int total = 26;

        int[] neighbours = new int[total];
        int i = 0;

        for (int x = -1; x < 2; x++)
        {
            for (int y = -1; y < 2; y++)
            {
                for (int z = -1; z < 2; z++)
                {
                    if (x == 0 && y == 0 && z == 0) continue;

                    Vector3Int global_pos = new Vector3Int(position.x + x, position.y + y, position.z + z);

                    int n = CreatePathPoint(global_pos);

                    neighbours[i] = n;

                    i++;
                }
            }
        }

        return neighbours;

    }
}
[Serializable]
public class PathPoint
{
    public Vector3Int position;
    public bool walkable;
    public float h;
    public int parent_index;
    public bool closed;
    public int index;
}


