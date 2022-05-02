using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Singleton for referencing, loading and saving highscore data.
public class HighscoreManager : MonoBehaviour
{
    public static HighscoreManager instance;



    public int[] highscores = new int[10];
    public string[] names = new string[10];


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        //Load Highscores

        highscores = SaveSystem.LoadData().highscores;
        names = SaveSystem.LoadData().names;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //returns true if the score is in the top 10 scores.
    public bool CheckHighscore(int scoreToCheck, string usrName)
    {
        for(int i = 0; i < highscores.Length; i++)
        {
            if(scoreToCheck > highscores[i])
            {
                TakePlace(scoreToCheck, i, usrName);
                return true;
            }
        }

        return false;
    }

    // puts the score into the correct highscore place and  shifts the lower highscores removing the lowest
    private void TakePlace(int score, int place, string usrName)
    {
        for(int i = highscores.Length - 1; i >= place; i--)
        {
            //shift scores
            if (i != highscores.Length - 1)
            {
                highscores[i + 1] = highscores[i];
                names[i + 1] = names[i];
            }

            //set new score on last
            if(i == place)
            {
                highscores[i] = score;
                names[i] = usrName;
            }
        }

        //Save highscores
        SaveSystem.SaveData(highscores, names);

    }
}
