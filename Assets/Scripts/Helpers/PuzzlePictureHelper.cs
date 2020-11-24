using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Runtime.Serialization;

public static class PuzzlePictureHelper {

    private static PuzzlePictureData.PuzzlePictureDataCollection puzzlePictureDataCollection;
    private static List<PuzzlePictureData> loadedPuzzlePictureData; //Opgehaalde JSON data
    private static GameObject[] puzzlePicturePlaceholderParents;

    public static List<PuzzlePictureModel> loadPuzzlePictureData(out List<PuzzlePictureModel> puzzlePictureModelList)
    {
        List<PuzzlePictureModel.PuzzlePictureColorEnum> rememberColorList = new List<PuzzlePictureModel.PuzzlePictureColorEnum>();

        //Initialize puzzlePictureDataList. This is gonna be returned
        puzzlePictureModelList = new List<PuzzlePictureModel>();

        //Load PuzzlePictureData from JSON
        loadedPuzzlePictureData = GameDataLoader.LoadGameData(puzzlePictureDataCollection, GlobalHelper.GAME_DATA_FILE_NAME).pictures;

        if (loadedPuzzlePictureData == null)
        {
            throw new Exception("Pictures object is empty");
        }

        //Shuffle puzzlePictureData so it the order is random.
        GlobalHelper.ShuffleList(loadedPuzzlePictureData);

        //Set amount of pictures in the room
        int amount = GlobalHelper.AMOUNT_OF_PUZZLE_PICTURES_IN_ROOM;

        if (amount > loadedPuzzlePictureData.Count)
        {
            amount = loadedPuzzlePictureData.Count;
        }

        //Take (amount) of items from list
        loadedPuzzlePictureData = loadedPuzzlePictureData.Take(amount).ToList();

        int[] numberArr = new int[10];

        for (int i = 0; i < 10; i++)
        {
            numberArr[i] = i;
        }

        GlobalHelper.Shuffle(numberArr);

        foreach (PuzzlePictureData puzzlePictureData in loadedPuzzlePictureData)
        {
            PuzzlePictureModel puzzlePictureModel = new PuzzlePictureModel();
            puzzlePictureModel.number = numberArr[loadedPuzzlePictureData.IndexOf(puzzlePictureData)];
            puzzlePictureModel.puzzlePictureData = puzzlePictureData;

            //Foreach all enum colors
            foreach (PuzzlePictureModel.PuzzlePictureColorEnum enumvalue in Enum.GetValues(typeof(PuzzlePictureModel.PuzzlePictureColorEnum)))
            {
                //Check if Color is already taken
                if (!rememberColorList.Contains(enumvalue))
                {
                    rememberColorList.Add(enumvalue);
                    puzzlePictureModel.color = enumvalue;
                    break;
                }
            }
            puzzlePictureModelList.Add(puzzlePictureModel);
        }
        return puzzlePictureModelList;
    }

    public static List<GameObject> loadPuzzlePicturePlaceholderParents(int amount, out List<GameObject> picturePlaceholderParentChildsList)
    {
        //Initialize picturePlaceholderParentChildsList. This is gonna be returned
        picturePlaceholderParentChildsList = new List<GameObject>();
        
        //Load PuzzlePicturePlaceholderParents
        puzzlePicturePlaceholderParents = GameObject.FindGameObjectsWithTag(GlobalHelper.PUZZLE_PICTURE_PLACEHOLDER_PARENT_TAG);

        for (int i = 0; i < amount; i++)
        {
            //List for storage childs of the parent
            List<GameObject> childs = new List<GameObject>();

            //Current placeholder
            GameObject currentPuzzlePicturePlaceholderParent = puzzlePicturePlaceholderParents[i];

            //Loop trough the childs of the parent with PuzzlePicturePlaceholderTag
            if (currentPuzzlePicturePlaceholderParent.transform.childCount > 0)
            {
                for (int j = 0; j < currentPuzzlePicturePlaceholderParent.transform.childCount; j++)
                {
                    if (currentPuzzlePicturePlaceholderParent.transform.GetChild(j).gameObject.tag == GlobalHelper.PUZZLE_PICTURE_PLACEHOLDER_TAG)
                    {
                        childs.Add(currentPuzzlePicturePlaceholderParent.transform.GetChild(j).gameObject);
                    }
                }
            }
            else
            {
                break;
            }

            //Shuffle childs list
            GlobalHelper.ShuffleList(childs);

            //Add first placeholder to the list
            picturePlaceholderParentChildsList.Add(childs[0]);
        }
        return picturePlaceholderParentChildsList;
    }


}
