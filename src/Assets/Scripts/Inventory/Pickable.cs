using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Game object that can be simply picked up
 */ 
public class Pickable : MonoBehaviour, IInteractable {

    [SerializeField]
    private Item item;

    private Inventory inventory;
    private bool active = true;

    public bool Active
    {
        get
        {
            return active;
        }

        set
        {
            active = value;
        }
    }

    public Vector3 Position
    {
        get
        {
            return transform.position;
        }
    }

    // Use this for initialization
    void Start () {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    /*
     * Adds the item to the players inventory and destroys the GameObject
     */ 
    public void Interact()
    {
        if (inventory.AddItem(item))
        {
            Debug.Log("Picked up " + item.name);
            Destroy(gameObject);
        }
    }
    
}
