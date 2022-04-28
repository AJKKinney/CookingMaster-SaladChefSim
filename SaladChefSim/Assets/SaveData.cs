using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int[] highscores;

    public SaveData(int[] newScores)
    {
        highscores = newScores;
    }
}
