using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] int damage = 100;
    [SerializeField] AudioClip negativeSound;
    public int GetDamage() { 
        return damage;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.tag == "Berserk" && collision.gameObject.tag!="Player Laser" && collision.gameObject.tag != "Player Bullet"&&collision.gameObject.tag != "Powerup")
            Destroy(collision.gameObject);
    }

    public void Hit()
    {
        if (negativeSound)
         AudioSource.PlayClipAtPoint(negativeSound, transform.position);
        //Don't destroy laser powerup when hitting enemies
        if (gameObject.tag!="Player Laser" && gameObject.tag!="Berserk")
            Destroy(gameObject);
    }
}
