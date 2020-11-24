using System;
using UnityEngine;
using System.Linq;

public class StageController : MonoBehaviour {

    private int _currentStage;   
    public int CurrentStage {
        get { return _currentStage; }
        set {
            _currentStage = value;
        }
    }

    // Use this for initialization
    void Start ()
    {
        if(!(CurrentStage > 0))
        {
            CurrentStage = 0;
        }        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public Boolean NextStageIsAccesable(int interactedStage)
    {
        return GetNextStageNumber() == interactedStage;
    }

    //Returns the hint audio, for how to get to the next stage
    public AudioClip[] GetNextStageHint()
    {
        var InteractibleObjects = FindObjectsOfType<Interactible>().ToList();

        var NextInteractible = InteractibleObjects.Find(x => x.stage == GetNextStageNumber());

        return NextInteractible != null ? NextInteractible.nextStageHintAudioClips : null;        
    }

    private int GetNextStageNumber()
    {
        return this.CurrentStage + 1;
    }
}
