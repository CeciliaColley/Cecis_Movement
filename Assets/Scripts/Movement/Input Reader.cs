using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    [SerializeField] private bool showKeyboardDebugMessages = false;
    [SerializeField] private bool showMouseDebugMessages = false;

    public event Action<Vector2, InputActionPhase> onMovement = delegate { };

    public event Action<Vector2> onLook = delegate { };

    public event Action onJump = delegate { };

    public event Action<InputActionPhase> onSprint = delegate { };

    public event Action<InputActionPhase> onSneak = delegate { };

    public event Action onSkate = delegate { };

    public void Move(InputAction.CallbackContext ctx)
    {
        if (showKeyboardDebugMessages) Debug.Log("Move action triggered with value: " + ctx.ReadValue<Vector2>() + " and phase: " + ctx.phase);
        onMovement.Invoke(ctx.ReadValue<Vector2>(), ctx.phase);
    }
    public void Look(InputAction.CallbackContext ctx)
    {
        if (showMouseDebugMessages) Debug.Log("Look action triggered with value: " + ctx.ReadValue<Vector2>());
        onLook.Invoke(ctx.ReadValue<Vector2>());
    }
    public void Jump(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if (showKeyboardDebugMessages) Debug.Log("Jump action triggered with phase: " + ctx.phase);
            onJump.Invoke();
        }
    }
    public void Sprint(InputAction.CallbackContext ctx)
    {
        if (showKeyboardDebugMessages) Debug.Log("Sprint action triggered with phase: " + ctx.phase);
        onSprint.Invoke(ctx.phase);
    }
    public void Sneak(InputAction.CallbackContext ctx)
    {
        if (showKeyboardDebugMessages) Debug.Log("Sneak action triggered with phase: " + ctx.phase);
        onSneak.Invoke(ctx.phase);
    }
    public void Skate(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if (showKeyboardDebugMessages) Debug.Log("Skate action triggered with phase: " + ctx.phase);
            onSkate.Invoke();
        }
    }
}

