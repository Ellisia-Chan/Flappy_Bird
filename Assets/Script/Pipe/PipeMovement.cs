using UnityEngine;
using SystemManagers;

namespace PipeObstacle {
    [RequireComponent(typeof(Rigidbody2D))]
    public class PipeMovement : MonoBehaviour {
        [Header("Movement")]
        [SerializeField] private float decelerationSpeed = 1f;

        private Rigidbody2D rb;
        private bool canMove = false;
        private float moveSpeed;

        private void Awake() {
            rb = GetComponent<Rigidbody2D>();
            moveSpeed = GameManager.Instance.GetPipeMovementSpeed();
        }

        private void Update() {
            if (GameInputManager.Instance != null) {
                if (GameManager.Instance.GetGameState() == GameManager.GameState.PLAYING) {
                    canMove = true;
                } else {
                    canMove = false;
                }
            }
        }

        private void FixedUpdate() {
            if (canMove) {
                rb.linearVelocity = new Vector2(moveSpeed, 0f); 
            } else {
                rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, Vector2.zero, decelerationSpeed * Time.fixedDeltaTime);
            }
        }
    }
}