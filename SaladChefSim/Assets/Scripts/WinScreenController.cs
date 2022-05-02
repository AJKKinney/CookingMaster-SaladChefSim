using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


//When you have time break this class into single responsibilities - 5/2/2022 AJK

// Call declare winner to create win screen ui and update highscore data
public class WinScreenController : MonoBehaviour
{

    [Header("Win Screen")]
    public GameObject playerWinScreen;
    public GameObject newHighScoreText;
    public Image bannerImage;
    public TextMeshProUGUI winnerText;
    public TextMeshProUGUI subText;
    public TextMeshProUGUI secondHighscoreText;
    public TextMeshProUGUI scores;
    public TextMeshProUGUI names;
    public TextMeshProUGUI highscoreOne;
    public TextMeshProUGUI highscoreTwo;

    [Header("Player Colors")]
    public Color red = new Color(255, 0, 0);
    public Color blue = new Color(0, 11, 255);
    public Color yellow = new Color();

    [Header("Win Screen Audio")]
    public AudioClip endGameSound;
    public AudioClip endMusic;

    public void DeclareWinner()
    {
        //Play End Game Sound
        SFXAudioController.instance.PlaySFX(endGameSound);
        //Play Win Screen Music
        MusicController.instance.ChangeSong(endMusic);

        //Check for winner
        //Player One Wins
        if (ScoreTracker.instance.getScore(1) > ScoreTracker.instance.getScore(2))
        {
            Debug.Log("Player One Wins");

            //Set-up Win Screen
            foreach (TextMeshProUGUI text in playerWinScreen.GetComponentsInChildren<TextMeshProUGUI>())
            {
                text.color = red;
            }

            bannerImage.color = red;

            //Set Winner Message
            winnerText.text = "PLAYER ONE";
            subText.text = "IS THE WINNER!";

            //Check for highscores
            if (HighscoreManager.instance.CheckNewHighscore(ScoreTracker.instance.getScore(1), CharacterSelectionController.playerOneInitials))
            {
                //update highscore UI
                foreach (TextMeshProUGUI text in newHighScoreText.GetComponentsInChildren<TextMeshProUGUI>())
                {
                    text.color = red;
                }

                //new Highscore HUD
                newHighScoreText.SetActive(true);
                highscoreOne.text = CharacterSelectionController.playerOneInitials;

                if (HighscoreManager.instance.CheckNewHighscore(ScoreTracker.instance.getScore(2), CharacterSelectionController.playerTwoInitials))
                {
                    secondHighscoreText.gameObject.SetActive(true);
                    secondHighscoreText.color = blue;
                    highscoreTwo.text = CharacterSelectionController.playerTwoInitials;
                }
            }
            playerWinScreen.SetActive(true);
        }
        //Player Two Wins
        else if (ScoreTracker.instance.getScore(1) < ScoreTracker.instance.getScore(2))
        {
            Debug.Log("Player Two Wins");

            //Set-up Win Screen

            foreach (TextMeshProUGUI text in playerWinScreen.GetComponentsInChildren<TextMeshProUGUI>())
            {
                text.color = blue;
            }

            bannerImage.color = blue;

            //winner message
            winnerText.text = "PLAYER TWO";
            subText.text = "IS THE WINNER!";

            //Check for highscores
            //Use reverse order for player 2 winner
            if (HighscoreManager.instance.CheckNewHighscore(ScoreTracker.instance.getScore(2), CharacterSelectionController.playerTwoInitials))
            {

                //update highscore UI
                foreach (TextMeshProUGUI text in newHighScoreText.GetComponentsInChildren<TextMeshProUGUI>())
                {
                    text.color = blue;
                }

                //new Highscore HUD
                newHighScoreText.SetActive(true);
                highscoreTwo.text = CharacterSelectionController.playerTwoInitials;


                if (HighscoreManager.instance.CheckNewHighscore(ScoreTracker.instance.getScore(1), CharacterSelectionController.playerOneInitials))
                {
                    secondHighscoreText.gameObject.SetActive(true);
                    secondHighscoreText.color = red;
                    highscoreOne.text = CharacterSelectionController.playerOneInitials;
                }
            }

            playerWinScreen.SetActive(true);
        }
        //players tied
        else
        {
            Debug.Log("The Players Tied");


            //Set-up Tie Screen

            foreach (TextMeshProUGUI text in playerWinScreen.GetComponentsInChildren<TextMeshProUGUI>())
            {
                text.color = yellow;
            }

            bannerImage.color = yellow;

            //draw message
            winnerText.text = "DRAW";
            subText.text = "YOU ARE BOTH WINNERS!";

            //Check for highscores
            //1st player checks first on ties
            if (HighscoreManager.instance.CheckNewHighscore(ScoreTracker.instance.getScore(1), CharacterSelectionController.playerOneInitials))
            {
                //update highscore UI
                foreach (TextMeshProUGUI text in newHighScoreText.GetComponentsInChildren<TextMeshProUGUI>())
                {
                    text.color = red;
                }
                //new Highscore HUD
                newHighScoreText.gameObject.SetActive(true);
                highscoreOne.text = CharacterSelectionController.playerOneInitials;


                if (HighscoreManager.instance.CheckNewHighscore(ScoreTracker.instance.getScore(2), CharacterSelectionController.playerTwoInitials))
                {
                    secondHighscoreText.gameObject.SetActive(true);
                    secondHighscoreText.color = blue;
                    highscoreTwo.text = CharacterSelectionController.playerTwoInitials;
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
        string[] hNames = HighscoreManager.instance.names;


        string newScores = "";
        string newNames = "";

        //write scores
        for (int i = 0; i < hScores.Length; i++)
        {
            newScores += hScores[i].ToString() + "\n";
        }

        //write names
        for (int i = 0; i < hNames.Length; i++)
        {
            if (hNames[i] != null)
            {
                newNames += hNames[i].ToString() + "\n";
            }
        }

        scores.text = newScores;
        names.text = newNames;
    }

}
