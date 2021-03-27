using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Atlas.Core { 

	/**
		This class handles input from the user on gameplay, i.e. when no UI is active.
		This class closely communicates with player controller and camera controller.
		They both need to react to any input on gameplay. 
	*/
    public class GameplayInputHandler : MonoBehaviour
    {
        private float vertical;
        private float horizontal;

        private bool sprint;
        private bool roll;
        private bool leftMouse;
        private bool rightMouse;
        private bool focus;
        private bool magicAttack;

        private string[] keyboardAxes = new string[] { "Vertical", "Horizontal" };
        private string[] mouseAxes = new string[] { "Mouse Y", "Mouse X" };

        private PlayerController animController;
        private CameraController camController;

        // Use this for initialization
        public void /*Start()*/ Init()
        {
            animController = GetComponentInChildren<PlayerController>();
            camController = GetComponentInChildren<CameraController>();

            animController.Init();
            camController.Init();
        }

        // Update is called once per frame
        public void /*Update()*/ Recompute() 
        {
            if (animController.focused == null || !animController.focused.enabled)
            {
                animController.IsFocused = camController.IsFocused = false;
                animController.FocusedTarget = camController.FocusedTarget = null;
                animController.focused = null;
            }

            AnimateCharacter();

            CheckScrolling();
        }

        private void CheckScrolling()
        {
            if(animController.focused == null)
            {
                return;
            }

            float scrolling = Input.GetAxis("Mouse ScrollWheel");

            if (scrolling > 0)
            {
                Atlas.Combat.FocusTarget newFocus = Combat.Combat.getNextFocusTarget();
                if (newFocus == null)
                {
                    animController.focused.HideHealth();
                }
                animController.focused = newFocus;
            }
            else if (scrolling < 0)
            {
                Atlas.Combat.FocusTarget newFocus = Combat.Combat.getPreviousFocusTarget();
                if (newFocus == null)
                {
                    animController.focused.HideHealth();
                }
                animController.focused = newFocus;
            }
        }

        public void /*FixedUpdate()*/ FixedRecompute()
        {
            RotateCamera();

            animController.FixedRecompute();
        }

		/**
			Turns of player tracking focus target.
		*/
        private void DisableFocus()
        {
            animController.IsFocused = camController.IsFocused = false;
            animController.FocusedTarget = camController.FocusedTarget = null;

            animController.focused.HideHealth();
            animController.focused = null;
        }
      
        void GetAxesInput(ref string[] axes)
        {
            vertical = Input.GetAxis(axes[0]);
            horizontal = Input.GetAxis(axes[1]);
        }

        void GetSpecialInput()
        {
            sprint = Input.GetKey(KeyCode.LeftShift);
            focus = Input.GetKeyDown(KeyCode.F);
            roll = Input.GetKeyDown(KeyCode.Space);

            leftMouse = Input.GetMouseButtonDown(0);
            rightMouse = Input.GetMouseButton(1);
            magicAttack = Input.GetKeyDown(KeyCode.Q);

        }

		/**
			Animates main character based on input.
		*/
        private void AnimateCharacter()
        {
            GetAxesInput(ref keyboardAxes);
            GetSpecialInput();

            animController.Vertical = vertical;
            animController.Horizontal = horizontal;
            animController.DeltaTime = Time.deltaTime;
            animController.IsSprinting = sprint;
            animController.IsRolling = roll;

            animController.LeftMouseClicked = leftMouse;
            animController.RightMouseClicked = rightMouse;
            animController.magicAttack = magicAttack;

            Vector3 v = camController.transform.forward * vertical;
            Vector3 h = camController.transform.right * horizontal;
            animController.MoveDirection = (v + h).normalized;
            animController.MoveAmount = Mathf.Clamp01(Mathf.Abs(vertical) + Mathf.Abs(horizontal));

            if (focus)
            {
                animController.IsFocused = !animController.IsFocused;

                if (animController.IsFocused)
                {
                    
                    Atlas.Combat.FocusTarget newFocus = Combat.Combat.getNextFocusTarget();
                    if(newFocus == null)
                    {
                        animController.IsFocused = false;

                    } else
                    {
                        animController.focused = newFocus;
                        animController.FocusedTarget = animController.focused.target;
                    }
                }
                else if (animController.focused != null)
                {
                    animController.focused.HideHealth();
                    animController.focused = null;
                }

            }

            animController.Recompute();
        }

		/**
			Rotates camera based on mouse input.
		*/
        private void RotateCamera()
        {
            GetAxesInput(ref mouseAxes);
            GetSpecialInput();

            camController.Vertical = vertical;
            camController.Horizontal = horizontal;
            camController.DeltaTime = Time.fixedDeltaTime;

            camController.IsFocused = animController.IsFocused;

            if (camController.IsFocused)
            {
                camController.FocusedTarget = animController.focused.target;
            }
            else
            {
                camController.FocusedTarget = null;
            }

            camController.FixedRecompute();

        }
    }
}