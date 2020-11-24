using UnityEngine;

public class GameController : MonoBehaviour {
   
    private Vector3 playerSpawnPosition;
    private Quaternion playerSpawnRotation;

    public int startStage;
    public bool hasPuzzle;

    [HideInInspector] // Hides var below
    public GameObject cameraUIViewer;

    // Use this for initialization
    void Start () {
        cameraUIViewer = GlobalHelper.GetCameraUIViewer();
        
        if (hasPuzzle)
        {
            //UIViewer is necessary for Puzzle
            this.gameObject.AddComponent<PuzzlePictureController>();
            this.gameObject.AddComponent<FolderPanelUIController>();
            this.gameObject.AddComponent<KeypadController>();
        }
        else
        {
            //UIViewer is not necessary for Puzzle            
            cameraUIViewer.SetActive(false);
        }

        this.gameObject.AddComponent<CanvasRoomController>();    
        this.gameObject.AddComponent<StageController>();
        if (startStage != 0)
        {
            this.gameObject.GetComponent<StageController>().CurrentStage = startStage;
        }
        SetPlayerSpawnTransformValues();
    }

    // Update is called once per frame
    void Update () {

    }

    private void SetPlayerSpawnTransformValues()
    {
        this.playerSpawnPosition = GlobalHelper.GetOVRPlayerController().transform.position;
        this.playerSpawnRotation = GlobalHelper.GetOVRPlayerController().transform.rotation;
    }

    public Vector3 GetPlayerSpawnTransformPosition()
    {
        return this.playerSpawnPosition;
    }

    public Quaternion GetPlayerSpawnTransformRotation()
    {
        return this.playerSpawnRotation;
    }    
}
