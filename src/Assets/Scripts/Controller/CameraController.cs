using UnityEngine;

namespace Atlas.Core
{
	/**
		Part of the third person controller. Handles camera orbiting.
	*/
    public class CameraController : MonoBehaviour, IThirdPersonController
    {
        /* 
         * Interface properties
         */
        public bool IsFocused { get; set; }
        public Transform FocusedTarget { get; set; }
        public float Vertical { get; set; }
        public float Horizontal { get; set; }
        public float DeltaTime { get; set; }

        /*
         * Additional fields 
         */
        private Transform target;
        private Transform cameraTransform;
        private Transform pivotTranform;
        private Transform rotatorTransform;

        // Locomotion stats
        [SerializeField] private float moveSpeed = 5f;

        // Rotation stats
        private float lookAngle;
        private float tiltAngle;

        private float rotationSpeed = 3f;
        private float turnSmoothing = 0.1f;
        private float minimalAngle = -30f;
        private float maximalAngle = 35f;

        // Rotation smoothing parameters
        private float smoothX;
        private float smoothY;
        private float smoothXVelocity;
        private float smoothYVelocity;

        // Collison solving
        RaycastHit hitInfo;

        public void Init()
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
            cameraTransform = Camera.main.transform;
            pivotTranform = cameraTransform.parent;
            rotatorTransform = pivotTranform.parent;
        }

        public void Recompute()
        {

        }

        public void FixedRecompute()
        {
            FollowTarget();
            Rotate();
        }

        public void LateRecompute()
        {

        }

		/**
			Follows the player object in real time.
		*/
        void FollowTarget()
        {
            float speed = moveSpeed * DeltaTime;
            transform.position = Vector3.Lerp(transform.position, target.position, speed);

            if (Physics.Linecast(transform.position, pivotTranform.position, out hitInfo))
            {

                if (hitInfo.collider.CompareTag("Obstacle"))
                {
                    cameraTransform.position = hitInfo.point + (0.05f * hitInfo.normal);
                }
            }
            else
            {
                cameraTransform.position = Vector3.Lerp(cameraTransform.position, pivotTranform.position, speed);
            }

        }

		/**
			Orbits the camera according to the input.
		*/
        void Rotate()
        {
            smoothX = Mathf.SmoothDamp(smoothX, Horizontal, ref smoothXVelocity, turnSmoothing);
            smoothY = Mathf.SmoothDamp(smoothY, Vertical, ref smoothYVelocity, turnSmoothing);

            tiltAngle -= smoothY * rotationSpeed;
            tiltAngle = Mathf.Clamp(tiltAngle, minimalAngle, maximalAngle);
            rotatorTransform.localRotation = Quaternion.Euler(tiltAngle, 0, 0);

            if (FocusedTarget != null && IsFocused)
            {
                Vector3 directionTo = FocusedTarget.position - transform.position;
                directionTo.Normalize();

                if (directionTo == Vector3.zero)
                {
                    directionTo = transform.forward;
                }

                Quaternion rot = Quaternion.LookRotation(directionTo);
                transform.rotation = Quaternion.Slerp(transform.rotation, rot, DeltaTime * 9f);

                lookAngle = transform.rotation.eulerAngles.y;
            }
            else
            {
                FocusedTarget = null;
                lookAngle += smoothX * rotationSpeed;
                transform.rotation = Quaternion.Euler(0, lookAngle, 0);
            }
        }

    }
}