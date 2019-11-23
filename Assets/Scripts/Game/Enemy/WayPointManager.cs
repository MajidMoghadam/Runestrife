using UnityEngine;
using System.Collections.Generic;
public class WayPointManager : MonoBehaviour
{
    //Static values can be accessed from all classes without the 
    //need for linking components in the editor or by way of scripting.
    public static WayPointManager Instance;

    //The list of stored paths. Essentially, it’s a list of lists: 
    //Several paths that each contain several waypoints. 
    //See the Path class below
    public List<Path> Paths = new List<Path>();

    void Awake()
    {
        //This is a simplified version of the singleton pattern, 
        //which allows easy access to a manager-type class from anywhere.
        //The keyword this refers to the class it’s written in; 
        //in this case, WayPointManager
        Instance = this;
    }
    //this method returns the first waypoint of the specified path
    //this is the beginning point of the path that will be traversed
    public Vector3 GetSpawnPosition(int pathIndex)
    {
        return Paths[pathIndex].WayPoints[0].position;
    }
}

//a class that stores a list of waypoints
//each object of this class will hold one path
//and can be serialized (can edit in the inspector)
[System.Serializable]
public class Path
{
    //a list of transforms (3D points), this this list
    //stores a pathway for the enemies to traverse
    public List<Transform> WayPoints = new List<Transform>();
}