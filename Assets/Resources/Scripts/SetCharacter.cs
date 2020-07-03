using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCharacter : MonoBehaviour
{
    [SerializeField] List<Sprite> mySprites;
    private void Awake()
    {
        string characterToSet = PlayerPrefs.GetString("Character", "JP");
        GameObject spaceShip = GameObject.Find("Player");
        foreach (Sprite characterSprite in mySprites)
        {
            if (characterSprite.name.Contains(characterToSet))
                spaceShip.GetComponent<SpriteRenderer>().sprite = characterSprite;
        }

    }
}
