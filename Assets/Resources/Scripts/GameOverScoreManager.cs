using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScoreManager : MonoBehaviour
{
    #region Display Score When Player Dies
    [SerializeField] Text scoreText, highScoreText;
    // Start is called before the first frame update
    void Start()
    {
        
        ScoreManager scoreManagerScript = FindObjectOfType<ScoreManager>();
        scoreText.text = scoreManagerScript.getScore().ToString();
        highScoreText.text = scoreManagerScript.getHighScore().ToString();
       

    }
    #endregion


}
