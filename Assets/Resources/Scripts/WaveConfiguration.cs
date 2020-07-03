using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Enemy Wave Configuration")]
public class WaveConfiguration : ScriptableObject
{
    #region Variables To Use
    [SerializeField] GameObject enemyPrefab, pathPrefab;
    [SerializeField] float timeBetweenSpawns = .5f;
    [SerializeField] float numberOfEnemies = 5;
    [SerializeField] float enemieMovementSpeed=2f;
    #endregion



    #region Getters For All Variables
    public GameObject GetEnemyPrefab() { 
        return enemyPrefab; 
    }
    public List<Transform> GetWaypoints()
    {
        var waypoints = new List<Transform>() ;
        foreach (Transform child in pathPrefab.transform)
            waypoints.Add(child);

        return waypoints;

    }
    public float getTimeBetweenSpawns()
    {
        return timeBetweenSpawns;
    }
    public float getNumberOfEnemies()
    {
        return numberOfEnemies;
    }
    public float getEnemieMovementSpeed()
    {
        return enemieMovementSpeed;
    }
    #endregion


}
