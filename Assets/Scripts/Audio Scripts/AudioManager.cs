using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class AudioManager : MonoBehaviour
{
    private List<EventInstance> eventInstances;
    private List<StudioEventEmitter> eventEmitters;
    private EventInstance forestAmbienceEventInstance;
    private EventInstance musicEventInstance;
    public static AudioManager instance { get; private set; }
    
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more that one Audio Manager in the scene");
        }
        instance = this;

        eventInstances = new List<EventInstance>();
        eventEmitters = new List<StudioEventEmitter>();
    }

    private void Start()
    {
        //InitializeAmbience(FMODevents.instance.ForestAmbience);
        InitializeMusic(FMODevents.instance.ForestMusic);
    }

    private void InitializeAmbience(EventReference forestAmbienceEventReference)
    {
        forestAmbienceEventInstance = CreateInstance(forestAmbienceEventReference);
        forestAmbienceEventInstance.start();
    }

    private void InitializeMusic(EventReference musicEventReference)
    {
        musicEventInstance = CreateInstance(musicEventReference);
        musicEventInstance.start();
    }



    public void PlayOneShot(EventReference sound, Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(sound, worldPos);
    }
     
    public EventInstance CreateInstance(EventReference eventReference)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        eventInstances.Add(eventInstance);
        return eventInstance;
    }
    
    private void CleanUp()
    {
        // stop and release any created instances
        foreach (EventInstance eventInstance in eventInstances)
        {
            eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            eventInstance.release();
        }
    }

    private void OnDestroy() 
    {
        CleanUp();
    }
}
