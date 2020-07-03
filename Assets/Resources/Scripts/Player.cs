using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Variables To Use
    GameObject laserPrefab;
    GameObject laserBulletSpawnPosition;
    [SerializeField] int health = 500;
    [SerializeField][Range(1f,50f)] float bulletSpeed = 20f;
    [SerializeField][Range(0.15f,1f)] float shootingInterval = .1f;
    [SerializeField] AudioClip fireSound;
    [SerializeField] AudioClip deathSound;
    [SerializeField] GameObject deathExplosionPrefab;
    [SerializeField] GameObject laserPowerUp;
    [SerializeField] List<Transform> multiShotsSpawnPointsTransform;
    [SerializeField] GameObject berserk;
    bool isLaserPowerUp;
    bool isMultiShot;
    float timeForMultiShot = 5f;
   
    bool hasShotOnce;
    float nextShot = 0f;
    #endregion

    #region Player Health Functions
    public int GetHealth()
    {
        return health;
    }
    public void setHealth(int health)
    {
        this.health = health;
    }
    #endregion

    #region Populate laserPrefab & laserBulletSpawnPosition With Appropriate Objects
    // Start is called before the first frame update
    void Start()
    {
        
        
        laserPrefab = (GameObject)(Resources.Load("Prefabs/Bullets/Laser"));
        laserBulletSpawnPosition = GameObject.Find("Bullet/Laser Spawn Position");
        
       
    }
    #endregion

    #region Manage Player Movement,Shooting & Death
    // Update is called once per frame
    void Update()
    {
        ManagePlayer();

    }
    void ManagePlayer() {
        #region Handle Moving With Mouse & Shooting
        MovePosWithMouse();
        if (Input.GetButton("Fire1") && Time.time > nextShot && !laserPowerUp.activeSelf)
            Fire();
        #endregion
        if (health <= 0)
        {
            Die();
            
        }
        
    }
    private void MovePosWithMouse()
    {
        #region Get Mouse Position Relative To World Unit
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        #endregion

        #region Getting X Position with consideration to boundaries
        //I used 0.5 as padding
        float xPadding = 0.5f;
        var xMin = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + xPadding;
        var xMax = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - xPadding;
        var xPos = Mathf.Clamp(mousePosition.x, xMin, xMax);
        #endregion

        #region Getting y Position With Consideration To Boundaries
        //I used 0.8 as padding
        var yPadding = 0.8f;
        var yMin = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + yPadding;
        var yMax = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - yPadding;
        var yPos = Mathf.Clamp(mousePosition.y, yMin, yMax);
        #endregion

        #region Set Position
        Vector3 pos = new Vector3(xPos, yPos, 0);
        gameObject.transform.position = pos;
        #endregion

    }
    private void Fire()
    {
        if (!isMultiShot)
        {
            hasShotOnce = true;
            AudioSource.PlayClipAtPoint(fireSound, transform.position);
            nextShot = Time.time + shootingInterval;
            GameObject laser = Instantiate(laserPrefab, laserBulletSpawnPosition.transform.position, Quaternion.identity) as GameObject;
            laser.transform.parent = laserBulletSpawnPosition.transform;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, bulletSpeed);
        }
        else {
            /*StartCoroutine(ManageMultiFire(timeForMultiShot));*/
            ManageMultiFire(timeForMultiShot);
           
           
        }
    }
    void ManageMultiFire(float time)
    {
        foreach (Transform spawnPoint in multiShotsSpawnPointsTransform)
        {
            GameObject newBullet = GameObject.Instantiate(Resources.Load("Prefabs/Bullets/Player Spinner")) as GameObject;
            newBullet.transform.position = spawnPoint.transform.position;


            Rigidbody2D newBulletRigidBody = newBullet.GetComponent<Rigidbody2D>();

            Vector3 dir = (spawnPoint.transform.position - this.gameObject.transform.position) * bulletSpeed;

            newBulletRigidBody.velocity = new Vector2(dir.x, dir.y);
        }
        
      
    }

    public void setIsMultiShot(bool value,float timeForMultiShot)
    {
        /*isMultiShot = value;
        this.timeForMultiShot = timeForMultiShot;*/
        StartCoroutine(MultiShotCoroutine(value, timeForMultiShot));
        
    }
    IEnumerator MultiShotCoroutine(bool value, float timeForMultiShot) {
        isMultiShot = value;
        this.timeForMultiShot = timeForMultiShot;
        yield return new WaitForSeconds(timeForMultiShot);
        isMultiShot = false;
    }


    #region Laser
    public void LaserPowerUp(float collectableTimeInSeconds)
    {
        StartCoroutine(ManageLaserPowerup(collectableTimeInSeconds));
    }
    IEnumerator ManageLaserPowerup(float collectableTimeInSeconds)
    {
        if (!isLaserPowerUp)
        {
            laserPowerUp.SetActive(true);
            isLaserPowerUp = true;
            yield return new WaitForSeconds(collectableTimeInSeconds);
            laserPowerUp.SetActive(false);
            isLaserPowerUp = false;
        }

    }
    #endregion


    public bool getHasShotOnce()
    {
        return hasShotOnce;
    }
    private void Die()
    {
        AudioSource.PlayClipAtPoint(deathSound, transform.position);
        
        Destroy(gameObject);
        GameObject explosion = Instantiate(deathExplosionPrefab, transform.position, Quaternion.identity) as GameObject;
        Destroy(explosion, 1f);
        FindObjectOfType<ManagerScript>().GameOver();
    }




    #endregion

    #region Berserk

    public void StartBerserkMode(float berserkTime)
    {
        StartCoroutine(Berserk(berserkTime));
       
    }
    IEnumerator Berserk(float berserkTime)
    {
        berserk.SetActive(true);
        yield return new WaitForSeconds(berserkTime);
        berserk.SetActive(false);
    }

    #endregion

    #region When Player Gets Hit
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Enemy Laser")
        {
            Camera.main.GetComponent<CameraShake>().ShakeCamera(.1f, .1f);
            DamageDealer damageDealerScript = collision.GetComponent<DamageDealer>();
            health -= damageDealerScript.GetDamage();
            damageDealerScript.Hit();
            StartCoroutine(ChangeColor());
        }
    }
    IEnumerator ChangeColor()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }
    #endregion

  /*  private void FixedUpdate()
    {
        if (laserPowerUpActive && Time.time > timeForDeactivate)
        {
            print("HEY");
            playerScript.DeactivateLaserPowerUp();
            laserPowerUpActive = false;

        }
    }*/
}
