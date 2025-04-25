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

        private void Awake() {
            rb = GetComponent<Rigidbody2D>();
            rb.gravityScale = 0;
        }

        private void OnEnable() {
            EventBus.Subscribe<PlayerInputEventJumpAction>(e => HandleJump());
            EventBus.Subscribe<GameEventStateChange>(e => HandleJumpState(e.newState));
        }

        private void OnDisable() {
            EventBus.Unsubscribe<PlayerInputEventJumpAction>(e => HandleJump());
            EventBus.Unsubscribe<GameEventStateChange>(e => HandleJumpState(e.newState));
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
