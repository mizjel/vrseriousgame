using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasRoomController : MonoBehaviour {

    private StageController stageController;
    private GameObject[] stageCanvases;
    [HideInInspector]
    public List<GameObject> AnimatedImages;
    
    // Use this for initialization
    void Start () {
        AnimatedImages = new List<GameObject>();
        stageController = GlobalHelper.GetStageController();
        stageCanvases = GameObject.FindGameObjectsWithTag("StageCanvas");
        SubscribeToIconEvents();
        EnableCanvas(stageController.CurrentStage);
    }

    /// <summary>
    /// Enable canvas with the name of the stage integer given, canvases have to be named according to the stage they are used in, example: Stage0
    /// </summary>
    /// <param name="stage">Number of stage</param>
    void EnableCanvas(int stage)
    {
        foreach(GameObject canvas in stageCanvases)
        {
            if(canvas.name == "Stage" + stage)
            {
                canvas.SetActive(true);
            }
            else
            {
                canvas.SetActive(false);
            }
        }
    }
    public void EnableCanvasFromInteractionObject(object sender, EventArgs e)
    {
        GameObject interactionObject = GameObject.FindGameObjectWithTag("CanvasRoomInteractionObject");
        EnableCanvas(stageController.CurrentStage);
    }
    /// <summary>
    /// Subscribes to all button events with the tag StageIcon for static icon and StageAnimation for animated icons
    /// </summary>
    void SubscribeToIconEvents()
    {
        GameObject[] staticComputerIcons = GameObject.FindGameObjectsWithTag("StageIcon");
        foreach(GameObject gameObject in staticComputerIcons)
        {
            ComputerIconController controller = gameObject.GetComponent<ComputerIconController>();
            controller.OnIconClick += HandleIconClick;
        }
        GameObject[] animationComputerIcons = GameObject.FindGameObjectsWithTag("StageAnimation");
        foreach(GameObject gameObject in animationComputerIcons)
        {
            ComputerIconController controller = gameObject.GetComponent<ComputerIconController>();
            controller.OnIconClick += HandleIconClick;
            controller.enabled = false;
            AnimatedImages.Add(gameObject);
            gameObject.SetActive(false);
        }
        GameObject[] canvasInteractionObject = GameObject.FindGameObjectsWithTag("CanvasRoomInteractionObject");
        foreach (GameObject gameObject in canvasInteractionObject)
        {
            CanvasRoomObjectController controller = gameObject.GetComponent<CanvasRoomObjectController>();
            controller.RoomCanvasPressed += EnableCanvasFromInteractionObject;
        }
    }
    /// <summary>
    /// Fires when user clicks on an icon, sets the next stage if the icon doesn't have to play a animation
    /// </summary>
    /// <param name="sender">object that was clicked on</param>
    /// <param name="e">event arguments</param>
    public void HandleIconClick(object sender, EventArgs e)
    {
        ComputerIconController controller = (ComputerIconController)sender;
        if (controller.type != CanvasType.Animated)
        {
            EnableCanvas(stageController.CurrentStage);
        }
    }
}
