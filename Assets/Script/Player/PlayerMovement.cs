using UnityEngine;
using EventSystem;
using EventSystem.Events;
using System.Collections;

namespace PlayerSystem {
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour {

        [Header("Jump Settings")]
        [SerializeField] private float jumpForce = 3f;

        [Header("Fall Settings")]
        [SerializeField] private float maxFallSpeed = -5f;

        private Rigidbody2D rb;

        private void Awake() {
            rb = GetComponent<Rigidbody2D>();
        }

        private void OnEnable() {
            EventBus.Subscribe<PlayerInputEventJumpAction>(e => HandleJump());
        }

        private void OnDisable() {
            EventBus.Unsubscribe<PlayerInputEventJumpAction>(e => HandleJump());
        }

        private void FixedUpdate() {
            HandleFallSpeed();
        }

        private void HandleJump() {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        private void HandleFallSpeed() {
            if (rb.linearVelocity.y < maxFallSpeed) {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, maxFallSpeed);
            }
        }
    }
}
