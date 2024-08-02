using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterBrain : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;
    [SerializeField] private CharacterBody body;
    [SerializeField] private bool showKeyboardDebugMessages = false;
    [SerializeField] private bool showMouseDebugMessages = false;
    private bool canSkate = false;

    private void OnEnable()
    {
        inputReader.onMovement += HandleMovement;
        inputReader.onLook += HandleLook;
        inputReader.onJump += HandleJump;
        inputReader.onSprint += HandleSprint;
        inputReader.onSneak += HandleSneak;
        if (canSkate) inputReader.onSkate += HandleSkate;
    }

    private void OnDisable()
    {
        inputReader.onMovement -= HandleMovement;
        inputReader.onLook -= HandleLook;
        inputReader.onJump -= HandleJump;
        inputReader.onSprint -= HandleSprint;
        inputReader.onSneak -= HandleSneak;
        inputReader.onSkate -= HandleSkate;
    }

    private void HandleMovement(Vector2 movementInput, InputActionPhase phase)
    {
        Vector3 inputDirection = new Vector3(movementInput.x, 0, movementInput.y);
        if (phase == InputActionPhase.Performed)
        {
            if (showKeyboardDebugMessages) Debug.Log($"HandleMovement called with input: {movementInput} and phase: {phase}");
            body.Locomote(inputDirection.normalized);
        }
        if (phase == InputActionPhase.Canceled && inputDirection == Vector3.zero)
        {
            if (showKeyboardDebugMessages) Debug.Log("Movement canceled: Input returned to zero.");
            body.StopLocomotion();
        }
    }

    private void HandleLook(Vector2 lookInput)
    {
        if (showMouseDebugMessages)
        {
            Debug.Log($"HandleLook called with input: {lookInput}");
        }
    }

    private void HandleJump()
    {
        if (showKeyboardDebugMessages) Debug.Log("Jump action triggered.");
        body.Jump();
    }

    private void HandleSprint(InputActionPhase phase)
    {
        switch (phase)
        {
            case InputActionPhase.Performed:
                if (showKeyboardDebugMessages) Debug.Log("Sprint started.");
                break;
            case InputActionPhase.Canceled:
                if (showKeyboardDebugMessages) Debug.Log("Sprint canceled.");
                break;
        }
    }

    private void HandleSneak(InputActionPhase phase)
    {
        switch (phase)
        {
            case InputActionPhase.Performed:
                if (showKeyboardDebugMessages) Debug.Log("Sneak activated.");
                break;
            case InputActionPhase.Canceled:
                if (showKeyboardDebugMessages) Debug.Log("Sneak deactivated.");
                break;
        }
    }

    private void HandleSkate()
    {
        if (showKeyboardDebugMessages) Debug.Log("Skate action triggered.");
    }

    public void LearnToSkate()
    {
        if (!canSkate)
        {
            if (showKeyboardDebugMessages) Debug.Log("Learned to skate.");
            canSkate = true;
            inputReader.onSkate += HandleSkate;
        }
    }
}
