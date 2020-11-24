using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractPuzzlePictureController : Interactible {

    //The puzzlepicture that is interacted with
    private GameObject currentPuzzlePicturePlaceholder;

    public InteractPuzzlePictureController() : base(true) {
        
    }

    public override void Interact()
    {
        Image imageUIViewer = GameObject.Find("CameraUIViewer/ImageUI").GetComponent<Image>();

        if (!this.isHolding)
        {
            this.isHolding = true;

            currentPuzzlePicturePlaceholder = GetComponent<Transform>().gameObject;
            var spriteName = currentPuzzlePicturePlaceholder.GetComponent<SpriteRenderer>().sprite.name;
            //Set the sprite on the camera
            imageUIViewer.sprite = GlobalHelper.loadSpriteFromResources(GlobalHelper.PUZZLE_PICTURE_SPRITE_PATH, spriteName);
            imageUIViewer.enabled = true;
        }
        else
        {
            this.isHolding = false;
            imageUIViewer.enabled = false;
        }
    }

    public override void SetPreviousColor()
    {
        SetPreviousColor(this.gameObject);
    }
}
