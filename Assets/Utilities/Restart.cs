using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{

    public string target;

    // Update is called once per frame
    public void StartOver()
    {
        // This resets 
        SceneManager.LoadScene(target);
    }
}
