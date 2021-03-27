using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Atlas.Core
{
	/**
		Class for handling all input to the game from the player.
		It is implemented as a finite state machine to differentiate
		between normal gameplay and any UI activity that pauses the game.
	*/
    public class InputHandler : MonoBehaviour {

        public static InputHandler instance;
        public StateUI currentState;

        public static bool menuOn;
        public static bool dialogOn;

        public enum StateUI
        {
            ACHIEVEMENTS,
            SKILL_TREE,
            DIALOG,
            //JOURNAL,
            INVENTORY,
            MERCHANT,
            LOOT,
            MENU,

            NOTHING
        }


        public AchievementsUI achievements;
        public SkillTreeUI skillTree;
        public DialogUI dialog;
        public PlayerInventoryUI inventory;
        public MerchantInventoryUI merchant;
        public LootInventoryUI loot;
        public GameObject journal;
        public MenuUI menu;



        GameplayInputHandler gameplayInput;

        private void Awake()
        {
            instance = this;            
        }

	    // Use this for initialization
	    void Start () {
            gameplayInput = GetComponent<GameplayInputHandler>();
            gameplayInput.Init();

            achievements = GameObject.FindGameObjectWithTag("Achievements").GetComponent<AchievementsUI>();
            skillTree = GameObject.FindGameObjectWithTag("SkillTree").GetComponent<SkillTreeUI>();
            dialog = GameObject.FindGameObjectWithTag("Dialog").GetComponent<DialogUI>();
            inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<PlayerInventoryUI>();
            merchant = GameObject.FindGameObjectWithTag("Merchant").GetComponent<MerchantInventoryUI>();
            loot = GameObject.FindGameObjectWithTag("LootInventory").GetComponent<LootInventoryUI>();
            journal = GameObject.FindGameObjectWithTag("Journal");
            menu = GameObject.FindGameObjectWithTag("Menu").GetComponent<MenuUI>();
            
            ChangeState(StateUI.NOTHING);
        }
	
	    // Update is called once per frame
	    void Update () {
            

            if (currentState == StateUI.NOTHING)
            {
                Time.timeScale = 1f;
                Cursor.visible = false;
                if (!CheckForUIInput())
                {
                    gameplayInput.Recompute();
                }
            }
            else
            {
                Time.timeScale = 0f;
                Cursor.visible = true;
                HandleUIInput();
            }
        }

        public void ChangeState(StateUI state)
        {
            currentState = state;
        }

        void FixedUpdate()
        {
            if (currentState == StateUI.NOTHING)
            {
                gameplayInput.FixedRecompute();
            }
        }

        private void HandleUIInput() {

            switch (currentState)
            {
                case StateUI.ACHIEVEMENTS:
                    Achievements();
                    break;

                case StateUI.SKILL_TREE:
                    SkillTree();
                    break;

                case StateUI.DIALOG:
                    Dialog();
                    break;

                /*case StateUI.JOURNAL:
                    Journal();
                    break;*/

                case StateUI.INVENTORY:
                    Inventory();
                    break;

                case StateUI.MERCHANT:
                    Merchant();
                    break;

                case StateUI.LOOT:
                    Loot();
                    break;

                case StateUI.MENU:
                    Menu();
                    break;

                default:
                    break;
            }
        }

        private void Achievements()
        {
            if (ReadKey(KeyCode.Escape) || ReadKey(KeyCode.L))
            {
                achievements.Hide();
                //currentState = StateUI.NOTHING;
            }
        }

        private void SkillTree()
        {
            if (ReadKey(KeyCode.Escape) || ReadKey(KeyCode.K))
            {
                skillTree.Hide();
                //currentState = StateUI.NOTHING;
            }
        }

        private void Dialog()
        {
            if (ReadKey(KeyCode.Escape))
            {
                dialog.Hide();
                //currentState = StateUI.NOTHING;
            }
        }

        private void Inventory()
        {
            if (ReadKey(KeyCode.Escape) || ReadKey(KeyCode.I))
            {
                inventory.HideInventory();
                //currentState = StateUI.NOTHING;
            }
        }

        private void Merchant()
        {
            if (ReadKey(KeyCode.Escape))
            {
                merchant.HideInventory();
                //currentState = StateUI.NOTHING;
            }
        }

        private void Loot()
        {
            if (ReadKey(KeyCode.Escape))
            {
                loot.HideInventory();
                //currentState = StateUI.NOTHING;
            }
        }

        /*private void Journal()
        {
            if (ReadKey(KeyCode.Escape) || ReadKey(KeyCode.J))
            {
                // journal.Hide();
                currentState = StateUI.NOTHING;
            }
        }*/

        private void Menu()
        {
            if (ReadKey(KeyCode.Escape))
            {
                menu.Hide();
                
                //currentState = StateUI.NOTHING;
            }
        }

        private bool ReadKey(KeyCode keyCode)
        {
            return Input.GetKeyDown(keyCode);
        }

        private bool CheckForUIInput()
        {
            bool ret = false;

            if (dialogOn)
            {
                //currentState = StateUI.DIALOG;
                //ret = true;
            }
            else if (ReadKey(KeyCode.L))
            {
                achievements.Show();
                //currentState = StateUI.ACHIEVEMENTS;
                ret = true;
            }
            else if (ReadKey(KeyCode.K))
            {
                skillTree.Show();
                //currentState = StateUI.SKILL_TREE;
                ret = true;
            }
            /*else if (ReadKey(KeyCode.J))
            {
                //journal.Show();
                currentState = StateUI.JOURNAL;
                ret = true;
            }*/
            else if (ReadKey(KeyCode.I))
            {
                inventory.ShowInventory();
                //currentState = StateUI.INVENTORY;
                ret = true;
            }
            else if (ReadKey(KeyCode.Escape))
            {
                menu.Show();
                //currentState = StateUI.MENU;
                //menuOn = true;
                ret = true;
            }

            return ret;
        }
    }
}