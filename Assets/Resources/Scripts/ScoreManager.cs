using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    Text scoreText;
    Text highScoreText;
    int score = 0, highScore;

    #region Assign Values to scoreText, highScoreText & highScore
    // Start is called before the first frame update
    void Start()
    {
        
        scoreText = GameObject.Find("Score").GetComponent<Text>();
        highScoreText = GameObject.Find("High Score").GetComponent<Text>();
        highScore = PlayerPrefs.GetInt("High Score", 0);
        


    }
    #endregion

    #region Manage Score & Highscore Display & Values
    // Update is called once per frame
    void Update()
    {
        ManageScore();
    }

    private void ManageScore()
    {
        DisplayScoreAndHighScore();
        UpdateHighScore();
    }

    private void UpdateHighScore()
    {
        if (score > highScore)
        {
            scoreText.color = Color.red;
            highScoreText.color = Color.red;
            highScore = score;
            PlayerPrefs.SetInt("High Score", highScore);
        }
    }

    private void DisplayScoreAndHighScore()
    {
        scoreText.text = score.ToString();
        highScoreText.text = "Best: " + highScore;
    }
    #endregion


    #region Getters for score & high score
    public int getScore()
    {
        return score;
    }
    public int getHighScore()
    {
        return highScore;
    }
    #endregion

    #region Public Methods To Reset & Increase Score
    public void AddScore(int points)
    {
        score += points;
    }
   
    public void ResetScore()
    {
        score = 0;
    }
    #endregion 



}
