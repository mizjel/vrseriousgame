using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class KeypadController : MonoBehaviour {
    private List<PuzzlePictureModel> puzzlePictureModels;
    private List<KeypadPanelModel> panelModelList = new List<KeypadPanelModel>();

    private GameObject keyPad;
    private Transform[] keyPadChildren;

    private bool wrongNumberEntered = false;
    private int keyEnteredCounter = 0;

    private AudioClip[] unlockDoor;
    private AudioClip[] buzzer;

    void Start()
    {
        this.unlockDoor = Resources.LoadAll<AudioClip>("Audio/SteegScene/unlock_door");
        this.buzzer = Resources.LoadAll<AudioClip>("Audio/SteegScene/buzzer");
        this.keyPad = GameObject.FindGameObjectWithTag("Keypad");
        //Sets scripts on the keypad buttons
        SetKeyPadChildren();
        puzzlePictureModels = GlobalHelper.GetGameController().GetComponent<PuzzlePictureController>().puzzlePictureModelList;
        SubscribeToButtonEvents();
        LoadPuzzle();
    }


    //Loops through all the children(buttons) in the keypad object and assigns the KeyPadButtonController as script component
    void SetKeyPadChildren()
    {    
        keyPadChildren = this.keyPad.GetComponentsInChildren<Transform>();

        foreach (Transform button in keyPadChildren)
        {
            if (button.gameObject.tag == "KeypadButton")
            {
                if(button.gameObject.name != "#" && button.gameObject.name != "*")
                {
                    button.gameObject.AddComponent<KeypadButtonController>();
                    button.gameObject.GetComponent<KeypadButtonController>().isPartOfStage = true;
                }
            }
        }
    }

    //Gets called when the ButtonPressed event from the KeypadButtonControllers fires
    //Checks if the sender object name is the same as the current item in the puzzle sequence and moves the entry up +1 if it's correct
    public void CheckAnswerEntry(object sender, System.EventArgs e)
    {
        KeypadButtonController kbc = (KeypadButtonController)sender;
        keyEnteredCounter++;

        if (Convert.ToInt32(kbc.name) != panelModelList.ElementAt(keyEnteredCounter - 1).number)
        {
            wrongNumberEntered = true;            
        }

        SetPanelText(panelModelList.ElementAt(keyEnteredCounter - 1).panel, kbc.name);

        //Same amount of numbers entered as there are panels available
        if (keyEnteredCounter == panelModelList.Count)
        {
            keyEnteredCounter = 0;        
            
            if (wrongNumberEntered)
            {
                wrongNumberEntered = false;
                PuzzleSolved(false);
            }
            else
            {
                PuzzleSolved(true);
            }
        }           
    }

    private void PuzzleSolved(bool solved = true)
    {
        if (solved)
        {
            Debug.Log("Puzzle complete!");
            GlobalHelper.GetAudioController().PlayClipsInAudioSource(unlockDoor, AudioSourceType.Single);

            int currentStage = GlobalHelper.GetGameController().GetComponent<StageController>().CurrentStage;            
            GlobalHelper.GetGameController().GetComponent<StageController>().CurrentStage = currentStage +=1;
            Destroy(GameObject.Find("IngangKelderdeur").GetComponent<BaseInteractController>());
        }
        else
        {
            //Not solved
            Debug.Log("Puzzel niet goed!");
            GlobalHelper.GetAudioController().PlayClipsInAudioSource(buzzer, AudioSourceType.Single);

            //Change text to default
            foreach (KeypadPanelModel model in panelModelList)
            {
                SetPanelText(model.panel, "-");
            }
        }
    }

    private void SetPanelText(GameObject panel, string text)
    {
        panel.GetComponentInChildren<Text>().text = text;
    }

    //Gets all the buttons in the keypad object and subscribes to it's ButtonPressed event
    private void SubscribeToButtonEvents()
    {
        foreach(Transform button in keyPadChildren)
        {
            if (button.gameObject.tag == "KeypadButton")
            {
                KeypadButtonController controller = button.gameObject.GetComponent<KeypadButtonController>();
                if (controller != null)
                {
                    controller.ButtonPressed += CheckAnswerEntry;
                }
            }  
        }
    }
    //Just because i want a method to "load" stuff, fite me irl
    private void LoadPuzzle()
    {
        SetPanelModelList();        
    }

    //Add the PuzzlePictureModels to a KeypadPanelModel class so we can link a keypad panel to a piece of the puzzle
    //Add the model to a list of KeypadPanelModels so we can loop through them to check if the user input is a correct answer
    private void SetPanelModelList()
    {
        GameObject[] keyPadPanels = GameObject.FindGameObjectsWithTag("KeypadPanel");

        for(int i = 0; i < puzzlePictureModels.Count; i++)
        {
            Color color;
            ColorUtility.TryParseHtmlString(puzzlePictureModels[i].color.ToString(), out color);

            //Set Color on the keypadPanel
            keyPadPanels[i].GetComponent<Image>().color = color;

            panelModelList.Add(new KeypadPanelModel
            {
                panel = keyPadPanels[i],
                color = color,
                number = puzzlePictureModels[i].number
            });
        }

        //Sort the list by comparing the panel name
        panelModelList.Sort((x, y) => x.panel.name.CompareTo(y.panel.name));
    }    
}
