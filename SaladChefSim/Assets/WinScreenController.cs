using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


// Call declare winner to create win screen ui and update highscore data
public class WinScreenController : MonoBehaviour
{

    [Header("Win Screen")]
    public GameObject playerWinScreen;
    public TextMeshProUGUI winnerText;
    public Image bannerImage;
    public GameObject newHighScoreText;
    public TextMeshProUGUI secondHighscoreText;
    public TextMeshProUGUI scores;
    public TextMeshProUGUI names;

    public Color red = new Color(255, 0, 0);
    public Color blue = new Color(0, 11, 255);
    public Color yellow = new Color();

    public void DeclareWinner()
    {
        if (ScoreTracker.instance.getScore(1) > ScoreTracker.instance.getScore(2))
        {
            Debug.Log("Player One Wins");

            //Set HUD

            foreach (TextMeshProUGUI text in playerWinScreen.GetComponentsInChildren<TextMeshProUGUI>())
            {
                text.color = red;
            }

            bannerImage.color = red;

            winnerText.text = "PLAYER ONE";

            //Check for highscores
            if (HighscoreManager.instance.CheckHighscore(ScoreTracker.instance.getScore(1)))
            {

                //new Highscore HUD
                newHighScoreText.SetActive(true);


                if (HighscoreManager.instance.CheckHighscore(ScoreTracker.instance.getScore(2)))
                {
                    secondHighscoreText.gameObject.SetActive(true);
                    secondHighscoreText.color = blue;
                }
            }
            playerWinScreen.SetActive(true);
        }
        else if (ScoreTracker.instance.getScore(1) < ScoreTracker.instance.getScore(2))
        {
            Debug.Log("Player Two Wins");

            //Set HUD

            foreach (TextMeshProUGUI text in playerWinScreen.GetComponentsInChildren<TextMeshProUGUI>())
            {
                text.color = blue;
            }

            bannerImage.color = blue;

            winnerText.text = "PLAYER TWO";

            //Check for highscores
            //Use reverse order for player 2 winner
            if (HighscoreManager.instance.CheckHighscore(ScoreTracker.instance.getScore(2)))
            {

                //new Highscore HUD
                newHighScoreText.SetActive(true);


                if (HighscoreManager.instance.CheckHighscore(ScoreTracker.instance.getScore(1)))
                {
                    secondHighscoreText.gameObject.SetActive(true);
                    secondHighscoreText.color = red;
                }
            }

            playerWinScreen.SetActive(true);
        }
        else
        {
            Debug.Log("The Players Tied");


            //Set HUD

            foreach (TextMeshProUGUI text in playerWinScreen.GetComponentsInChildren<TextMeshProUGUI>())
            {
                text.color = yellow;
            }

            bannerImage.color = yellow;

            winnerText.text = "DRAW";

            //Check for highscores
            //1st player checks first on ties
            if (HighscoreManager.instance.CheckHighscore(ScoreTracker.instance.getScore(1)))
            {

                //new Highscore HUD
                newHighScoreText.gameObject.SetActive(true);


                if (HighscoreManager.instance.CheckHighscore(ScoreTracker.instance.getScore(2)))
                {
                    secondHighscoreText.gameObject.SetActive(true);
                    secondHighscoreText.color = blue;
                }

            } 
            playerWinScreen.SetActive(true);
        }

        //display highscores
        UpdateHighscoresUI();
    }


    //updates highscore values
    private void UpdateHighscoresUI()
    {
        int[] hScores = HighscoreManager.instance.highscores;

        string newScores = "";

        for(int i = 0; i < hScores.Length; i++)
        {
            newScores += hScores[i].ToString() + "\n";
        }

        scores.text = newScores;
    }

}
