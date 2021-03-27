using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * sscript handling buttons in the ingame menu
 */ 
public class MenuUI : MonoBehaviour {

    public GameObject panel;

    public void ExitGame()
    {
        SceneManager.LoadScene(0);
        Cursor.visible = true;
    }

    public void Resume()
    {
        Hide();
    }

    /*
     * not implemented
     */
    public void SaveGame()
    {
        /*SaveLoad.SaveGame();*/
    }

    /*
     * not implemented
     */
    public void LoadGame()
    {
        /*if (SaveLoad.LoadGame())
        {
            SaveLoad.ApplyLoaded();
        }*/
    }

    /*
     * starts a new game
     */ 
    public void NewGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Hide()
    {
        if (panel.activeSelf)
        {
            panel.SetActive(false);
            Atlas.Core.InputHandler.instance.ChangeState(Atlas.Core.InputHandler.StateUI.NOTHING);
        } 
    }

    public void Show()
    {
        if (!panel.activeSelf)
        {
            panel.SetActive(true);
            Atlas.Core.InputHandler.instance.ChangeState(Atlas.Core.InputHandler.StateUI.MENU);
        }
    }

	// Use this for initialization
	void Start () {
        Hide();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
