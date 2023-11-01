using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class ScoreManager : MonoBehaviour
{

    public static int score;
    [SerializeField] Text bestScoreText;

    Text text;
    
    void Awake ()
    {
        text = GetComponent <Text> ();
        score = 0;
    }

    void Update ()
    {
        text.text = "Score: " + score;
        
    }
    public void CheckScore()
    {
        //called via animation event 

        var bestScore = Convert.ToInt32(GameSaveLoad.instance.GetValue(GameSaveLoad.instance.SCORE_KEY));
        if (score>bestScore)
        {
            bestScoreText.text = score.ToString();
            GameSaveLoad.instance.SetValue(GameSaveLoad.instance.SCORE_KEY, score.ToString());
            GameSaveLoad.instance.Save();
        }
        else
        {
            bestScoreText.text = bestScore.ToString();
        }
    }

}