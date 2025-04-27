using SystemManagers;
using EventSystem;
using EventSystem.Events;
using UnityEngine;
using System;
using Misc;

namespace Environment.Ground {
    public class GroundMovement : MonoBehaviour {
        [SerializeField] private float decelerationSpeed = 1f;

        private Action<PipeSpeedChangeEvent> OnPipeSpeedChange;

        private Rigidbody2D rb;
        private float groudSpeed;
        private bool canMove;

        private void Awake() {
            rb = GetComponent<Rigidbody2D>();

            OnPipeSpeedChange = e => { groudSpeed = e.speed; };
        }

        private void Start() {
            groudSpeed = GameManager.Instance.GetPipeMovementSpeed();
        }

        private void OnEnable() {
            EventBus.Subscribe(OnPipeSpeedChange);
        }

        private void OnDisable() {
            EventBus.Unsubscribe(OnPipeSpeedChange);
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
                rb.linearVelocity = new Vector2(groudSpeed, 0);
            } else {
                rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, Vector2.zero, decelerationSpeed * Time.fixedDeltaTime);
            }
        }
    }
}