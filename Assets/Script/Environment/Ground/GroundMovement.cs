using EventSystem;
using EventSystem.Events;
using System;
using SystemManagers;
using UnityEngine;

namespace Environment.Ground {
    public class GroundMovement : MonoBehaviour {
        [SerializeField] private float width = 20f;

        private Action<PipeSpeedChangeEvent> OnSpeedChange;

        private SpriteRenderer spriteRenderer;

        private Vector2 startSize;gi
        private float speed;
        private bool canMove;

        private void Awake() {
            spriteRenderer = GetComponent<SpriteRenderer>();
            startSize = new Vector2(spriteRenderer.size.x, spriteRenderer.size.y);

            OnSpeedChange = e => { speed = e.speed; };
        }

        private void OnEnable() {
            EventBus.Subscribe(OnSpeedChange);
        }

        private void OnDisable() {
            EventBus.Unsubscribe(OnSpeedChange);
        }

        private void Start() {
            speed = GameManager.Instance.GetPipeMovementSpeed();
        }

        private void Update() {
            if (GameInputManager.Instance != null) {
                if (GameManager.Instance.GetGameState() == GameManager.GameState.PLAYING) {
                    canMove = true;
                } else {
                    canMove = false;
                }
            }

            if (canMove) {
                spriteRenderer.size = new Vector2(spriteRenderer.size.x - speed * Time.deltaTime, spriteRenderer.size.y);

                if (spriteRenderer.size.x < width) {
                    spriteRenderer.size = startSize;
                } 
            } else {
                //spriteRenderer.size = Vector2.Lerp(spriteRenderer.size, startSize, Time.deltaTime);
            }
        }
    }
}
