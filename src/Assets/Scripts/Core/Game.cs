using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
	Basic game session representation to be used to save and load the game.
*/
[System.Serializable]
public class Game {

    public Transform playerTransform;
    public QuestSystem questSystem;

    public Game(QuestSystem questSystem, Transform playerTransform)
    {
        this.playerTransform = playerTransform;
        this.questSystem = questSystem;
    }
}
