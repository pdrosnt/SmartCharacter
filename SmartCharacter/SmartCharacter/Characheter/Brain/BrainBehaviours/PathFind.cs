using UnityEngine;
using System;
using UnityEditor;

public class PathFind : MonoBehaviour
{
    public bool solved = false;
    public float collisionRetreat = 1f;

    public Vector3Int start = Vector3Int.zero;
    public Vector3Int end = Vector3Int.one * 200;
    public PathPoint[] pathPoints = null;

    public PathPoint[] CreatePathPoints()
    {
        int x_distance = start.z - end.z;
        int y_distance = start.y - end.y;
        int z_distance = start.x - end.x;
        int i =0;
        int totalPathPoints = Mathf.Abs(z_distance) * Mathf.Abs(y_distance) * Mathf.Abs(x_distance);
    
        PathPoint[] pathPoints = new PathPoint[totalPathPoints];

        for (int x = 0; x < Mathf.Abs(x_distance); x++)
        {
            for (int y = 0; y < Mathf.Abs(y_distance); y++)
            {
                for (int z = 0; z < Mathf.Abs(z_distance); z++)
                {
                    int _x = x_distance > 0 ? x : -x;
                    int _y = y_distance > 0 ? y : -y;
                    int _z = z_distance > 0 ? z : -z;

                    _x += start.x;
                    _y += start.y;
                    _z += start.z;

                    pathPoints[i].gridPosition = new Vector3Int(_x, _y, _z);

                    i++;
                }
            }
        }

        return pathPoints;

    }

    public Vector3Int GetPositionInGrid(Vector3 position)
    {

        Vector3Int pos = Vector3Int.FloorToInt(position);

        return pos;

    }

    void OnDrawGizmos()
        {
            int x_distance = start.z - end.z;
            int y_distance = start.y - end.y;
            int z_distance = start.x - end.x;

            Debug.Log("running");

            for (int x = 0; x < Mathf.Abs(x_distance); x++)
            {
                for (int y = 0; y < Mathf.Abs(y_distance); y++)
                {
                    
                        int _x = x_distance > 0 ? x : -x;
                        int _y = y_distance > 0 ? y : -y;
                        int _z = start.z;

                        _x += start.x;
                        _y += start.y;
                       
                        Gizmos.DrawLine(new Vector3(_x,_y,_z),Vector3.forward * z_distance);
                    
                }
            }

        }


}

[Serializable]
public class PathPoint
{
    public Vector3Int gridPosition;
    RaycastHit downRay;

    public bool colided = false;
    public PathPoint(Vector3Int _gridPosition)
    {
        gridPosition = _gridPosition;
    }

}


[CustomEditor(typeof(PathFind))]
public class PathFindGUI : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        PathFind pf = target as PathFind;

        if (pf.pathPoints == null)
        {
            pf.pathPoints = pf.CreatePathPoints();
        }

        
    }
}
