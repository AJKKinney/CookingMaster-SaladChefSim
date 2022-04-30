using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int[] highscores;
    public string[] names;

    public SaveData(int[] newScores, string[] newNames)
    {
        highscores = newScores;
        names = newNames;
    }
}
