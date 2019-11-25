using System;
using System.Collections.Generic;
using UnityEngine;

//This is a simple class that’ll be used as data container 
//to make it easy to add waves in the Unity editor.
//this attribute makes it seen and editable in the editor
[Serializable]
public class EnemyWave
{
    //The index of the path this wave should take.
    public int pathIndex;

    //Time in seconds before this wave starts.
    public float startSpawnTimeInSeconds;

    //Delay in seconds between each spawn.
    public float timeBetweenSpawnsInSeconds = 1f;

    //The list of enemies in this wave.
    public List<GameObject> listOfEnemies = new List<GameObject>();
}