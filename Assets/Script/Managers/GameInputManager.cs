using EventSystem;
using EventSystem.Events;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SystemManagers {
    public class GameInputManager : MonoBehaviour {
        public static GameInputManager Instance { get; private set; }

        private System.Action<InputAction.CallbackContext> OnJumpPerformed;
        private System.Action<InputAction.CallbackContext> OnJumpCanceled;

        private InputSystem_Actions inputActions;

        private void Awake() {
            if (Instance != null) {
                Debug.LogWarning("GameInputManager: There is already a GameInputManager in the scene, destroying this one.");
                Destroy(gameObject);
                return;
            }

            Instance = this;
            inputActions = new InputSystem_Actions();

            OnJumpPerformed = ctx => EventBus.Publish(new PlayerInputEventJumpAction());
            OnJumpCanceled = ctx => EventBus.Publish(new PlayerInputEventJumpAction());
        }

        private void OnEnable() {
            if (Instance != this) return;

            inputActions.Enable();
            inputActions.Player.Jump.performed += OnJumpPerformed;
            inputActions.Player.Jump.canceled += OnJumpCanceled;
        }

        private void OnDisable() {
            if (Instance != this) return;

            inputActions.Disable();
            inputActions.Player.Jump.performed -= OnJumpPerformed;
            inputActions.Player.Jump.canceled -= OnJumpCanceled;
        }
    }
}
