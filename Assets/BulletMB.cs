using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMB : MonoBehaviour
{
    public GameObject intendedTarget;

    [SerializeField]
    private float duration=7;

    public int baseDamage = 1;

    [SerializeField]
    private float speed = 5;

    [SerializeField]
    private bool reflected;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void updateTarget(GameObject target)
    {
        intendedTarget = target;
    }

    private void Update()
    {
            transform.LookAt(intendedTarget.transform);
            duration -= Time.deltaTime;
        if (reflected)
        {

            rb.velocity = transform.forward * -speed * Time.deltaTime;
            if (duration < 0)
            {
                Debug.Log("This should be going away");
                Destroy(this.gameObject);
            }

        }
        else
        {
            
            rb.velocity = transform.forward * speed * Time.deltaTime;
            if(duration < 0)
            {
                Debug.Log("This should be going away");
                Destroy(this.gameObject);
            }
        }
        
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
        switch (other.gameObject.layer)
        {
            case 10: // Invincible targets are on layer 10
                     // We don't need to do anything here!
                break;
            case 9: // bullets are on layer 9
                // We don't need to do anything with this just yet. 
                break;
            case 8: // shield for 8
                AudioManager.instance.PlayOneShot(FMODevents.instance.reflectShot, this.transform.position);
                reflect();
                break;
            case 7: // enemies for 7
                AudioManager.instance.PlayOneShot(FMODevents.instance.sentinelDamaged, this.transform.position);
                Debug.Log("I hit a bogey");
                other.gameObject.GetComponent<EnemyCore>().Damage(baseDamage);
                Destroy(this.gameObject);
                break;
            case 6: // Players for 6
                Debug.Log("PlayerHit");
                other.GetComponentInParent<PlayerCore>().Damage(baseDamage);
                break;
            default:
                break;
        }
    }

    public void reflect()
    {
        reflected = true;
        duration += 3;
        
    }
}
