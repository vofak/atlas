using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
	This class holds all callbacks used on buttons in main menu and in credits scene.
*/
public class MenuButtons : MonoBehaviour {

	// Reference to loading panel in the main menu
    public GameObject loadingPanel;

	public void NewGame()
    {
        loadingPanel.SetActive(true);
        SceneManager.LoadSceneAsync(1);
    }

    public void LoadGame()
    {
        /*if (SaveLoad.LoadGame())
        {
            SaveLoad.ApplyLoaded();
        }*/
    }

    public void Credits()
    {
        SceneManager.LoadScene(2);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Back()
    {
        SceneManager.LoadScene(0);
    }

    private void Start()
    {
        Cursor.visible = true;
    }
}
