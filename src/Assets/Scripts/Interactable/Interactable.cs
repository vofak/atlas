using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Interface for all the objects in the world that the player can interact with
 */
public interface IInteractable{
    bool Active { get; set; }
    Vector3 Position { get;  }

    void Interact();
}
