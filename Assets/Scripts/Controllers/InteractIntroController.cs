using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractIntroController : Interactible {

    public override void Interact()
    {
        IntroRoomController controller = GlobalHelper.GetIntroRoomController();
        controller.vrCameraUI.SetActive(false);
        controller.startButton.SetActive(false);
        controller.StartTextTyperRoutine();
    }

    public override void SetPreviousColor()
    {
        this.SetPreviousColor(this.gameObject);
    }
}
