using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;



public class EnemyCore : MonoBehaviour
{

    StudioEventEmitter soundEmitter;
    
    [SerializeField]
    private int health = 2;


    private void Start()
    {
        soundEmitter = GetComponent<StudioEventEmitter>();
    }
    public void Damage(int incomingDamage)
    {
        health -= incomingDamage;
        if(health < 1)
        {
            AudioManager.instance.PlayOneShot(FMODevents.instance.sentinelDeath, this.transform.position);
            Destroy(this.gameObject);
        }
    }

    // emit a beeping noise
    public void Beep()
    {
        StartCoroutine(pattern1());

    }

    IEnumerator pattern1()
    {
        for (int i = 0; i < 4; i++)
        {
            soundEmitter.Play();
            
            yield return new WaitForSeconds(0.5f);
            
        }

    }
}
