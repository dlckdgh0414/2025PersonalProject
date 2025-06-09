using UnityEngine;

public enum AudioType
{
    BGM,
    SFX
}

public static class AudioEvents
{
    public static AudioChangeEvent AudioChangeEvent = new AudioChangeEvent();
}

public class AudioChangeEvent : GameEvent 
{
    public AudioType audioType;
    public AudioClip clip;
    public bool IsLooping;

    public AudioChangeEvent Initializer(AudioType audioType,AudioClip clip,bool isLooping)
    {
        this.audioType = audioType;
        this.clip = clip;
        IsLooping = isLooping;
        return this;
    }
}

