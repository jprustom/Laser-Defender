using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseCharacter : MonoBehaviour
{
    
    public void ChooseSpaceShip(string characterName)
    {
        PlayerPrefs.SetString("Character",characterName);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
