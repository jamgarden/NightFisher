using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODevents : MonoBehaviour
{
    [field: Header("Sentinel Death")]  
    [field: SerializeField] public EventReference sentinalDeath { get; private set; }
    [field: Header("ReflectShot")]  
    [field: SerializeField] public EventReference reflectShot { get; private set; }
    [field: Header("Sentinel Damaged")]  
    [field: SerializeField] public EventReference sentinelDamaged { get; private set; }

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
