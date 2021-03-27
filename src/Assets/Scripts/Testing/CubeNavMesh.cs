using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/**
		For testing purposes.
		For testing of the Unity Nav Mesh Agent component.
	*/
public class CubeNavMesh : MonoBehaviour {


    public List<Transform> waypoints = new List<Transform>();
    int currWaypoint = 0;

    NavMeshAgent agent;
    

	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine("Patrol");
	}

    IEnumerator Patrol()
    {
        for (; ;)
        {
            if (!agent.isStopped)
            {
                agent.SetDestination(waypoints[currWaypoint].position);


                while (waypoints[(++currWaypoint) % waypoints.Count] == null)
                {
                    ;
                }
                currWaypoint %= waypoints.Count;
            }

            yield return new WaitForSeconds(10f);
        }
        
    }
	
	// Update is called once per frame
	void Update () {
       // Debug.Log(agent.velocity.magnitude / agent.speed);
	}
}
