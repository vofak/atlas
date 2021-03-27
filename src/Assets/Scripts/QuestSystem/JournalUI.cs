using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * script for showing  messages about leveling up, completend quests...
 */ 
public class JournalUI : MonoBehaviour {
    #region Singleton
    private static JournalUI instance;

    public static JournalUI Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindGameObjectWithTag("Journal").GetComponent<JournalUI>();
            }
            return instance;

        }
    }
    #endregion

    public Text text;

    LinkedList<string> messages;

    // Use this for initialization
    void Start () {
        messages = new LinkedList<string>();
        Events.Instance.onLevelUpCallback += LevelUpMessage;

        text = GetComponent<Text>();
        text.text = "";

        StartCoroutine("MyUpdate");
	}

    IEnumerator MyUpdate()
    {
        for (; ; )
        {
            yield return new WaitForSeconds(2);

            if (messages.Count > 0)
            {
                string msg = messages.First.Value;
                messages.RemoveFirst();
                text.text = msg;
            }
            else
            {
                text.text = "";
            }
        }
    }

    public void ShowMessage(string str)
    {
        messages.AddLast(str);
    }

    public void LevelUpMessage()
    {
        messages.AddLast("<b>Level up!</b>");
    }
}
