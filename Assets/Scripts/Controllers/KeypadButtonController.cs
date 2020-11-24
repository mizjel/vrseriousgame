using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeypadButtonController : Interactible {
    public event System.EventHandler ButtonPressed;

    protected virtual void OnButtonPressed(System.EventArgs e)
    {
        System.EventHandler handler = ButtonPressed;
        if (handler != null)
        {
            handler(this, e);
        }
    }

    public override void Interact()
    {
        //AlarmEventArgs args = new AlarmEventArgs(currentTime);
        //OnAlarmEvent(args);
        
        Debug.Log("knop clicked");
        System.EventArgs args = new System.EventArgs();
        OnButtonPressed(args);

        //throw new System.NotImplementedException();
    }

    public override void SetPreviousColor()
    {
        SetPreviousColor(this.gameObject);
    }
}
