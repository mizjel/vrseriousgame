using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameDataLoader : MonoBehaviour {
    public static T LoadGameData<T>(T type, string gameDataFileName)
    {
        string filePath = Path.Combine(Application.streamingAssetsPath + "/", gameDataFileName);
        string dataAsJson;

        if (filePath.Contains("://") || filePath.Contains(":///"))
            {
                WWW reader = new WWW(filePath);
                while (!reader.isDone) { }
                dataAsJson = reader.text;
            }
            else
            {
                dataAsJson = System.IO.File.ReadAllText(filePath);
            }

        return JsonUtility.FromJson<T>(dataAsJson);
    }
}
