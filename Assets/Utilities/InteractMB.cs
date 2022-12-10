using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractMB : MonoBehaviour
{
    [SerializeField]
    private string PlayerObjectName = "";
    public string targetNode;
    // Update is called once per frame
    private void OnTriggerEnter(Collider collider)
    {
        //Debug.Log(collider.gameObject.name); //Useful to find out if your player isn't called "Player"

        if (collider.gameObject.name == PlayerObjectName)
        {
            PlayerCore player = collider.gameObject.GetComponentInParent<PlayerCore>();
            player.talkable = true;
            player.targetNode = this.targetNode;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        //Debug.Log(collider.gameObject.name);
        if (collider.gameObject.name == PlayerObjectName)
        {
            PlayerCore player = collider.gameObject.GetComponentInParent<PlayerCore>();
            player.talkable = false;
            player.targetNode = "";
        }
    }
}
