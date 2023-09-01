using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetIndicator : MonoBehaviour
{
    private Transform target;

    //Islands
    [SerializeField] private Transform island1;
    [SerializeField] private Transform island2;
    [SerializeField] private Transform island3;
    [SerializeField] private Transform island4;
    [SerializeField] private Transform island5;



    private void Update()
    {
        //get variable for island
        int lastislandVisited = ((Ink.Runtime.IntValue)DialogueManager.getInstance().GetVariablesState("islands_visited")).value;

        if(lastislandVisited == 0)
        {
            target = island1;
        }
        else if (lastislandVisited == 1)
        {
            target = island2;
        }
        else if (lastislandVisited == 2)
        {
            target = island3;
        }
        else if (lastislandVisited == 3)
        {
            target = island4;
        }
        else if (lastislandVisited == 4)
        {
            target = island5;
        }

        var direction = target.position - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
