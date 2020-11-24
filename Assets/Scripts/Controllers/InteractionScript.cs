using UnityEngine;

public class InteractionScript : MonoBehaviour
{
    //References for the anchor camera's, needed to ensure the correct camera will be used for raycasting
    public GameObject CenterEyeAnchor;
    public GameObject LeftHandAnchor;
    public GameObject RightHandAnchor;
    private GameObject UsedHandAnchor;

    //Previous object, used to check if the object the raycast hit is the same as the previous one
    private Interactible previousInteractible;

    //Interacted object. Means the last object that is clicked
    private Interactible lastInteractedObject;

    //Interface that enforces methods for interactible objects in the scene
    Interactible currentInteractible;

    public float RayLength = 50f;
    private Transform player;
    private GameObject teleportMarker;
    private RaycastHit seen;
    private bool canMove = false;

    void Start()
    {
        currentInteractible = null;
        this.teleportMarker = GlobalHelper.GetTeleportMarker();
        this.player = GlobalHelper.GetOVRPlayerController().transform;        
    }

    void DeselectPreviousObject()
    {
        //Previous object not null and was a Interactable object
        var PreviousInteractableObject = previousInteractible != null ? previousInteractible.GetComponent("Interactible") as Interactible : null;

        if (PreviousInteractableObject != null)
        {
            PreviousInteractableObject.ToggleHighlight();
            previousInteractible = null;
        }
    }

    public bool HasLastInteractedAndIsHolding()
    {
        return lastInteractedObject != null && lastInteractedObject.GetIsHolding();
    }

    void Update()
    {
        CheckForController();
        teleportMarker.SetActive(false);

        Ray raydirection = new Ray(UsedHandAnchor.transform.position, UsedHandAnchor.transform.forward);

        if (Physics.Raycast(raydirection, out seen, RayLength))
        {
            currentInteractible = seen.transform.gameObject.GetComponent<Interactible>();

            StageController stageController = GlobalHelper.GetGameController().GetComponent<StageController>();
            AudioController audioController = GlobalHelper.GetAudioController();

            //Found object is    interactible and script is enabled
            //And voice is not playing
            if (currentInteractible != null && currentInteractible.enabled && !audioController.SingleAudioClipIsPlaying())
            {
                //Previous object not equals to founded object then toggle it, and deselect the previous
                //Because the ray casts against an object different then the one with the highlight
                if (previousInteractible != currentInteractible)
                {
                    currentInteractible.ToggleHighlight();
                    this.DeselectPreviousObject();
                }

                //Interactable is found and mouse is clicked
                if (Input.GetMouseButtonDown(0))
                {
                    //Remember the interactible as lastInteractableObject
                    if (currentInteractible != lastInteractedObject && lastInteractedObject != null && lastInteractedObject.GetIsHolding())
                    {
                        lastInteractedObject.Interact();
                    }
                    
                    if (stageController.NextStageIsAccesable(currentInteractible.stage) || currentInteractible.isPartOfStage) {
                        
                        if (stageController.NextStageIsAccesable(currentInteractible.stage) && !currentInteractible.isPartOfStage){
                            stageController.CurrentStage = currentInteractible.stage;
                        }

                        if (currentInteractible.HasExtraSingleAudioClips())
                        {
                            audioController.ExtraBackgroudAudioSource = currentInteractible.extraSingleAudioSource;
                            audioController.PlayClipsInAudioSource(currentInteractible.extraSingleAudioClips, AudioSourceType.ExtraSingle);
                        }

                        //Play stageEntered voice audio that is attached to this interactible                                               
                        audioController.PlayClipsInAudioSource(currentInteractible.interactAudioClips, AudioSourceType.Single);
                        audioController.PlayClipsInAudioSource(currentInteractible.loopAudioClips, AudioSourceType.Loop);

                        //And call the interact function
                        currentInteractible.Interact();                                                                                         
                    }
                    else
                    {
                        //Play hint, what to do to get to next stage
                        audioController.PlayClipsInAudioSource(stageController.GetNextStageHint(), AudioSourceType.Single);
                    }

                    //Interactible is clicked, save as last interactible
                    lastInteractedObject = currentInteractible;
                }
                //Set previousobject
                previousInteractible = currentInteractible;
            }
            else
            {
                //Objects in raycast are not Interactable
                //Clear the previous object
                this.DeselectPreviousObject();

                //We have interacted once with a object?
                if (lastInteractedObject != null && lastInteractedObject.GetIsHolding())
                {
                    canMove = false;

                    //Object is holding, so interact again when clicked
                    if (Input.GetMouseButtonDown(0) && lastInteractedObject.GetIsHolding())
                    {
                        lastInteractedObject.Interact();
                        lastInteractedObject = null;
                    }
                }
                else
                {
                    canMove = true;
                }
                
                //Tag is ground and has not a lastinteracted object
                if (HitGround() && canMove)
                {
                    if (!teleportMarker.activeSelf)
                    {
                        teleportMarker.SetActive(true);
                    }

                    MeshRenderer renderer = teleportMarker.GetComponent<MeshRenderer>();
                    var height = renderer.bounds.size.y / 2;
                    teleportMarker.transform.position = new Vector3(seen.point.x, seen.point.y + height, seen.point.z);

                    if (Input.GetMouseButtonDown(0))
                    {
                        Vector3 markerPosition = teleportMarker.transform.position;
                        player.position = new Vector3(seen.point.x, player.position.y, seen.point.z);
                    }
                }
                else
                {
                    teleportMarker.SetActive(false);
                }
            }
        }
    }

    void CheckForController()
    {
        if (OVRInput.IsControllerConnected(OVRInput.Controller.LTrackedRemote))
        {
            //Left handed remote connected
            //Debug.Log("Left hand IsControllerConnected: " + OVRInput.IsControllerConnected(OVRInput.Controller.LTrackedRemote));
            UsedHandAnchor = LeftHandAnchor;
        }
        else if (OVRInput.IsControllerConnected(OVRInput.Controller.RTrackedRemote))
        {
            //Right handed remote connected
            //Debug.Log("Right hand IsControllerConnected: " + OVRInput.IsControllerConnected(OVRInput.Controller.RTrackedRemote));
            UsedHandAnchor = RightHandAnchor;
        }
        else
        {
            //No remote connected
            //Debug.Log("No controller connected");
            UsedHandAnchor = CenterEyeAnchor;
        }
    }

    public bool HitGround()
    {
        return this.seen.collider.tag == "Ground";
    }       
}
