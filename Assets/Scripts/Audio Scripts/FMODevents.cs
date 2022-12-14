using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODevents : MonoBehaviour
{
    [field: Header("World Sounds")]
  
    [field: SerializeField] public EventReference ForestAmbience { get; private set; }
    [field: SerializeField] public EventReference ForestMusic { get; private set; }
    [field: Header("Player SFX")]
    [field: SerializeField] public EventReference bubbleShield { get; private set; }  
    [field: SerializeField] public EventReference playerFootsteps { get; private set; }
    [field: Header("Sentinel SFX")]  
    [field: SerializeField] public EventReference sentinelDeath { get; private set; }  
    [field: SerializeField] public EventReference reflectShot { get; private set; }  
    [field: SerializeField] public EventReference sentinelDamaged { get; private set; }
    [field: Header("Object Sounds")]
    [field:SerializeField] public EventReference mirrorBreak { get; private set;}
    [field:SerializeField] public EventReference glassFly { get; private set;}
    public static FMODevents instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one FMOD Events script in the scene.");
        }
        instance = this;
    }
}
