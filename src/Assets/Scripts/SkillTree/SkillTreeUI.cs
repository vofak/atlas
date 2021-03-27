using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * script handling GUI of skill tree
 */ 
public class SkillTreeUI : MonoBehaviour {
    
    public GameObject skillTreePanel;
    public Text availablePointsText;

    private SkillTree skillTree;

    private SkillUI[] skillUIs;

	// Use this for initialization
	void Start () {
        skillTree = GameObject.FindGameObjectWithTag("Player").GetComponent<SkillTree>();
        skillTree.onSkillUpdateCallback += UpdateUI;
        skillUIs = skillTreePanel.GetComponentsInChildren<SkillUI>();
        Hide();
    }
	
	// Update is called once per frame
	void Update () {

    }

    internal void UpgradeSkill(int pos)
    {
        skillTree.UpgradeSkill(pos);
    }

    public void Hide()
    {
        if (!skillTreePanel.activeSelf)
        {
            return;
        }
        skillTreePanel.SetActive(false);
        Atlas.Core.InputHandler.instance.ChangeState(Atlas.Core.InputHandler.StateUI.NOTHING);
    }

    public virtual void UpdateUI()
    {
        availablePointsText.text = skillTree.skillPoints.ToString();
        for (int i = 0; i < skillUIs.Length; i++)
        {
            skillUIs[i].SetSkill(skillTree.skills[i]);
        }
    }

    public void Show()
    {
        if (skillTreePanel.activeSelf)
        {
            return;
        }
        UpdateUI();
        skillTreePanel.SetActive(true);
        Atlas.Core.InputHandler.instance.ChangeState(Atlas.Core.InputHandler.StateUI.SKILL_TREE);
    }
}
