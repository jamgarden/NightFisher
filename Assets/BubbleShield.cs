using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class BubbleShield : MonoBehaviour
{
    [SerializeField]
    private GameObject shield;

    private bool shielding = false;

    private float duration;
    public float maxDuration = 1.5f;



    private void Start()
    {
        duration = maxDuration;
    }


    private void Update()
    {
        if (shielding)
        {
            duration -= Time.deltaTime;
            if(duration < 0)
            {
                shield.SetActive(false);
                duration = maxDuration;
                shielding = false; 
            }
        }
    }

    private void OnShield()
    {
        if (!shielding)
        {
            GameObject papa = GetComponentInParent<PlayerCore>().gameObject;
            Debug.Log(papa.layer);
            AudioManager.instance.PlayOneShot(FMODevents.instance.bubbleShield, this.transform.position);
            shield.SetActive(true);
            shielding = true;
        }
        
    }
}
