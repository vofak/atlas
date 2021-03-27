using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Singleton class that is called whenever something important happens and other classes can subscribe toits delegates
 */ 
public class Events {

    #region Singleton
    private static Events instance;

    private Events() { }

    public static Events Instance {
        get
        {
            if(instance == null)
            {
                instance = new Events();
            }
            return instance;
        }
    }
    #endregion



    public delegate void OnEnemyDied(string enemyName);
    public OnEnemyDied onEnemyDiedCallback;

    public void EnemyDied(string enemyName)
    {
        Debug.Log("something called " + enemyName + " died");
        if(onEnemyDiedCallback != null)
        {
            onEnemyDiedCallback.Invoke(enemyName);

        }
    }

    public delegate void OnLevelUp();
    public OnLevelUp onLevelUpCallback;

    public void LevelUp()
    {
        if(onLevelUpCallback != null)
        {
            onLevelUpCallback.Invoke();
        }
    }

    public delegate void OnQuestCompleted(Quest q);
    public OnQuestCompleted onQuestCompletedCallback;

    public void QuestCompleted(Quest q)
    {
        Debug.Log("quest " + q.questName + " completed");
        if(onQuestCompletedCallback != null)
        {
            onQuestCompletedCallback.Invoke(q);

        }
        
    }

}
