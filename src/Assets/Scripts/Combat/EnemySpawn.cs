using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
	Behavior for enemy spawners.
*/
public class EnemySpawn : MonoBehaviour {
	
	// Instance to be spawned
    public Atlas.Core.Enemy prefab;
	
	//  Time till next enemy spawn
    public int respawnTime = 60;

	// Reference to currently spawned and living instance
    private Atlas.Core.Enemy instance;
    private bool waitingForRespawn;

    void Start()
    {
        SpawnObject();
    }

    
    void Update()
    {
        if (!waitingForRespawn && (instance == null || instance.isDead))
        {
            waitingForRespawn = true;
            StartCoroutine("WaitForRespawn");
        }
    }

    void SpawnObject()
    {
        instance = Instantiate(prefab, transform);
        instance.OnSpawn();
        waitingForRespawn = false;
    }

    private IEnumerator WaitForRespawn()
    {
        yield return new WaitForSeconds(respawnTime);
        SpawnObject();
    }
}
