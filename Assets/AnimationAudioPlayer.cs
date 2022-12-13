using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class AnimationAudioPlayer : MonoBehaviour
{   
    public void PlayMirrorBreak()
    {
        AudioManager.instance.PlayOneShot(FMODevents.instance.mirrorBreak, transform.position);
    }
    public void PlayPlayGlassFly()
    {
        AudioManager.instance.PlayOneShot(FMODevents.instance.glassFly, transform.position);
    }


}
