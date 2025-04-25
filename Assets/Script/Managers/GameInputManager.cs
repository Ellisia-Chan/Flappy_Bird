using EventSystem;
using EventSystem.Events;
using UnityEngine;

namespace SystemManagers {
    public class GameInputManager : MonoBehaviour {
        public static GameInputManager Instance { get; private set; }

        private InputSystem_Actions inputActions;

        private void Awake() {
            if (Instance == null) {
                Instance = this;
            } else {
                Debug.LogWarning("GameInputManager: There is already a GameInputManager in the scene, destroying this one.");
                Destroy(gameObject);
            }

            inputActions = new InputSystem_Actions();
        }

        private void OnEnable() {
            inputActions.Enable();
            inputActions.Player.Jump.performed += _ => EventBus.Publish(new PlayerInputEventJumpAction());
            inputActions.Player.Jump.canceled += _ => EventBus.Publish(new PlayerInputEventJumpAction());
        }

        private void OnDisable() {
            inputActions.Disable();
            inputActions.Player.Jump.performed -= _ => EventBus.Publish(new PlayerInputEventJumpAction());
            inputActions.Player.Jump.canceled -= _ => EventBus.Publish(new PlayerInputEventJumpAction());
        }
    } 
}
