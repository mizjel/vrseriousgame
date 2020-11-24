using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class PuzzlePictureModel {
    public int number;
    public PuzzlePictureData puzzlePictureData;
    public PuzzlePictureColorEnum color;
    public GameObject puzzlePicturePlaceholder;

    public enum PuzzlePictureColorEnum
    {
        [EnumMember(Value = "#ffff00")]
        Yellow,
        [EnumMember(Value = "#008000")]
        Green,
        [EnumMember(Value = "#0000ff")]
        Blue
    }
}
