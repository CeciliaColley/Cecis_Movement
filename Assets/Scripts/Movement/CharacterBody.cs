using UnityEngine;

namespace Movement
{
    [RequireComponent(typeof(Rigidbody))]
    public class CharacterBody : MonoBehaviour
    {
        [SerializeField] private float maxSprintSpeed = 7.0f;
        [SerializeField] private float maxSpeed = 5.0f;
        [Tooltip("How many seconds it takes to reach Max Speed")]
        [SerializeField] private float acceleration = 5.0f;
        [Tooltip("How many seconds it takes to go from Max Speed to 0")]
        [SerializeField] private float deceleration = 5.0f;

        private Rigidbody rb;
        private Displacement displacement;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            if (rb == null)
            {
                Debug.LogError("Rigidbody component not found!");
                return;
            }

            displacement = new Displacement(rb, acceleration, deceleration, this);
        }

        private void FixedUpdate()
        {
            rb.velocity = displacement.CurrentMovement * Mathf.Lerp(0, maxSpeed, displacement.SpeedLerpValue);
        }

        private void OnValidate()
        {
            if (displacement != null)
            {
                displacement.SetAcceleration(acceleration);
                displacement.SetDeceleration(deceleration);
            }
        }

        public void Move(Vector3 movementVector)
        {
            displacement.Move(movementVector);
        }

        public void Accelerate()
        {
            displacement.Accelerate();
        }

        public void Decelerate()
        {
            displacement.Decelerate();
        }
    }
}
