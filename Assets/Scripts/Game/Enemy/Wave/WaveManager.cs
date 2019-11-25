using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    //one instance of the wavemanager object, singleton pattern
    public static WaveManager Instance;

    //will store the list of waves of enemies
    public List<EnemyWave> enemyWaves = new List<EnemyWave>();

    //time passed since the level started in seconds
    private float elapsedTime = 0f;

    //The wave that’s currently spawning enemies
    private EnemyWave activeWave;

    //keeps track of when the last spawn happened
    //reset every time new spawn of enemy wave
    //incremented every frame
    private float spawnCounter = 0f;

    //A list of waves that were already started. 
    //Any waves in here won’t start again
    private List<EnemyWave> activatedWaves = new List<EnemyWave>();

    //Sets the Instance variable to script itself. Singleton pattern;
    void Awake()
    {
        Instance = this;
    }

    //Add to elapsedTime. 
    //Check if a new wave has to be started
    //update the wave that’s active every frame.
    void Update()
    {
        elapsedTime += Time.deltaTime;
        SearchForWave();
        UpdateActiveWave();
    }
    private void SearchForWave()
    {
        //Iterate over the enemyWaves list.
        foreach (EnemyWave enemyWave in enemyWaves)
        {
            //Check that a wave wasn’t already started before and that
            //the time spent in the level is past the start time of that wave.
            if (!activatedWaves.Contains(enemyWave)
                    && enemyWave.startSpawnTimeInSeconds <= elapsedTime)
            {
                //make the enemy wave the active one
                //Add it to the list of already activated waves 
                //and reset the spawn counter
                activeWave = enemyWave;
                activatedWaves.Add(enemyWave);
                spawnCounter = 0f;

                //Break out of the list iteration as a suitable wave has been found
                break;
            }
        }
    }

    //this code is responsible for the actual spawning of enemies.
    private void UpdateActiveWave()
    {
        //Only continue if there’s an active wave.
        if (activeWave != null)
        {
            spawnCounter += Time.deltaTime;

            //If the spawn counter is higher than the active wave’s timeBetweenSpawnsInSeconds
            //variable, there’s an enemy that needs to spawn.Also reset the spawn counter
            if (spawnCounter >= activeWave.timeBetweenSpawnsInSeconds)
            {
                spawnCounter = 0f;

                //Check if there’s still enemies in the current wave.
                if (activeWave.listOfEnemies.Count != 0)
                {
                    //Spawn the first entry in the enemy list at the position 
                    //of the first waypoint of the wave’s path.
                    GameObject enemy = (GameObject)Instantiate(
                    activeWave.listOfEnemies[0], WayPointManager.Instance.
                    GetSpawnPosition(activeWave.pathIndex), Quaternion.identity);

                    //Set the new enemy’s pathIndex so it knows where it should move
                    enemy.GetComponent<Enemy>().pathIndex = activeWave.pathIndex;

                    //Remove the first entry in the list of enemies
                    activeWave.listOfEnemies.RemoveAt(0);
                }
                else
                {
                    //If the check above fails, there are no more enemies in the current wave. 
                    //Empty the activeWave variable by giving it a null value
                    activeWave = null;

                    //If the number of total waves equals the number if waves that were 
                    //already activated that means all waves are over
                    if (activatedWaves.Count == enemyWaves.Count)
                    {
                        // All waves are over
                        StopSpawning();
                    }
                }
            }
        }
    }

    public void StopSpawning()
    {
        elapsedTime = 0;
        spawnCounter = 0;
        activeWave = null;
        activatedWaves.Clear();
        enabled = false;
    }
}
