using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Input
{
    public class InputReader : MonoBehaviour
    {
        public event Action<Vector2, InputActionPhase> onMovementInput = delegate { };

        public void HandleMovementInput(InputAction.CallbackContext ctx)
        {
            onMovementInput.Invoke(ctx.ReadValue<Vector2>(), ctx.phase);
        }
    }
}

