using UnityEngine;
using EventSystem;
using EventSystem.Events;

namespace SystemManagers {
    public class GameManager : MonoBehaviour {
        public static GameManager Instance { get; private set; }

        private float pipeMovementSpeed = 4f;

        public enum GameState {
            WAITING,
            PLAYING,
            GAMEOVER
        }

        private GameState gameState;

        /// <summary>
        /// Initializes the GameManager instance, ensuring only one instance exists in the scene,
        /// and sets the initial game state to WAITING.
        /// </summary>
        private void Awake() {
            if (Instance == null) {
                Instance = this;
            } else {
                Debug.LogWarning("GameManager: There is already a GameManager in the scene, destroying this one.");
                Destroy(gameObject);
            }

            gameState = GameState.WAITING;
        }

        /// <summary>
        /// Called when the behaviour becomes enabled and active.
        /// Subscribes to the PlayerInputEventJumpAction and PipePlayerCollisionEnterEvent events.
        /// </summary>
        private void OnEnable() {
            EventBus.Subscribe<PlayerInputEventJumpAction>(e => InteractAction());
            EventBus.Subscribe<PipePlayerCollisionEnterEvent>(e => HandleGameOver());
            EventBus.Subscribe<CoinValueChangeEvent>(e => IncreasePipeSpeed(e.amount));
        }

        /// <summary>
        /// Called when the behaviour becomes disabled and inactive.
        /// </summary>
        private void OnDisable() {
            EventBus.Unsubscribe<PlayerInputEventJumpAction>(e => InteractAction());
            EventBus.Unsubscribe<PipePlayerCollisionEnterEvent>(e => HandleGameOver());
            EventBus.Unsubscribe<CoinValueChangeEvent>(e => IncreasePipeSpeed(e.amount));
        }

        /// <summary>
        /// Handles the player's interaction action,
        /// transitioning the game state from WAITING to PLAYING if applicable.
        /// </summary>
        private void InteractAction() {
            if (gameState == GameState.WAITING) {
                gameState = GameState.PLAYING;
                EventBus.Publish(new GameEventStateChange(gameState));
            }
        }

        // Handles the game over state by updating the game state and publishing a game event state change.
        private void HandleGameOver() {
            gameState = GameState.GAMEOVER;
            EventBus.Publish(new GameEventStateChange(gameState));
        }

        private void IncreasePipeSpeed(int amount) {
            if (amount % 2 == 0) {
                pipeMovementSpeed += 0.3f;
            }
        }

        /// <summary>
        /// Gets the current game state.
        /// </summary>
        /// <returns>The current game state.</returns>
        public GameState GetGameState() => gameState;

        public float GetPipeMovementSpeed() => pipeMovementSpeed;
    }
}
