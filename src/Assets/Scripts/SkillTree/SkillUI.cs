using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/*
 * script handling the image of a skill as a button
 */ 
public class SkillUI : MonoBehaviour, IPointerClickHandler
{
    public int pos;
    public SkillTreeUI skillTreeUI;
    public Text text;
    public Image image;
    private int level;
    private int maxLevel;

    public virtual void OnLeftClick()
    {
        skillTreeUI.UpgradeSkill(pos);
    }

    public virtual void OnRightClick()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick();
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClick();
        }
    }

    /*
     * sets the image and background depending on the skill and its level
     */ 
    public void SetSkill(AcquiredSkill skill)
    {
        level = skill.currLevel;
        maxLevel = skill.skill.maxLevel;

        text.text = level.ToString() + "/" + maxLevel.ToString();
        if(level > 0)
        {
            image.color = new Color(1f, 1f, 1f, 1f);
        } else
        {
            image.color = new Color(0.5f, 0.5f, 0.5f, 0.3f);
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


}
