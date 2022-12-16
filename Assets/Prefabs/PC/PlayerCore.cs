using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using Yarn.Unity;


public class PlayerCore : MonoBehaviour
{
    public bool talkable = false;
    public string targetNode = "";

    [SerializeField]
    private PlayerState playerState;

    private StarterAssets.StarterAssetsInputs controls;
    //public Movement moveControls;
    public GameObject dialogueHolder;
    DialogueRunner dialogueRunner;
    //private PlayerAttack attacker;


    private void Start()
    {

        controls = GetComponent<StarterAssets.StarterAssetsInputs>();
        playerState.health = playerState.maxHealth;
       // moveControls = GetComponent<Movement>();
        //attacker = GetComponent<PlayerAttack>();
        if (dialogueHolder != null)
        {
            dialogueRunner = dialogueHolder.GetComponent<DialogueRunner>();
            dialogueRunner.onDialogueComplete.AddListener(DoneInteracting);
        }
        else
        {
            Debug.LogWarning("You haven't specified which DialogueSystem to use!");
        }
    }

    // For keeping track of things like health and other instance specific things.
    // Stat block here

    public void Damage(int damage)
    {
        playerState.health--;
        if(playerState.health < 1)
        {
            Die();
        }
    }
    // Public methods here
    public void Die()
    {
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("GameOver"); // Whisks us directly to the game over screen.
    }

    void OnInteract()
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
                //moveControls.canMove = false;
                //attacker.canAttack = false;
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

    public void ReleaseCursor()
    {
        //Input
        
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void GrabCursor()
    {
        // Disable input
        // let us control the cursor
        Cursor.lockState = CursorLockMode.None;
    }

    private void DoneInteracting()
    {
        //moveControls.canMove = true;
        //attacker.canAttack = true;
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
