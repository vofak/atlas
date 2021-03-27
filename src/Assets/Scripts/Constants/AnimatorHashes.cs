using UnityEngine;

namespace Atlas.Core
{

	/**
		Precomputed animator hashes for all animations and parameters that are used
		throughout the project.
	*/
    public static class AnimatorHashes
    {

		/**
			Hashes for the Human animator.
		*/
        public static class Human
        {

			/**
				Hashes for the animator parameters.
			*/
            public static class Param
            {
                public static int vertical = Animator.StringToHash("vertical");
                public static int horizontal = Animator.StringToHash("horizontal");

                public static int canMove = Animator.StringToHash("canMove");
                public static int hasShield = Animator.StringToHash("hasShield");
                public static int isArmed = Animator.StringToHash("isArmed");
                public static int isFocused = Animator.StringToHash("isFocused");
                public static int isGrounded = Animator.StringToHash("isGrounded");
                public static int isParrying = Animator.StringToHash("isParrying");
                public static int isRunning = Animator.StringToHash("isRunning");
                public static int isDead = Animator.StringToHash("isDead");
            }

			/**
				Hashes for specific animations used within the animator.
			*/
            public static class Animation
            {
                public static int fall = Animator.StringToHash("Humanoid|Fall");
                public static int rollsBlend = Animator.StringToHash("Humanoid|Rolls");

                public static int attack01 = Animator.StringToHash("Humanoid|Attack01");
                public static int attack02 = Animator.StringToHash("Humanoid|Attack02");
                public static int spellcast = Animator.StringToHash("Humanoid|Spellcast");
                public static int damaged = Animator.StringToHash("Humanoid|Damaged");
                public static int parried = Animator.StringToHash("Humanoid|Parried");

                public static int die = Animator.StringToHash("Die");
            }
        }

		/**
			Hashes for the Dragon animator.
		*/
        public static class Dragon
        {
			/**
				Hashes for the animator parameters.
			*/
            public static class Param
            {
                public static int speed = Animator.StringToHash("speed");
                public static int canMove = Human.Param.canMove;
                public static int isFlying = Animator.StringToHash("isFlying");
                public static int isDead = Animator.StringToHash("isDead");
                public static int hasMoved = Animator.StringToHash("hasMoved");
            }
			
			/**
				Hashes for specific animations used within the animator.
			*/
            public static class Animation
            {
                public static int attack01 = Animator.StringToHash("Dragon|Attack01");
                public static int attack02 = Animator.StringToHash("Dragon|Attack02");
                public static int die = Animator.StringToHash("Dragon|Die");
            }
        }

		/**
			Hashes for the Spider animator.
		*/
        public static class Spider
        {
			/**
				Hashes for the animator parameters.
			*/
            public static class Param
            {
                public static int speed = Dragon.Param.speed;
                public static int canMove = Dragon.Param.canMove;
                public static int isDead = Dragon.Param.isDead;
            }
			
			/**
				Hashes for specific animations used within the animator.
			*/
            public static class Animation
            {
                public static int attack01 = Animator.StringToHash("Spider|Attack01");
                public static int attack02 = Animator.StringToHash("Spider|Attack02");
                public static int damaged = Animator.StringToHash("Spider|Damaged");
                public static int die = Animator.StringToHash("Spider|Die");
            }
        }

		/**
			Hashes for the Skeleton animator.
		*/
        public static class Skeleton
        {
			/**
				Hashes for the animator parameters.
			*/
            public static class Param
            {
                public static int speed = Dragon.Param.speed;
                public static int canMove = Dragon.Param.canMove;
                public static int isDead = Dragon.Param.isDead;
            }

			/**
				Hashes for specific animations used within the animator.
			*/
            public static class Animation
            {
                public static int attack01 = Animator.StringToHash("Skeleton|Attack01");
                public static int attack02 = Animator.StringToHash("Skeleton|Attack02");
                public static int damaged = Animator.StringToHash("Skeleton|Damaged");
                public static int die = Animator.StringToHash("Skeleton|Die");
            }
        }

		/**
			Hashes for the Troll animator.
		*/
        public static class Troll
        {
			/**
				Hashes for the animator parameters.
			*/
            public static class Param
            {
                public static int speed = Dragon.Param.speed;
                public static int canMove = Dragon.Param.canMove;
                public static int isMoving = Animator.StringToHash("isMoving");
                public static int isDead = Dragon.Param.isDead;
            }

			/**
				Hashes for specific animations used within the animator.
			*/
            public static class Animation
            {
                public static int attack01 = Animator.StringToHash("Troll|Attack01");
                public static int attack02 = Animator.StringToHash("Troll|Attack02");
                public static int damaged = Animator.StringToHash("Troll|Damaged");
                public static int die = Animator.StringToHash("Troll|Die");
            }
        }

    }
}