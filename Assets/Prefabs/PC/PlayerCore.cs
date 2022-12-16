using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using Yarn.Unity;


public class PlayerCore : MonoBehaviour
{
    [SerializeField]
    private PlayerState playerState;
    private DialogueRunner dialogueRunner;
    private StarterAssets.StarterAssetsInputs controls; // Trying to disable controls, but having a time tracking

    public bool talkable = false;
    public string targetNode = "";
    public GameObject dialogueHolder;


    private void Start()
    {

        controls = GetComponent<StarterAssets.StarterAssetsInputs>();
        playerState.health = playerState.maxHealth; // If we have different scenes, this will heal our character every scene change.
        if (dialogueHolder != null)
        {
            dialogueRunner = dialogueHolder.GetComponent<DialogueRunner>();
            // dialogueRunner.onDialogueComplete.AddListener(DoneInteracting);
        }
        else
        {
            Debug.LogWarning("You haven't specified which DialogueSystem to use!");
        }
    }


    // Public methods
    public void Damage(int damage)
    {
        playerState.health -= damage;
        if(playerState.health < 1)
        {
            Die();
        }
    }
    

    public void Die()
    {
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("GameOver"); // Whisks us directly to the game over screen.
    }

    public void ReleaseCursor()
    {
        // Needs to enable input
        // Currently just releases control of the cursor.
        
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void GrabCursor()
    {
        // Needs to Disable input
        // Currently just lets us control the cursor
        Cursor.lockState = CursorLockMode.None;
    }

    // Private Methods
    private void OnInteract()
    {

        Debug.Log("OnInteract fired");

        if (talkable && dialogueRunner != null)
        {
            if (targetNode != "")
            {
                controls.StopAllCoroutines();
                Cursor.lockState = CursorLockMode.None;
                dialogueRunner.Stop();
                dialogueRunner.StartDialogue(targetNode);
                talkable = false;
            }
            else
            {
                Debug.LogWarning("No target node");
            }
        }
        else
        {
            Debug.LogWarning("No Dialogue Runner to Start Talking");
        }
    }


    // Yarn Commands
    [YarnCommand("enter_room")]
    private void EnterRoom(string roomName)
    {
        if (SceneUtility.GetBuildIndexByScenePath(roomName) >= 0)
        {
            SceneManager.LoadScene(roomName);
        }
        else
        {
            Debug.LogWarning("Scene " + roomName + " not in build.");
        }
    }

}
