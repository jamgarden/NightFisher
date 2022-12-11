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
            case 8:
                reflect();
                break;
            case 7:
                Debug.Log("I hit a bogey");
                other.gameObject.GetComponent<EnemyCore>().Damage(baseDamage);
                Destroy(this.gameObject);
                break;
            default:
                break;
        }
        if(other.name == intendedTarget.name)
        {
            Debug.Log("OUCH");
            Destroy(this.gameObject);
            intendedTarget.GetComponentInParent<PlayerCore>().Damage(baseDamage); // Deal damage here.
        }
    }

    public void reflect()
    {
        reflected = true;
        duration += 3;
    }
}
