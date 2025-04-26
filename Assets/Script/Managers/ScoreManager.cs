using EventSystem;
using EventSystem.Events;
using System;
using UnityEngine;

namespace SystemManagers {
    public class ScoreManager : MonoBehaviour {
        public static ScoreManager Instance { get; private set; }

        private Action<CoinCollectEvent> _addScoreAction;

        private int score = 0;

        /// <summary>
        /// Initializes the ScoreManager instance and ensures only one instance exists in the scene.
        /// </summary>
        private void Awake() {
            if (Instance != null) {
                Debug.LogWarning("ScoreManager: There is already a ScoreManager in the scene, destroying this one.");
                Destroy(gameObject);
                return;
            }

            Instance = this;

            _addScoreAction = e => AddScore(e.amount);
        }

        /// <summary>
        /// Called when the behaviour becomes enabled and active.
        /// </summary>
        private void OnEnable() {
            if (Instance != this) return;

            EventBus.Subscribe(_addScoreAction);
        }

        /// <summary>
        /// Unsubscribes from the CoinCollectEvent when the ScoreManager is disabled.
        /// </summary>
        private void OnDisable() {
            if (Instance != this) return;

            EventBus.Unsubscribe(_addScoreAction);
        }

        /// <summary>
        /// Adds the specified amount to the current score and publishes a CoinValueChangeEvent.
        /// </summary>
        /// <param name="amount">The amount to add to the score.</param>
        private void AddScore(int amount) {
            score += amount;
            EventBus.Publish(new CoinValueChangeEvent(score));
        }

        public int GetScore() => score;
    }
}