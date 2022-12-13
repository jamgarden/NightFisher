using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyCore : MonoBehaviour
{
    
    [SerializeField]
    private int health = 2;

    public void Damage(int incomingDamage)
    {
        health -= incomingDamage;
        if(health < 1)
        {
            AudioManager.instance.PlayOneShot(FMODevents.instance.sentinalDeath, this.transform.position);
            Destroy(this.gameObject);
        }
    }
}
