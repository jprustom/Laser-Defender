using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float health = 300;
    float shotCounter;
    [SerializeField] float minTimeBetweenShots = .2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] float enemyBulletSpeed = 20f;
    GameObject spaceShip;
    [SerializeField] List<GameObject> enemyBulletsListPrefabs; //Populate In Inspector
    GameObject explosionEffectPrefab;
    [SerializeField] AudioClip soundWhenShooting, deathSound;
    ScoreManager scoreManagerScript;
    [SerializeField] int pointsOnDeath;
    Collectables collectablesScript;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        #region Diminish Enemy Health & Change Color To Red When Hit
        if (collision.gameObject.tag == "Player Bullet" || collision.gameObject.tag == "Player Laser" || collision.gameObject.tag == "Berserk")
        {
            DamageDealer damageDealerScript = collision.GetComponent<DamageDealer>();
            health -= damageDealerScript.GetDamage();
            damageDealerScript.Hit();
            StartCoroutine(ChangeColor());
        }
        #endregion
    }
    IEnumerator ChangeColor()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }

    private void Start()
    {
        collectablesScript = FindObjectOfType<Collectables>();
        scoreManagerScript = FindObjectOfType<ScoreManager>();
        explosionEffectPrefab = Resources.Load("VFX/Explosion Particles") as GameObject;
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        spaceShip = GameObject.Find("Player");
    }
    private void Update()
    {
        #region Destroy Object If No More Health
        if (health <= 0)
        {
            Die();
        }
        #endregion

        #region Let Enemy Shoot At SpaceShip If Still Alive
        if (spaceShip)
            CountDownAndFire();
        #endregion


    }

    private void Die()
    {
        collectablesScript.SpawnRandomCollectable(transform);
        scoreManagerScript.AddScore(pointsOnDeath);
        AudioSource.PlayClipAtPoint(deathSound, transform.position);
        var explosionEffect = Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity) as GameObject;
        Destroy(gameObject);
        Destroy(explosionEffect, 1f);
    }

    void CountDownAndFire()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0)
        {
            Fire();
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    void Fire()
    {
        AudioSource.PlayClipAtPoint(soundWhenShooting, transform.position);
        Transform enemyBulletSpawnPositionTransform = gameObject.transform.GetChild(0).transform;
        GameObject bulletToShoot = Instantiate(enemyBulletsListPrefabs[Random.Range(0,enemyBulletsListPrefabs.Count-1)], enemyBulletSpawnPositionTransform.position, Quaternion.identity);
        var target = bulletToShoot.tag== "Enemy Laser"?
            new Vector3(0f,-enemyBulletSpeed,0)
            :(spaceShip.transform.position - transform.position).normalized * (enemyBulletSpeed);
        bulletToShoot.GetComponent<Rigidbody2D>().velocity = target;
    }
}
