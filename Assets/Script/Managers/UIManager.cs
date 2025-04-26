using System;
using EventSystem;
using EventSystem.Events;
using UnityEngine;

namespace SystemManagers {
    public class UIManager : MonoBehaviour {
        public static UIManager Instance { get; private set; }

        [Header("UI")]
        [SerializeField] private GameObject waitingToStartUI;
        [SerializeField] private GameObject scoreUI;
        [SerializeField] private GameObject gameOverUI;

        private Action<GameEventStateChange> _handleStateChangeCallback;

        private void Awake() {
            if (Instance == null) {
                Instance = this;
            } else {
                Debug.LogWarning("UIManager: There is already a UIManager in the scene, destroying this one.");
                Destroy(gameObject);
                return;
            }

            _handleStateChangeCallback = e => HandleUIState(e.newState);
        }

        private void Start() {
            if (GameManager.Instance.GetGameState() == GameManager.GameState.WAITING) {
                waitingToStartUI.SetActive(true);
                scoreUI.SetActive(false);
                gameOverUI.SetActive(false);
            }
        }

        private void OnEnable() {
            if (Instance != this) return;

            EventBus.Subscribe(_handleStateChangeCallback);
        }

        private void OnDisable() {
            if (Instance != this) return;

            EventBus.Unsubscribe(_handleStateChangeCallback);
        }

        private void HandleUIState(GameManager.GameState state ) {
            switch (state) {
                case GameManager.GameState.WAITING:
                    ToggleWaitingToStartUI(true);
                    ToggleScoreUI(false);
                    ToggleGameOverUI(false);
                    break;
                case GameManager.GameState.PLAYING:
                    ToggleWaitingToStartUI(false);
                    ToggleScoreUI(true);
                    ToggleGameOverUI(false);
                    break;
                case GameManager.GameState.GAMEOVER:
                    ToggleWaitingToStartUI(false);
                    ToggleScoreUI(false);
                    ToggleGameOverUI(true);
                    break;
            }
        }

        private void ToggleWaitingToStartUI(bool activeState) => waitingToStartUI.SetActive(activeState);
        private void ToggleScoreUI(bool activeState) => scoreUI.SetActive(activeState);
        private void ToggleGameOverUI(bool activeState) => gameOverUI.SetActive(activeState);
    }
}