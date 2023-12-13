using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    float timeTicker = 0f;
    int counterLeft = 3;
    int timeLeft = 61;

    public GameObject RedScreen;
    public GameObject BlueScreen;
    public GameObject DrawScreen;

    bool screenLocker = false;

    public TMP_Text startCounterText;
    public TMP_Text timerText;

    void Start()
    {
        Time.timeScale = 1;
        timerText.text = null;
    }

    void Update()
    {
        #region Timer
        timeTicker += Time.deltaTime;
        if (timeTicker >= 1f)
        {
            if (counterLeft > 1)
            {
                --counterLeft;
                startCounterText.text = counterLeft.ToString();
            } 
            else
            {
                GameManager.instance.gameState = "Gameplay";
                startCounterText.color = Color.clear;
            }

            if (GameManager.instance.gameState == "Gameplay")
            {
                --timeLeft;
                timerText.text = timeLeft.ToString();
            }
            
            timeTicker = 0f;
        }
        #endregion

        #region EndGame Screen
        if (timeLeft <= 0f && !screenLocker)
        {
            screenLocker = true;

            if (GameManager.instance.scoreRed > GameManager.instance.scoreBlue)
            {
                GameObject screen = Instantiate(RedScreen, transform.position, Quaternion.identity);
            }
            else if (GameManager.instance.scoreBlue > GameManager.instance.scoreRed)
            {
                GameObject screen = Instantiate(BlueScreen, transform.position, Quaternion.identity);
            } else
            {
                GameObject screen = Instantiate(DrawScreen, transform.position, Quaternion.identity);
            }

            GameManager.instance.gameState = "EndGame";
            Time.timeScale = 0;
        }
        #endregion
    }
}
