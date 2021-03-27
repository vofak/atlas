using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

/**
	Static functions to be used to save and load the game.
*/
public static class SaveLoad {

    public static Game loaded;

    public static void SaveGame()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player").transform.parent.gameObject;
        QuestSystem questSystem = QuestSystem.Instance;
        Transform playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        Game g = new Game(questSystem, playerTransform);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/savedGame.atlas"); //you can call it anything you want
        bf.Serialize(file, g);
        file.Close();
    }

    public static bool LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + "/savedGame.atlas"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/savedGame.atlas", FileMode.Open);
            loaded = (Game) bf.Deserialize(file);
            file.Close();
            return true;
        }
        else
        {
            return false;
        }
    }

    public static void ApplyLoaded()
    {

        SceneManager.LoadScene(1);

        GameObject player = GameObject.FindGameObjectWithTag("Player").transform.parent.gameObject;
        QuestSystem questSystem = QuestSystem.Instance;

        Transform playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        playerTransform.localPosition = loaded.playerTransform.localPosition;
        playerTransform.localRotation = loaded.playerTransform.localRotation;


        QuestSystem.Instance = loaded.questSystem;
        //GameObject.DestroyImmediate(player);
        //GameObject.Instantiate(loaded.player);
    }
}
