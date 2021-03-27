using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * class handling dialog gui
 */ 
public class DialogUI : MonoBehaviour {

    [SerializeField]
    private GameObject dialogPanel;
    [SerializeField]
    private Text text;
    [SerializeField]
    private Transform responses;
    [SerializeField]
    private Text talkerName;
    [SerializeField]
    private ResponseUI responsePrefab;


    // Use this for initialization
    void Start () {
        Hide();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /*
     * hides the panel
     */ 
    public void Hide()
    {
        if (!dialogPanel.activeSelf)
        {
            return;
        }
        dialogPanel.SetActive(false);
        Atlas.Core.InputHandler.instance.ChangeState(Atlas.Core.InputHandler.StateUI.NOTHING);
        
    }

    /*
     * shows the panel
     */ 
    public void Show()
    {
        if (dialogPanel.activeSelf)
        {
            return;
        }
        dialogPanel.SetActive(true);
        Atlas.Core.InputHandler.instance.ChangeState(Atlas.Core.InputHandler.StateUI.DIALOG);
        
    }

    public void SetTalkerName(string talkerName)
    {
        this.talkerName.text = talkerName;
    }

    /*
     * Sets the text to the text of the exchange and creates buttons with text of possible responses
     */
    public void SetDialogExchange(DialogExchange dialogExchange)
    {
        if(text == null)
        {
            return;
        }
        text.text = dialogExchange.NPCsays;
        ResponseUI[] rs = responses.GetComponentsInChildren<ResponseUI>();
        foreach(ResponseUI r in rs)
        {
            DestroyImmediate(r.gameObject);
        }
        
        for(int i = 0; i < dialogExchange.responses.Count; ++i)
        {
            ResponseUI nr = Instantiate<ResponseUI>(responsePrefab, responses);
            nr.Init(i, dialogExchange.responses[i].text);
        }
    }

    /*
     * On click handler of the cross button. Tells dialog system that the current dialog ended
     */ 
    public void OnEndClicked()
    {
        DialogSystem.Instance.EndDialog();
    }
}
