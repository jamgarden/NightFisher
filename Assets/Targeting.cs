using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeting : MonoBehaviour
{

    public float reload = 2;
    private float cooldown = 0;
    public bool targetAcquired = false;

    
    public GameObject bullet;

    [SerializeField]
    private Transform gun;
    private GameObject target;

    // Start is called before the first frame update
    [SerializeField]
    private string targetName = "PlayerCapsule";
    void Start()
    {
        // gun = GetComponentInChildren<Transform>();
    }

    private void OnTriggerEnter(Collider source)
    {
        Debug.Log(source.gameObject.name);
        this.gameObject.GetComponent<SphereCollider>().enabled = false;
        if(source.gameObject.name == targetName)
        {
            targetAcquired = true;
            target = source.gameObject;
            Attack(target);
        }
        // Instantiate(bullet, gun); // Might need to use world space
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
                correction = item.transform;
            }

        }

        this.transform.LookAt(correction.position, this.transform.up);
        GameObject bulletA = Instantiate(bullet, gun.position, this.transform.rotation) as GameObject; // Might need to use world space
        bulletA.GetComponent<BulletMB>().updateTarget(correction.gameObject);
    }



    private void Update()
    {
        if (targetAcquired && cooldown < reload)
        {
            cooldown += Time.deltaTime;
        }else if(targetAcquired && cooldown > reload)
        {
            cooldown = 0;
            Attack(target);

        }
    }
}
