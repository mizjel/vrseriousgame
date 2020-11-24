using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Interactible : MonoBehaviour, IInteractible {

    protected Color PreviousColor;
    protected bool isShowable = false;

    protected bool isHighlighted = false;
    protected bool isHolding = false;

    public int stage;
    public bool isPartOfStage = false;

    public AudioClip[] nextStageHintAudioClips = new AudioClip[0];
    public AudioClip[] interactAudioClips = new AudioClip[0];
    public AudioClip[] loopAudioClips = new AudioClip[0];

    public AudioSource extraSingleAudioSource = new AudioSource();
    public AudioClip[] extraSingleAudioClips = new AudioClip[0];

    public virtual void Start()
    {
        if (this.gameObject.GetComponent<Renderer>() == null)
        {
            this.gameObject.AddComponent<Renderer>();
        }

        if (this.gameObject.GetComponent<BoxCollider>() == null)
        {
            this.gameObject.AddComponent<BoxCollider>();
        }

        this.gameObject.GetComponent<GameController>();
        SetPreviousColor();
    }

    public Interactible(bool isShowable = false)
    {
        this.isShowable = isShowable;
    }

    public virtual void ToggleHighlight()
    {
        Image image = GetComponent<Image>();
        Renderer renderer = GetComponent<Renderer>();
        
        if (image != null)
        {
            if (isHighlighted)
            {
                image.color = PreviousColor;
                isHighlighted = false;
            }
            else if (!isHighlighted)
            {
                image.color = Color.green;
                isHighlighted = true;
            }
        }
        else
        {
            if (isHighlighted)
            {
                renderer.material.color = PreviousColor;
                isHighlighted = false;
            }
            else if (!isHighlighted)
            {
                renderer.material.color = Color.green;
                isHighlighted = true;
            }
        }
        
    }

    public bool GetIsShowable()
    {
        return isShowable;
    }

    public bool GetIsHolding()
    {
        return isHolding;
    }

    public bool HasExtraSingleAudioClips()
    {
        return this.extraSingleAudioClips.Length > 0;
    }

    public abstract void Interact();
    public abstract void SetPreviousColor();

    public void SetPreviousColor(GameObject gameObject)
    {
        var renderer = gameObject.GetComponent<Renderer>();

        PreviousColor = renderer.material.color;
    }
}
