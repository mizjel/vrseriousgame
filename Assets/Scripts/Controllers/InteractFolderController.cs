using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractFolderController : Interactible {

    private FolderPanelUIController folderPanelUIController;

    public InteractFolderController() : base(true)
    {

    }

    public override void Interact()
    {
        folderPanelUIController = GlobalHelper.GetGameController().GetComponent<FolderPanelUIController>();

        if (!this.isHolding)
        {
            this.isHolding = true;
            folderPanelUIController.SetActive(true);
        }
        else
        {
            this.isHolding = false;
            folderPanelUIController.SetActive(false);
        }
    }

    public override void SetPreviousColor()
    {
        SetPreviousColor(this.gameObject);
    }
}
