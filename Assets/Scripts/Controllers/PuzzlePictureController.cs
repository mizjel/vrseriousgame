using System.Collections.Generic;
using UnityEngine;

public class PuzzlePictureController : MonoBehaviour {

    public List<PuzzlePictureModel> puzzlePictureModelList = new List<PuzzlePictureModel>();

    // Use this for initialization
    void Start()
    {
        this.LoadPuzzlePictures();
    }

    public void LoadPuzzlePictures()
    {
        //Set all sprites to null
        GameObject[] placeholders = GameObject.FindGameObjectsWithTag(GlobalHelper.PUZZLE_PICTURE_PLACEHOLDER_TAG);
        foreach (GameObject placeholder in placeholders)
        {
            placeholder.GetComponent<SpriteRenderer>().sprite = null;
        }

        PuzzlePictureHelper.loadPuzzlePictureData(out puzzlePictureModelList);

        List<GameObject> puzzlePicturePlaceholdersList;
        PuzzlePictureHelper.loadPuzzlePicturePlaceholderParents(puzzlePictureModelList.Count, out puzzlePicturePlaceholdersList);

        //Save the puzzelpictures in a list
        for (int i = 0; i < puzzlePictureModelList.Count; i++)
        {
            //Create model
            PuzzlePictureModel model = new PuzzlePictureModel();
            model.color = puzzlePictureModelList[i].color;
            model.puzzlePictureData = puzzlePictureModelList[i].puzzlePictureData;
            model.puzzlePicturePlaceholder = puzzlePicturePlaceholdersList[i];

            model.puzzlePicturePlaceholder.GetComponent<SpriteRenderer>().sprite = GlobalHelper.loadSpriteFromResources(GlobalHelper.PUZZLE_PICTURE_SPRITE_PATH, model.puzzlePictureData.sprite);
            model.puzzlePicturePlaceholder.AddComponent<BoxCollider>();
            model.puzzlePicturePlaceholder.GetComponent<BoxCollider>().size = model.puzzlePicturePlaceholder.GetComponent<SpriteRenderer>().size;
            model.puzzlePicturePlaceholder.AddComponent<InteractPuzzlePictureController>();
            model.puzzlePicturePlaceholder.GetComponent<InteractPuzzlePictureController>().isPartOfStage = true;

            Color newCol;
            ColorUtility.TryParseHtmlString(model.color.ToString(), out newCol);
            model.puzzlePicturePlaceholder.GetComponent<Renderer>().material.color = newCol;
        }
    }
}
