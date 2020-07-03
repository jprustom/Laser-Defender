using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] int healthBonus = 15;
    [SerializeField] AudioClip collectableSoundEffect;
    Player playerScript;
    [SerializeField]float collectableTime = 10f;
    

    
    
    
 
    private void Awake()
    {
        playerScript = FindObjectOfType<Player>();
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            AudioSource.PlayClipAtPoint(collectableSoundEffect, Camera.main.transform.position);



            if (gameObject.name.Contains("Health"))

                playerScript.setHealth(playerScript.GetHealth() + healthBonus);


            else if (gameObject.name.Contains("Laser"))
                playerScript.LaserPowerUp(collectableTime);

            else if (gameObject.name.Contains("Multi-Fire"))
            {
                playerScript.setIsMultiShot(true, collectableTime);

            }
            else if (gameObject.name.Contains("Berserk"))
                playerScript.StartBerserkMode(collectableTime);
            
        }

    }
}
