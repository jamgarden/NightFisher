using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class AudioManager : MonoBehaviour
{
    private List<EventInstance> eventInstances;
    private List<StudioEventEmitter> eventEmitters;
    private EventInstance jetpackSpeedEventInstance;
    private EventInstance musicEventInstance;
    public static AudioManager instance { get; private set; }

    [SerializeField] public string parameterName;
    [SerializeField] public string parameterValue;
    
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
        InitializeJetpackSpeed(FMODevents.instance.jetpackSpeed);
        InitializeMusic(FMODevents.instance.ForestMusic);
    }

    private void InitializeJetpackSpeed(EventReference jetpackSpeedEventReference)
    {
        jetpackSpeedEventInstance = CreateInstance(jetpackSpeedEventReference);
        jetpackSpeedEventInstance.start();
    }

    private void InitializeMusic(EventReference musicEventReference)
    {
        musicEventInstance = CreateInstance(musicEventReference);
        musicEventInstance.start();
    }

    public void SetJetpackSpeedParameter(string parameterName, float parameterValue)
    {
        jetpackSpeedEventInstance.setParameterByName(parameterName, parameterValue);
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
