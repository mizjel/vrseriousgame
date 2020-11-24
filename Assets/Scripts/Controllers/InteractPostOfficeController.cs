using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractPostOfficeController : Interactible {

    private bool isInteractedOnce; 
    public GameObject package;

    public void Update()
    {
        if (!GlobalHelper.GetAudioController().SingleAudioClipIsPlaying() && isInteractedOnce)
        {
            package.SetActive(true);
            isInteractedOnce = false;
        }
    }

    public override void Start() 
    {
        base.Start();
        package.SetActive(false);
    }

    public override void Interact()
    {
        this.isInteractedOnce = true;
    }

    public override void SetPreviousColor()
    {
        this.SetPreviousColor(this.gameObject);
    }
}
