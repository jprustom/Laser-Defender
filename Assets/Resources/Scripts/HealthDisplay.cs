using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    Text healthText;
    
    Player playerScript;

    // Start is called before the first frame update
    void Start()
    {
        healthText = GameObject.Find("Health Text").GetComponent<Text>();
        playerScript = FindObjectOfType<Player>();
       
        
    }
  
    // Update is called once per frame
    void Update()
    {
        healthText.text = Mathf.Clamp(playerScript.GetHealth(),0,100000).ToString();
    }
}
