using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    #region Variables To Use
    WaveConfiguration waveConfig; //Will be set in enemy spawner script
    List<Transform> wayPointsTransformList;
    int wayPointIndex = 0;
    #endregion


    void Start()
    {
        #region Get Waypoints Positions From waveConfig set in EnemySpawner.cs then set enemy position to first index
        wayPointsTransformList = waveConfig.GetWaypoints();
        transform.position = wayPointsTransformList[wayPointIndex].position;
        #endregion

    }

    // Update is called once per frame
    void Update()
    {
        MoveEnemie();
    }

    #region MoveEnemie() Function
    private void MoveEnemie()
    {
        #region Move Enemie Through All Paths
        if (wayPointIndex < wayPointsTransformList.Count )
        {
            var targetPosition = wayPointsTransformList[wayPointIndex].position;
            var movementSpeedThisFrame = waveConfig.getEnemieMovementSpeed() * Time.deltaTime; //to make movement independent frame
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementSpeedThisFrame);
            //When enemie arrived at a certain waypoint, move to the next one
            if (transform.position == targetPosition)
                wayPointIndex++;

        }
        #endregion

        #region When done, destroy gameObject
        else
            Destroy(gameObject);
        #endregion

    }
    #endregion

    #region SetWaveConfig Function that will be used in EnemySpawner.cs
    public void SetWaveConfig(WaveConfiguration waveConfig)
    {
        this.waveConfig = waveConfig;
    }
    #endregion

}
