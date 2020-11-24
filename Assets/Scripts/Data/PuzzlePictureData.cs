using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization;


[System.Serializable]
public class PuzzlePictureData {
    public string title;
    public string sprite;

    [System.Serializable]
    public class PuzzlePictureDataCollection
    {
        public List<PuzzlePictureData> pictures;
    }
}
