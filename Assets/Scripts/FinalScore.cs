using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FinalScore : MonoBehaviour
{
    public TMP_Text redScoreText;
    public TMP_Text blueScoreText;

    void Start()
    {
        redScoreText.text = GameManager.instance.scoreRed.ToString();
        blueScoreText.text = GameManager.instance.scoreBlue.ToString();
    }
}
