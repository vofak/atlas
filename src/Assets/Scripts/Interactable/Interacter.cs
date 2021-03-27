using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * player's script enabling him to interact with IInteractable objects in the game world
 */ 
public class Interacter : MonoBehaviour {

    
    public  float interactingRange = 3f;

    private InteractImage interactImage;
    private Camera cam;
    private IInteractable closestInteractable;

    // Use this for initialization
    void Start () {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        interactImage = GameObject.FindGameObjectWithTag("Interact Image").GetComponent<InteractImage>();
	}
	
	// Update is called once per frame
	void Update () {

        FindClosestInteractable();

        if (closestInteractable != null)
        {
            DrawInteractableOption();
            if (Input.GetButtonDown("Interact"))
            {
                closestInteractable.Interact();
            }
        }
        else
        {
            interactImage.transform.position = new Vector3(-1000, -1000, -1000);
        }
    }

    /*
     * Indicates to the player that he can interact with an object
     */ 
    private void DrawInteractableOption()
    {
        
        Vector3 interactablePos = cam.WorldToScreenPoint(closestInteractable.Position);
        interactImage.transform.position = interactablePos;
        Lootable lootable = closestInteractable as Lootable;
        if (lootable != null)
        {
            interactImage.SetOptionText("loot");
            return;
        }
        Pickable pickable = closestInteractable as Pickable;
        if (pickable != null)
        {
            interactImage.SetOptionText("pick up");
        }
        DialogNPC talker = closestInteractable as DialogNPC;
        if (talker != null)
        {
            interactImage.SetOptionText("talk");
        }

    }

    /*
     * Finds the closest IInteractable object to the player
     */ 
    private void FindClosestInteractable()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, interactingRange);

        closestInteractable = null;
        
        foreach (Collider collider in colliders)
        {
            
            IInteractable interactable = collider.GetComponent<IInteractable>();

            if (interactable == null || !interactable.Active)
            {
                
                continue;
            }
            
            if (closestInteractable == null || Vector3.Distance(interactable.Position, transform.position) < Vector3.Distance(closestInteractable.Position, transform.position))
            {
                closestInteractable = interactable;
            }
        }
    }
}
