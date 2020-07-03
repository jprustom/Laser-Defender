using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    GameObject healthCollectablePrefab, laserCollectablePrefab, multiShotCollectablePrefab, berserkCollectablePrefab;

    private void Awake()
    {
        healthCollectablePrefab = Resources.Load("Prefabs/Collectables/Health") as GameObject;
        laserCollectablePrefab = Resources.Load("Prefabs/Collectables/Laser") as GameObject;
        multiShotCollectablePrefab = Resources.Load("Prefabs/Collectables/Multi-Fire") as GameObject;
        berserkCollectablePrefab = Resources.Load("Prefabs/Collectables/Berserk") as GameObject;
    }

    public void SpawnRandomCollectable(Transform t)
    {
        float random = Random.value;
        if (random <= 0.4)
        {

            float randomValue = Random.value;
            if (randomValue <= 0.05f)
            {
                Instantiate(berserkCollectablePrefab, t.position, Quaternion.identity);
            }
            else if (randomValue <= 0.15f)
            {

                Instantiate(laserCollectablePrefab, t.position, Quaternion.identity);
            }
            else if (randomValue <= 0.3f)
            {
                Instantiate(multiShotCollectablePrefab, t.position, Quaternion.identity);
            }
            else
            {
                Instantiate(healthCollectablePrefab, t.position, Quaternion.identity);
            }
        }

    }
}
