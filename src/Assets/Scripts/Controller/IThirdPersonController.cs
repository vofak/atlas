using UnityEngine;

namespace Atlas.Core
{
	/**
		Obsolete controller interface.
		Still used by the player and the camera.
	*/
    public interface IThirdPersonController
    {

        bool IsFocused { get; set; }
        Transform FocusedTarget { get; set; }
        float Vertical { get; set; }
        float Horizontal { get; set; }
        float DeltaTime { get; set; }


        void Init();

        void Recompute();
        void FixedRecompute();
        void LateRecompute();
    }
}