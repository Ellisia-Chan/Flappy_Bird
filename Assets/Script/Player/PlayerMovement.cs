using System;
using UnityEngine;
using EventSystem;
using EventSystem.Events;
using SystemManagers;


namespace PlayerSystem {
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour {

        [Header("Jump Settings")]
        [SerializeField] private float jumpForce = 3f;

        [Header("Fall Settings")]
        [SerializeField] private float maxFallSpeed = -5f;

        private Rigidbody2D rb;
        private bool canJump = false;

        private Action<PlayerInputEventJumpAction> _jumpCallback;
        private Action<GameEventStateChange> _stateChangeCallback;

        private void Awake() {
            rb = GetComponent<Rigidbody2D>();
            rb.gravityScale = 0;

            _jumpCallback = e => HandleJump();
            _stateChangeCallback = e => HandleJumpState(e.newState);
        }

        private void OnEnable() {
            EventBus.Subscribe(_jumpCallback);
            EventBus.Subscribe(_stateChangeCallback);
        }

        private void OnDisable() {
            EventBus.Unsubscribe(_jumpCallback);
            EventBus.Unsubscribe(_stateChangeCallback);
        }

        private void FixedUpdate() {
            HandleFallSpeed();
        }

        private void HandleJump() {
            if (canJump) {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce); 
            }
        }

        private void HandleFallSpeed() {
            if (rb.linearVelocity.y < maxFallSpeed) {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, maxFallSpeed);
            }
        }

        private void HandleJumpState(GameManager.GameState state) {
            if (state == GameManager.GameState.PLAYING) {
                canJump = true;
                rb.gravityScale = 1;
            } else {
                canJump = false;
            }
        }
    }
}
