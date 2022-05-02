using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{

    public static void SaveData(int[] highscores, string[] names)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/savedata.sss";

        FileStream stream = new FileStream(path, FileMode.Create);

        SaveData data = new SaveData(highscores, names);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static SaveData LoadData()
    {
        string path = "";

        if(CheckForSave(out path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);


            SaveData data = formatter.Deserialize(stream) as SaveData;

            stream.Close();

            return data;
        }
        else
        {
            CreateNewSave();
            Debug.Log("Created new save data in " + path);
            //recursively grab new save
            return LoadData();
        }
    }

    //Creates a new data file
    public static void CreateNewSave()
    {
        DeleteSave();
        SaveData(new int[10], new string[10]);
    }

    //Deletes save data file
    public static void DeleteSave()
    {

        string path = "";

        if (CheckForSave(out path))
        {
            File.Delete(path);
        }
    }

    //checks to see if a save file exists passes out path with overload
    public static bool CheckForSave()
    {
        string path = Application.persistentDataPath + "/savedata.sss";

        if (File.Exists(path))
        {
            return true;
        }

        else
        {
            Debug.LogWarning("Save data could not be found in " + path);
            return false;
        }
    }

    public static bool CheckForSave(out string path)
    {
        path = Application.persistentDataPath + "/savedata.sss";

        if (File.Exists(path))
        {
            return true;
        }

        else
        {
            Debug.LogWarning("Save data could not be found in " + path);
            return false;
        }
    }
}
