using EventSystem;
using EventSystem.Events;
using System;
using TMPro;
using UnityEngine;

namespace UI {
    public class ScoreUI : MonoBehaviour {
        private Action<CoinValueChangeEvent> _updateUIAction;

        [Header("Score")]
        [SerializeField] private TextMeshProUGUI scoreText;

        private void Awake() {
            UpdateUI(0);

            _updateUIAction = e => UpdateUI(e.amount);
        }

        /// Called when the behaviour becomes enabled.
        private void OnEnable() {
            EventBus.Subscribe(_updateUIAction);
        }

        // Disables the ScoreUI component, unsubscribing from CoinValueChangeEvent to prevent further updates.
        private void OnDisable() {
            EventBus.Unsubscribe(_updateUIAction);
        }

        /// <summary>
        /// Updates the UI to display the current score.
        /// </summary>
        /// <param name="score">The current score to be displayed.</param>
        private void UpdateUI(int score) {
            scoreText.text = score.ToString();
        }
    }
}