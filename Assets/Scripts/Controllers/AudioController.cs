using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum AudioSourceType
{
    Loop,
    Single,
    ExtraSingle
}

public class AudioController : MonoBehaviour {
   
    private AudioSource[] loopAudioSources;
    private AudioSource[] singleAudioSources;
    private AudioSource[] extraSingleAudioSources = new AudioSource[0];

    public AudioClip[] loopAudioClips;
    public AudioClip[] singleAudioClips;

    public AudioSource extraSingleAudioSource;
    public AudioClip[] extraSingleAudioClips;

    public AudioSource ExtraBackgroudAudioSource 
    {
        set
        {
            extraSingleAudioSources = new AudioSource[1];
            this.extraSingleAudioSources[0] = GameObject.Find(value.gameObject.name).GetComponent<AudioSource>();
        }
    }

    // Use this for initialization
    void Start () {
        this.loopAudioSources = GameObject.Find("LoopAudioSource").GetComponents<AudioSource>();       
        this.singleAudioSources = GameObject.Find("SingleAudioSource").GetComponents<AudioSource>();
        if(extraSingleAudioSource != null)
        {
            this.ExtraBackgroudAudioSource = this.extraSingleAudioSource;
        }        

        this.PlayClipsInAudioSource(this.loopAudioClips, AudioSourceType.Loop);
        this.PlayClipsInAudioSource(this.singleAudioClips, AudioSourceType.Single);
        this.PlayClipsInAudioSource(this.extraSingleAudioClips, AudioSourceType.ExtraSingle);
    }

    // Update is called once per frame
    void Update () {
        
    }   

    //Return audiosource by Enum
    public AudioSource[] GetAudioSourceByType(AudioSourceType audioSourceType)
    {
        switch (audioSourceType)
        {
            case AudioSourceType.Loop:
                return loopAudioSources;
            case AudioSourceType.Single:
                return singleAudioSources;
            case AudioSourceType.ExtraSingle:
                return extraSingleAudioSources;
        }

        return null;
    }

    public void PlayClipsInAudioSource(AudioClip[] clips, AudioSourceType audioSourceType)
    {
        AudioSource[] audioSource = GetAudioSourceByType(audioSourceType);

        this.PlayClipsInAudioSource(clips, ref audioSource);
    }

    private void PlayClipsInAudioSource(AudioClip[] audioClips, ref AudioSource[] audioSources)
    {
        if (audioClips != null)
        {
            //Not enough audiosources for every clip
            if (audioSources.Length < audioClips.Length)
            {
                AudioSource placeholderAudioSource = audioSources.First();

                int whileCounter = 0;
                while (audioSources.Length != audioClips.Length)
                {
                    whileCounter++;
                    
                    //The while loop is more then amount of audioClips.length, so break it.
                    if(whileCounter > audioClips.Length)
                    {
                       break;
                    }

                    AudioSource placeholder = placeholderAudioSource.GetComponent<AudioSource>();

                    placeholder.gameObject.AddComponent<AudioSource>(placeholder);

                    //audioSources is a reference to the instance
                    //Update new audiosources. !IMPORTANT
                    audioSources = GameObject.Find(audioSources.First().gameObject.name).GetComponents<AudioSource>();
                }
            }

            //Play all audioclips
            foreach (AudioClip clip in audioClips)
            {
                AudioSource source = audioSources[System.Array.IndexOf(audioClips, clip)];
                source.clip = clip;
                source.Play();
            }
        }
    }

    //Returns if a audioClip is playing in the SingleAudioSource
    public bool SingleAudioClipIsPlaying()
    {
        foreach(AudioSource source in singleAudioSources)
        {
            if (source.isPlaying)
            {
                return true;
            }
        }
        return false;
    }
}
