using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfiguration> waveConfigList; //Set In Inspector
    [SerializeField] bool loopingWaves = true;
    int startingWave = 0;
    Player playerScript;
    [SerializeField] GameObject tapToShoot;//In Inspector
    
    

    // Start is called before the first frame update
    void Awake()
    {
        playerScript = FindObjectOfType<Player>();
        #region Get Current Wave Which is the first wave in the waveConfigList List
        var currentWave = waveConfigList[startingWave];
        #endregion
    }
    IEnumerator Start()
    {
        #region Loop Through All Waves, unless loopingWaves is set to false
        do
        { 
            yield return StartCoroutine(SpawnAllWaves()); }
        while (loopingWaves);
        #endregion

    }
    #region Coroutine SpawnAllWaves()
    private IEnumerator SpawnAllWaves()
    {
        if (playerScript.getHasShotOnce())
        {
            tapToShoot.SetActive(false);
            yield return new WaitForSeconds(2f);
            for (int waveIndex = startingWave; waveIndex < waveConfigList.Count; waveIndex++)
            {
                var currentWave = waveConfigList[waveIndex];
                yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
                //I added the statement below to wait 2 to 5 seconds between each wave
                yield return new WaitForSeconds(Random.Range(2f, 5f));

            }
        }
    }
    #endregion

    #region Coroutine SpawnAllEnemiesInWave() which will take care of the enemies spawning in each wave
    private IEnumerator SpawnAllEnemiesInWave(WaveConfiguration waveConfig)
    {
        for (int enemyCounter = 0; enemyCounter < waveConfig.getNumberOfEnemies(); enemyCounter++) {
            GameObject SpawnedEnemy = Instantiate(waveConfig.GetEnemyPrefab() as GameObject, waveConfig.GetWaypoints()[0].position, Quaternion.identity);
            
            SpawnedEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);
            yield return new WaitForSeconds(waveConfig.getTimeBetweenSpawns());
        }
       
    }
    #endregion

}
