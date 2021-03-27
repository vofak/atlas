using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
	Representation of an NPC's patrol.
	This class holds all patrol's waypoints and handles correct looping through them.
*/
[System.Serializable]
public class Patrol {

    public int currentIndex;
    public int size;

    [Header("Inspector assingment")]
    public List<Transform> patrolWaypoints;
    

    /*public bool isOnPatrol;
    public int nextWaypoint;
    public float waitingTime;*/

    public void Init()
    {
        currentIndex = -1;
        size = patrolWaypoints != null ? patrolWaypoints.Count : -1;
    }

    public Transform getNextWaypoint()
    {
        if (size < 1)
        {
            return null;
        }

        IncrementIndex();
        return patrolWaypoints[currentIndex];
    }

    private void IncrementIndex()
    {
        currentIndex += 1;
        if (currentIndex > size - 1)
        {
            currentIndex = 0;
        }
    }
}
