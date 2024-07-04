using _Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Movement
{
    public class ControllerBrain : MonoBehaviour
    {
        [SerializeField] private InputReader inputReader;
        [SerializeField] private CharacterBody body;

        private void Start()
        {
            if (!inputReader)
            {
                Debug.LogError($"{name}: {nameof(inputReader)} is null!");
                return;
            }
            inputReader.onMovementInput += HandleMovement;
        }

        private void HandleMovement(Vector2 movementInput, InputActionPhase phase)
        {
            Vector3 moveVector = new Vector3(movementInput.x, 0, movementInput.y);
            

            switch (phase)
            {
                case InputActionPhase.Canceled:
                    body.Decelerate();
                    break;
                // performed is called on input change, necesary for gamepads.
                case InputActionPhase.Performed:
                    body.Move(moveVector);
                    break;
                default:
                    break;
            }
        }
    }
}