using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * class handling achievements GUI
 * 
 */
public class AchievementsUI : MonoBehaviour {

    public Text hoarderProgress;
    public Text dragonProgress;
    public Text skeletonProgress;
    public Text trollProgress;
    public Text spiderProgress;
    public Text veteranProgress;
    public Text killerProgress;

    public Image hoarderBadge;
    public Image dragonBadge;
    public Image skeletonBadge;
    public Image trollBadge;
    public Image spiderBadge;
    public Image veteranBadge;
    public Image killerBadge;

    public Image hoarderPicture;
    public Image dragonPicture;
    public Image skeletonPicture;
    public Image trollPicture;
    public Image spiderPicture;
    public Image veteranPicture;
    public Image killerPicture;

    public GameObject panel;

    private AchievementSystem achievementSystem;
    private Color bronze = new Color(0.8f, 0.5f, 0.2f);
    private Color silver = new Color(1.0f, 1.0f, 1.0f);
    private Color gold = new Color(1.0f, 1.0f, 0.0f);
    private Color grey = new Color(0.5f,0.5f,0.5f,0.3f);


    // Use this for initialization
    void Start () {
        achievementSystem = GameObject.FindGameObjectWithTag("Player").GetComponent<AchievementSystem>();
        Hide();
	}
	
	// Update is called once per frame
	void Update () {

    }

    // hides the panel
    public void Hide()
    {
        if (!panel.activeSelf)
        {
            return;
        }
        panel.SetActive(false);
        Atlas.Core.InputHandler.instance.ChangeState(Atlas.Core.InputHandler.StateUI.NOTHING);
    }

    /*
     * Updates a single picture representing one achievement depending on the achieved level of the achievement
     * 
     */ 
    public void UpdatePart(Quest bronzeQuest, Quest silverQuest, Quest goldenQuest, Image badge, Text text, Image picture)
    {
        if (bronzeQuest.completed)
        {
            if (silverQuest.completed)
            {
                if (goldenQuest.completed)
                {
                    text.text = goldenQuest.goals[0].requiredAmount.ToString() + "/" + goldenQuest.goals[0].requiredAmount.ToString();
                    badge.color = gold;
                }
                else
                {
                    text.text = goldenQuest.goals[0].completedAmount.ToString() + "/" + goldenQuest.goals[0].requiredAmount.ToString();
                    badge.color = silver;
                }
            }
            else
            {
                badge.color = bronze;
                text.text = silverQuest.goals[0].completedAmount.ToString() + "/" + silverQuest.goals[0].requiredAmount.ToString();
            }
            picture.color = new Color(1f, 1f, 1f, 1f);
        }
        else
        {
            badge.color = grey;
            picture.color = grey;
            text.text = bronzeQuest.goals[0].completedAmount.ToString() + "/" + bronzeQuest.goals[0].requiredAmount.ToString();
        }
    }

    /*
     * Updates all the pictures representing achievements depending on the achieved level of the achievement
     */
    public void UpdateUI()
    {
        UpdatePart(achievementSystem.hoarderBronze, achievementSystem.hoarderSilver, achievementSystem.hoarderGolden, hoarderBadge, hoarderProgress, hoarderPicture);
        UpdatePart(achievementSystem.skeletonBronze, achievementSystem.skeletonSilver, achievementSystem.skeletonGolden, skeletonBadge, skeletonProgress, skeletonPicture);
        UpdatePart(achievementSystem.trollBronze, achievementSystem.trollSilver, achievementSystem.trollGolden, trollBadge, trollProgress, trollPicture);
        UpdatePart(achievementSystem.spiderBronze, achievementSystem.spiderSilver, achievementSystem.spiderGolden, spiderBadge, spiderProgress, spiderPicture);
        UpdatePart(achievementSystem.veteranBronze, achievementSystem.veteranSilver, achievementSystem.veteranGolden, veteranBadge, veteranProgress, veteranPicture);
        UpdatePart(achievementSystem.killerBronze, achievementSystem.killerSilver, achievementSystem.killerGolden, killerBadge, killerProgress, killerPicture);

        if (achievementSystem.dragonGolden.completed)
        {
            dragonPicture.color = new Color(1f, 1f, 1f);
            dragonBadge.color = gold;
            dragonProgress.text = "1/1";
        } else
        {
            dragonPicture.color = grey;
            dragonBadge.color = grey;
            dragonProgress.text = "0/1";
        }
    }

    /*
     * shows the panel
     */ 
    public void Show()
    {
        if (panel.activeSelf)
        {
            return;
        }
        UpdateUI();
        panel.SetActive(true);
        Atlas.Core.InputHandler.instance.ChangeState(Atlas.Core.InputHandler.StateUI.ACHIEVEMENTS);
    }


}
