using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CanvasType { Static,Animated }
public class ComputerIconController : Interactible {
    public event EventHandler OnIconClick;
    public CanvasType type;
    public Sprite[] animationSpriteList;
    public GameObject objectToAnimate;
    public GameObject InteractionAnimationObject;
    private int currentSpriteIndex;
    private IEnumerator coroutine;
    public float animationInterval = 5;
    public bool endStage;
    
    private void Icon_Click(EventArgs s)
    {
        if (OnIconClick != null)
            OnIconClick(this, null);
    }
    public override void Interact()
    {
        if (name == "LeftArrowButton")
        {
            ExitCanvasRoom();
            return;
        }
        if(tag == "StageIcon" || tag == "StageAnimation")
        {
            if (type != CanvasType.Animated)
            {
                if (!endStage)
                {
                    EventArgs args = new EventArgs();
                    Icon_Click(args);
                }
                else if (endStage)
                {
                    GameObject.Find("CenterEyeAnchor").GetComponent<Camera>().fieldOfView = GlobalHelper.DEFAULT_FIELD_OF_VIEW;
                    ExitCanvasRoom();
                }
            }
            else
            {
                StartAnimation();
            }
        }
    }
    public void StartAnimation()
    {
        currentSpriteIndex = 0;
     
        foreach(GameObject go in GlobalHelper.GetCanvasRoomController().AnimatedImages)
        {
            ComputerIconController controller = go.GetComponent<ComputerIconController>();
            if(controller.stage == stage)
            {
                go.SetActive(true);
            }
        }

        if(objectToAnimate != null)
        {
            Image animationImage = objectToAnimate.GetComponent<Image>();
            coroutine = AnimationRoutine(animationImage);
            StartCoroutine(coroutine);
        }
    }
    public IEnumerator AnimationRoutine(Image image)
    {
        while (true)
        {
            if(currentSpriteIndex >= animationSpriteList.Length)
            {
                InteractionAnimationObject.GetComponent<ComputerIconController>().enabled = true;
                yield break;
            }
            image.sprite = animationSpriteList[currentSpriteIndex];
            currentSpriteIndex++;
        
            yield return new WaitForSeconds(1);
        }
    }
    public void ExitCanvasRoom()
    {
        GameObject controller = GlobalHelper.GetOVRPlayerController();
        controller.transform.position = GlobalHelper.GetGameController().GetPlayerSpawnTransformPosition();
        controller.transform.rotation = GlobalHelper.GetGameController().GetPlayerSpawnTransformRotation();

        OVRPlayerController script = controller.GetComponent<OVRPlayerController>();
        script.GravityModifier = 1;
    }
    public override void ToggleHighlight()
    {
        Image image = GetComponent<Image>();
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
