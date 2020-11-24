using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelUIModel {

    public GameObject panelUI;
    public GameObject topPanel;
    public GameObject bottomPanel;

    public PanelUIModel()
    {
        this.panelUI = GameObject.Find("CameraUIViewer/PanelUI").gameObject;
        this.topPanel = panelUI.gameObject.transform.Find("TopPanel").gameObject;
        this.bottomPanel = panelUI.gameObject.transform.Find("BottomPanel").gameObject;
    }

    public void SetTopPanelText(string text)
    {
        this.topPanel.GetComponentInChildren<Text>().text = text;
    }

    public void AddBottomPanelChilds(List<GameObject> childs)
    {
        foreach(GameObject child in childs)
        {
            child.transform.SetParent(this.bottomPanel.transform, false);
        }
    }
}

public class BottomPanelChildModel
{
    private GameObject panelGameObject;
    private GameObject panelTagGameObject;
    private GameObject panelDescriptionGameObject;

    public BottomPanelChildModel()
    {        
        this.panelGameObject = GameObject.Find("CameraUIViewer/PanelUI/BottomPanel/BottomPanelChild").gameObject;
        this.panelTagGameObject = panelGameObject.transform.Find("TagPanel").gameObject;
        this.panelDescriptionGameObject = panelGameObject.transform.Find("DescriptionPanel").gameObject;
    }

    public GameObject getPanelChild()
    {
        return this.panelGameObject;
    }

    public void SetTagText(string text)
    {
        this.panelTagGameObject.GetComponentInChildren<Text>().text = text;
    }

    public void SetDescriptionText(string text)
    {
        this.panelDescriptionGameObject.GetComponentInChildren<Text>().text = text;
    }
}
