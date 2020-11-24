using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseInteractController : Interactible {

    public override void Interact()
    {
        throw new System.NotImplementedException();
    }

    public override void SetPreviousColor()
    {
        this.SetPreviousColor(this.gameObject);
    }
}
