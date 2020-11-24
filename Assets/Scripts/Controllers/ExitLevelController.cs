using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitLevelController : Interactible {

    public string nextSceneName;

    public override void Interact()
    {
        StartCoroutine(this.CoroutineCanExit());
    }

    private IEnumerator CoroutineCanExit()
    {
        yield return new WaitUntil(() => !GlobalHelper.GetAudioController().SingleAudioClipIsPlaying());
        SceneManager.LoadScene(nextSceneName);
    }

    public override void SetPreviousColor()
    {
        this.SetPreviousColor(this.gameObject);
    }
}
