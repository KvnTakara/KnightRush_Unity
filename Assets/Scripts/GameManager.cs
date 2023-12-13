using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public string gameState = "Setting";

    public int scoreRed = 0;
    public int scoreBlue = 0;

    public int maxBlockers = 20;
    public int activeRedBlockers = 0;
    public int activeBlueBlockers = 0;

    public static GameManager instance;
    void Start()
    {
        #region Creating Singleton
        if (instance == null)
        {
            instance = this;
        }
        else Destroy(gameObject);
        #endregion
    }

    void Update()
    {
        #region Score SoftCap
        if (scoreRed < 0) scoreRed = 0;
        if (scoreBlue < 0) scoreBlue = 0;
        #endregion
    }
}
