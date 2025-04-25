using UnityEngine;
using EventSystem;
using EventSystem.Events;

namespace SystemManagers {
    public class GameManager : MonoBehaviour {
        public static GameManager Instance { get; private set; }

        public enum GameState {
            WAITING,
            PLAYING,
            GAMEOVER
        }

        private GameState gameState;

        private void Awake() {
            if (Instance == null) {
                Instance = this;
            } else {
                Debug.LogWarning("GameManager: There is already a GameManager in the scene, destroying this one.");
                Destroy(gameObject);
            }

            gameState = GameState.WAITING;
        }

        private void OnEnable() {
            EventBus.Subscribe<PlayerInputEventJumpAction>(e => InteractAction());
            EventBus.Subscribe<PipePlayerCollisionEnterEvent>(e => HandleGameOver());
        }

        private void OnDisable() {
            EventBus.Unsubscribe<PlayerInputEventJumpAction>(e => InteractAction());
            EventBus.Unsubscribe<PipePlayerCollisionEnterEvent>(e => HandleGameOver());
        }

        private void InteractAction() {
            if (gameState == GameState.WAITING) {
                gameState = GameState.PLAYING;
                EventBus.Publish(new GameEventStateChange(gameState));
            }
        }

        private void HandleGameOver() {
            gameState = GameState.GAMEOVER;
            EventBus.Publish(new GameEventStateChange(gameState));
        }

        public GameState GetGameState() => gameState;
    }
}
