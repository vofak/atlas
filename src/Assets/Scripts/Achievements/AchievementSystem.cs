using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * singleton class handling achievements
 * 
 */ 
public class AchievementSystem : MonoBehaviour {

    #region Singleton
    private static AchievementSystem instance;

    public static AchievementSystem Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new AchievementSystem();
            }
            return instance;

        }
    }
    #endregion

    #region Fuj
    public Quest spiderBronze;
    public Quest spiderSilver;
    public Quest spiderGolden;

    public Quest trollBronze;
    public Quest trollSilver;
    public Quest trollGolden;

    public Quest skeletonBronze;
    public Quest skeletonSilver;
    public Quest skeletonGolden;

    public Quest killerBronze;
    public Quest killerSilver;
    public Quest killerGolden;

    public Quest hoarderBronze;
    public Quest hoarderSilver;
    public Quest hoarderGolden;

    public Quest veteranBronze;
    public Quest veteranSilver;
    public Quest veteranGolden;
    
    public Quest dragonGolden;


    /*
     * creates new instances of the scriptable objects
     */
    private void Start()
    {
        Quest newSpiderBronze = ScriptableObject.CreateInstance<Quest>();
        newSpiderBronze.Init(spiderBronze);
        spiderBronze = newSpiderBronze;
        Quest newSpiderSilver = ScriptableObject.CreateInstance<Quest>();
        newSpiderSilver.Init(spiderSilver);
        spiderSilver = newSpiderSilver;
        Quest newSpiderGolden = ScriptableObject.CreateInstance<Quest>();
        newSpiderGolden.Init(spiderGolden);
        spiderGolden = newSpiderGolden;

        Quest newtrollBronze = ScriptableObject.CreateInstance<Quest>();
        newtrollBronze.Init(trollBronze);
        trollBronze = newtrollBronze;
        Quest newtrollSilver = ScriptableObject.CreateInstance<Quest>();
        newtrollSilver.Init(trollSilver);
        trollSilver = newtrollSilver;
        Quest newtrollGolden = ScriptableObject.CreateInstance<Quest>();
        newtrollGolden.Init(trollGolden);
        trollGolden = newtrollGolden;

        Quest newskeletonBronze = ScriptableObject.CreateInstance<Quest>();
        newskeletonBronze.Init(skeletonBronze);
        skeletonBronze = newskeletonBronze;
        Quest newskeletonSilver = ScriptableObject.CreateInstance<Quest>();
        newskeletonSilver.Init(skeletonSilver);
        skeletonSilver = newskeletonSilver;
        Quest newskeletonGolden = ScriptableObject.CreateInstance<Quest>();
        newskeletonGolden.Init(skeletonGolden);
        skeletonGolden = newskeletonGolden;

        Quest newkillerBronze = ScriptableObject.CreateInstance<Quest>();
        newkillerBronze.Init(killerBronze);
        killerBronze = newkillerBronze;
        Quest newkillerSilver = ScriptableObject.CreateInstance<Quest>();
        newkillerSilver.Init(killerSilver);
        killerSilver = newkillerSilver;
        Quest newkillerGolden = ScriptableObject.CreateInstance<Quest>();
        newkillerGolden.Init(killerGolden);
        killerGolden = newkillerGolden;

        Quest newhoarderBronze = ScriptableObject.CreateInstance<Quest>();
        newhoarderBronze.Init(hoarderBronze);
        hoarderBronze = newhoarderBronze;
        Quest newhoarderSilver = ScriptableObject.CreateInstance<Quest>();
        newhoarderSilver.Init(hoarderSilver);
        hoarderSilver = newhoarderSilver;
        Quest newhoarderGolden = ScriptableObject.CreateInstance<Quest>();
        newhoarderGolden.Init(hoarderGolden);
        hoarderGolden = newhoarderGolden;

        Quest newveteranBronze = ScriptableObject.CreateInstance<Quest>();
        newveteranBronze.Init(veteranBronze);
        veteranBronze = newveteranBronze;
        Quest newveteranSilver = ScriptableObject.CreateInstance<Quest>();
        newveteranSilver.Init(veteranSilver);
        veteranSilver = newveteranSilver;
        Quest newveteranGolden = ScriptableObject.CreateInstance<Quest>();
        newveteranGolden.Init(veteranGolden);
        veteranGolden = newveteranGolden;

        Quest newdragonGolden = ScriptableObject.CreateInstance<Quest>();
        newdragonGolden.Init(dragonGolden);
        dragonGolden = newdragonGolden;

        Events.Instance.onQuestCompletedCallback += QuestCompleted;
    }

    
    /*
     * Called whenever a quest is completed. If it's an achievement, it shows the player that he has achieved something
     * 
     */ 
    public void QuestCompleted(Quest q)
    {
        
        if(q == spiderBronze)
        {
            JournalUI.Instance.ShowMessage("Achievement: " + spiderBronze.questName);
        }
        else if (q == spiderSilver)
        {
            JournalUI.Instance.ShowMessage("Achievement: " + spiderSilver.questName);
        }
        else if(q == spiderGolden)
        {
            JournalUI.Instance.ShowMessage("Achievement: " + spiderGolden.questName);
        }
        else if (q == trollBronze)
        {
            JournalUI.Instance.ShowMessage("Achievement: " + trollBronze.questName);
        }
        else if(q == trollSilver)
        {
            JournalUI.Instance.ShowMessage("Achievement: " + trollSilver.questName);
        }
        else if (q == trollGolden)
        {
            JournalUI.Instance.ShowMessage("Achievement: " + trollGolden.questName);
        }
        else if(q == skeletonBronze)
        {
            JournalUI.Instance.ShowMessage("Achievement: " + skeletonBronze.questName);
        }
        else if (q == skeletonSilver)
        {
            JournalUI.Instance.ShowMessage("Achievement: " + skeletonSilver.questName);
        }
        else if(q == skeletonGolden)
        {
            JournalUI.Instance.ShowMessage("Achievement: " + skeletonGolden.questName);
        }
        else if (q == killerBronze)
        {
            JournalUI.Instance.ShowMessage("Achievement: " + killerBronze.questName);
        }
        else if (q == killerSilver)
        {
            JournalUI.Instance.ShowMessage("Achievement: " + killerSilver.questName);
        }
        else if (q == killerGolden)
        {
            JournalUI.Instance.ShowMessage("Achievement: " + killerGolden.questName);
        }
        else if (q == hoarderBronze)
        {
            JournalUI.Instance.ShowMessage("Achievement: " + hoarderBronze.questName);
        }
        else if (q == hoarderSilver)
        {
            JournalUI.Instance.ShowMessage("Achievement: " + hoarderSilver.questName);
        }
        else if (q == hoarderGolden)
        {
            JournalUI.Instance.ShowMessage("Achievement: " + hoarderGolden.questName);
        }
        else if (q == veteranBronze)
        {
            JournalUI.Instance.ShowMessage("Achievement: " + veteranBronze.questName);
        }
        else if (q == veteranSilver)
        {
            JournalUI.Instance.ShowMessage("Achievement: " + veteranSilver.questName);
        }
        else if (q == veteranGolden)
        {
            JournalUI.Instance.ShowMessage("Achievement: " + veteranGolden.questName);
        }
        else if (q == dragonGolden)
        {
            JournalUI.Instance.ShowMessage("Achievement: " + dragonGolden.questName);
        }
    }

#endregion

}
