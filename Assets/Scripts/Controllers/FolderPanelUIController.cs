using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FolderPanelUIController : MonoBehaviour {

    List<PuzzlePictureModel> puzzlePictureModelList;
    List<GameObject> bottomPanelChilds;

    public PanelUIModel folderPanelUI;

    void Start()
    {
        this.puzzlePictureModelList = GlobalHelper.GetGameController().GetComponent<PuzzlePictureController>().puzzlePictureModelList;
        this.bottomPanelChilds = new List<GameObject>();

        folderPanelUI = new PanelUIModel();
        folderPanelUI.SetTopPanelText("Boek Puzzelhints");

        foreach (PuzzlePictureModel model in puzzlePictureModelList)
        {
            BottomPanelChildModel child = new BottomPanelChildModel();
            child.SetTagText(model.number.ToString());
            child.SetDescriptionText(model.puzzlePictureData.title);

            bottomPanelChilds.Add(Instantiate(child.getPanelChild()));            
        }

        folderPanelUI.AddBottomPanelChilds(bottomPanelChilds);

        //Destroy firstchild of bottompanel, the others are visible because they are cloned
        Destroy(folderPanelUI.bottomPanel.transform.GetChild(0).transform.gameObject);

        //Set PanelUI inactive on start
        this.SetActive(false);
    }

    public void SetActive(bool active = true)
    { 
        if (active)
        {
            this.folderPanelUI.panelUI.SetActive(true);
        }
        else
        {
            this.folderPanelUI.panelUI.SetActive(false);
        }
    }
}
