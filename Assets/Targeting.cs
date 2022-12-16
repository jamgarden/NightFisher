using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class Targeting : MonoBehaviour
{


    public float reload = 2;
    private float cooldown = 0;
    public bool targetAcquired = false;

    
    public GameObject bullet;

    [SerializeField]
    private Transform gun;
    private GameObject target;

    private StudioEventEmitter soundEmitter;
    // Start is called before the first frame update
    [SerializeField]
    private string targetName = "PlayerCapsule";
    void Start()
    {
        // gun = GetComponentInChildren<Transform>();
        soundEmitter = GetComponent<StudioEventEmitter>();
    }

    private void OnTriggerEnter(Collider source)
    {
        Debug.Log(source.gameObject.name);
        this.gameObject.GetComponent<SphereCollider>().enabled = false;
        if(source.gameObject.name == targetName)
        {
            targetAcquired = true;
            target = source.gameObject;
            // Attack(target);
            // windup(); this should play some sort of warning noise, and maybe trigger an animation
            StartCoroutine(WindUp());
        }
    }

    public IEnumerator WindUp()
    {
        // This function plays a warning sound before attacking.
        Debug.Log("Winding up");
        // start making noises here.
        for (int i = 0; i < 4; i++)
        {
            yield return new WaitForSeconds(0.5f);
            soundEmitter.Play();
        }
        while (true)
        {
            Attack(target);
            soundEmitter.Play();
            yield return new WaitForSeconds(reload);
        }
    }
     

    public void Attack(GameObject target)
    {
        // this is the logic that actually fires the gunnnnnn.
        Transform[] targetArr = target.gameObject.GetComponentsInChildren<Transform>();

        Transform correction = this.transform; 
        foreach (Transform item in targetArr)
        {
            if(item.name == "Capsule") // Ew, hardcoded name.  This code searches through the child components and target's the players center. 
            {
                correction = item.transform; // This correction prevents us from shooting at the player's feet. 
            }

        }

        this.transform.LookAt(correction.position, this.transform.up);
        GameObject bulletA = Instantiate(bullet, gun.position, this.transform.rotation) as GameObject; // Might need to use world space
        bulletA.GetComponent<BulletMB>().updateTarget(correction.gameObject);
    }



    private void Update()
    {
        // This had something in it, I swear.
    }
    
}
