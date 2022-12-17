using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "StoryState", menuName = "ScriptableObjects/StoryState", order = 1)]
public class StoryState : ScriptableObject
{
    public List<Vector3> Teleporters;

    public bool addTeleporter(Transform input)
    {
        // Check input against Teleporters for distance.  Close targets should not be added to Teleporters.
        foreach (Vector3 item in Teleporters)
        {
            if(Vector3.Distance(item, input.position) < 5)
            {
                
                return false;
            }
        }
        Teleporters.Add(input.position);
        return true;
    }

    public Vector3 nextTeleport(Vector3 input)
    {
        Vector3 Target = input;
        for(int i = 0; i < Teleporters.Count; i++)
        {
            if(Vector3.Distance(input, Teleporters[i]) < 4)
            {
                if(Teleporters.Count < 2)
                {
                    return new Vector3(0, 0, 0);
                }
                if (i + 1 == Teleporters.Count)
                {
                    Target = Teleporters[0];
                    Debug.Log(Vector3.Distance(input, Teleporters[i]));
                    return Target;
                }
                else
                {
                    Target = Teleporters[i + 1];
                    return Target;
                }
            }
        }
        return new Vector3(0, 0, 0);
        

    }

}
