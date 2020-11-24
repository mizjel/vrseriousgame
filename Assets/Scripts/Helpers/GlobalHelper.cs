using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using System.Reflection;

public static class GlobalHelper
{

    public const string PUZZLE_PICTURE_PLACEHOLDER_PARENT_TAG = "PicturePlaceholderParentTag";
    public const string PUZZLE_PICTURE_PLACEHOLDER_TAG = "PicturePlaceholderTag";

    public const int AMOUNT_OF_PUZZLE_PICTURES_IN_ROOM = 3;
    public const int DEFAULT_FIELD_OF_VIEW = 60;

    public const string GAME_DATA_FILE_NAME = "GameData/PuzzlePictures.json";
    public const string PUZZLE_PICTURE_SPRITE_PATH = "Sprites/PuzzlePicture/";

    public static T GetCopyOf<T>(this Component comp, T other) where T : Component
    {
        Type type = comp.GetType();
        if (type != other.GetType()) return null; // type mis-matchq
        BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Default |
                             BindingFlags.DeclaredOnly;
        PropertyInfo[] pinfos = type.GetProperties(flags);
        foreach (var pinfo in pinfos)
        {
            if (pinfo.CanWrite)
            {
                try
                {
                    if (!pinfo.IsDefined(typeof(ObsoleteAttribute), true))
                    pinfo.SetValue(comp, pinfo.GetValue(other, null), null);
                }
                catch
                {
                } // In case of NotImplementedException being thrown. For some reason specifying that exception didn't seem to catch it, so I didn't catch anything specific.
            }
        }

        FieldInfo[] finfos = type.GetFields(flags);
        foreach (var finfo in finfos)
        {
            finfo.SetValue(comp, finfo.GetValue(other));
        }

        return comp as T;
    }

    public static T AddComponent<T>(this GameObject go, T toAdd) where T : Component
    {
        return go.AddComponent<T>().GetCopyOf(toAdd) as T;
    }

    public static void Shuffle<T>(T[] array)
    {
        var rng = new System.Random();

        int n = array.Length;
        while (n > 1)
        {
            int k = rng.Next(n--);
            T temp = array[n];
            array[n] = array[k];
            array[k] = temp;
        }
    }

    public static void ShuffleList<T>(this IList<T> list)
    {
        var rng = new System.Random();

        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    public static Sprite loadSpriteFromResources(string path, string spriteName)
    {
        return Resources.Load<Sprite>(path + spriteName) as Sprite;
    }

    public static GameController GetGameController()
    {
        return GameObject.FindWithTag("Room").GetComponent<GameController>() as GameController;
    }

    public static AudioController GetAudioController()
    {
        return GetGameController().gameObject.GetComponent<AudioController>() as AudioController;
    }

    public static StageController GetStageController()
    {
        return GetGameController().gameObject.GetComponent<StageController>();
    }

    public static CanvasRoomController GetCanvasRoomController()
    {
        return GetGameController().gameObject.GetComponent<CanvasRoomController>();
    }

    public static InteractionScript GetInteractionScript()
    {
        return GameObject.Find("OVRCameraRig").GetComponent<InteractionScript>();
    }

    public static GameObject GetOVRPlayerController()
    {
        return GameObject.Find("OVRPlayerController").gameObject;
    }

    public static GameObject GetTeleportMarker()
    {
        return GameObject.Find("TeleportMarker").gameObject;
    }

    public static GameObject GetCameraUIViewer()
    {
        return GameObject.Find("CameraUIViewer").gameObject;
    }
    public static GameObject GetVRCameraUI()
    {
        return GameObject.Find("VRCameraUI").gameObject;
    }
    public static IntroRoomController GetIntroRoomController()
    {
        return GameObject.Find("IntroRoom").GetComponent<IntroRoomController>();
    }
}
