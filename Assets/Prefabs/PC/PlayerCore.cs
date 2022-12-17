using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using Yarn.Unity;


public class PlayerCore : MonoBehaviour
{
    [SerializeField]
    private PlayerState _playerState;
    [SerializeField]
    private StoryState _storyState;

    private DialogueRunner _dialogueRunner;
    private StarterAssets.StarterAssetsInputs _controls; // Trying to disable _controls, but having a time tracking

    public bool _talkable = false;
    public string _targetNode = "";
    public GameObject _dialogueHolder;


    private void Start()
    {
        // Editor Specific Code
        if (Application.isEditor)
        {
            _playerState.health = _playerState.maxHealth; // When in editor, initializes at the beginning of every scene. 
            _storyState.Teleporters = new List<Vector3> { }; 
        }
        

        _controls = GetComponent<StarterAssets.StarterAssetsInputs>();
        if (_dialogueHolder != null)
        {
            _dialogueRunner = _dialogueHolder.GetComponent<DialogueRunner>();
            // _dialogueRunner.onDialogueComplete.AddListener(DoneInteracting);
        }
        else
        {
            Debug.LogWarning("You haven't specified which DialogueSystem to use!");
        }
    }


    // Public methods
    public void Damage(int damage)
    {
        _playerState.health -= damage;
        if(_playerState.health < 1)
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

    public void Teleport(Vector3 newPos)
    {
        this.transform.position = newPos;
    }

    // Private Methods
    private void OnInteract()
    {

        Debug.Log("OnInteract fired");

        if (_talkable && _dialogueRunner != null)
        {
            if (_targetNode != "")
            {
                _controls.StopAllCoroutines();
                Cursor.lockState = CursorLockMode.None;
                _dialogueRunner.Stop();
                _dialogueRunner.StartDialogue(_targetNode);
                _talkable = false;
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

    [YarnCommand("log_teleporter")]
    private void LogTeleporter()
    {
        Debug.Log("Logging teleporter now at: " + this.transform.position);
        if(_storyState.addTeleporter(this.transform) == false) // If adding a teleporter returns false, that means we already have it.  We will instead teleport to the next TP on the list.
        {
            // Get next teleporter.
            Vector3 next = _storyState.nextTeleport(this.transform.position);
            if(next == new Vector3(0,0,0))
            {
                Debug.Log("You ain't goin' anywhere");
            }
            else
            {
                Teleport(next);
            }
        }
    }

}
