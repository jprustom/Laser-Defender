using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    #region Manage Background Music
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SetupSingleton();
    }
    void SetupSingleton()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
            Destroy(gameObject);
    }
    #endregion


}
