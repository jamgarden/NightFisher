using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMB : MonoBehaviour
{
    public GameObject intendedTarget;

    [SerializeField]
    private float duration=7;

    [SerializeField]
    private float speed = 5;

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
        rb.velocity = transform.forward * speed * Time.deltaTime;
        if(duration < 0)
        {
            Debug.Log("This should be going away");
            Destroy(this.gameObject);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == intendedTarget.name)
        {
            Debug.Log("OUCH");
            Destroy(this.gameObject); // Deal damage here.
        }
    }
}
