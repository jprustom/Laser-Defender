using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerScript : MonoBehaviour
{
    [SerializeField] //drag in inspector
    GameObject gameOver;
    [SerializeField] AudioClip gameOverSound;
     float gameSpeed = 1f;
   
    private void Awake()
    {
        
    }
    private void FixedUpdate()
    {
        Time.timeScale = gameSpeed;
      
    }
    #region Function To Display Game Over
    public void GameOver()
    {
        StartCoroutine(WaitThenLoadGameOver());
  
    }
    IEnumerator WaitThenLoadGameOver()
    {
        yield return new WaitForSeconds(1.2f);
        gameOver.SetActive(true);
        AudioSource.PlayClipAtPoint(gameOverSound, transform.position);
    }
    #endregion

    #region Buttons Functions (Restart, Change Character, Quit)
    public void Restart()
    {
        ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
        scoreManager.ResetScore();
       
        SceneManager.LoadScene(2);
        gameOver.SetActive(false);

    }
    public void Quit()
    {
        Application.Quit();
        
    }
    public void ResetCharacter()
    {
        SceneManager.LoadScene(1);
        gameOver.SetActive(false);
    }
    #endregion


}
