using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasRoomObjectController : Interactible {

    private GameController gameController;
    private CanvasRoomController canvasRoomController;
    
    public delegate void CanvasRoomEventHandler(object sender, CanvasRoomEventArgs e);
    public event CanvasRoomEventHandler RoomCanvasPressed;
    
    protected virtual void OnRoomCanvasPressed(CanvasRoomEventArgs e)
    {
        CanvasRoomEventHandler handler = RoomCanvasPressed;
        if (handler != null)
        {
            handler(this, e);
        }
    }
    // Update is called once per frame
    void Update () {
		
	}

    public override void Interact()
    {
        gameController = GlobalHelper.GetGameController();
        canvasRoomController = gameController.GetComponent<CanvasRoomController>();
        CanvasRoomEventArgs args = new CanvasRoomEventArgs();
        OnRoomCanvasPressed(args);

        GameObject.Find("CenterEyeAnchor").GetComponent<Camera>().fieldOfView = 24;
        GlobalHelper.GetOVRPlayerController().GetComponent<OVRPlayerController>().GravityModifier = 0;
        GlobalHelper.GetOVRPlayerController().transform.position = GameObject.FindGameObjectWithTag("CanvasRoomSpawnPoint").transform.position;
        GlobalHelper.GetOVRPlayerController().transform.rotation = GameObject.FindGameObjectWithTag("CanvasRoomSpawnPoint").transform.rotation;
    }
    public override void ToggleHighlight()
    {
        Image image = GetComponentInChildren<Image>();
        if (isHighlighted)
        {
            image.color = PreviousColor;
            isHighlighted = false;
        }
        else if (!isHighlighted)
        {
            image.color = Color.green;
            isHighlighted = true;
        }
    }
    public override void SetPreviousColor()
    {
        SetPreviousColor(this.gameObject);
    }
}
