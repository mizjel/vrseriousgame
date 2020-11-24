using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroRoomController : MonoBehaviour
{
    public Text textElement;
    public string displayText;
    public float typeInterval;
    private int currentTextIndex = 0;
    private IEnumerator coroutine;
    private bool routineDone = false;
    public GameObject vrCameraUI;
    public GameObject startButton;
    public GameObject nextLevelButton;

    // Use this for initialization
    void Start()
    {
        GlobalHelper.GetOVRPlayerController().GetComponent<OVRPlayerController>().GravityModifier = 0;
        startButton = GameObject.Find("IntroStartButton");
        nextLevelButton = GameObject.Find("IntroStartGame");
        nextLevelButton.SetActive(false);
        vrCameraUI = GlobalHelper.GetVRCameraUI();
        textElement.text = string.Empty;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartTextTyperRoutine()
    {
        coroutine = TextTyperRoutine();
        StartCoroutine(coroutine);
    }

    public IEnumerator TextTyperRoutine()
    {
        for (int i = 0; i <= displayText.Length; i++)
        {
            textElement.text = displayText.Substring(0, i);
            if (i >= displayText.Length)
            {
                vrCameraUI.SetActive(true);
                nextLevelButton.SetActive(true);
                yield break;
            }
            yield return new WaitForSeconds(typeInterval);
        }
    }
}
