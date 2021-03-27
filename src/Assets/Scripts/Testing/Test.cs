using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
	For testing purposes.
	Tests Unity enumarator.
*/
public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine("Life");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator Life()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
