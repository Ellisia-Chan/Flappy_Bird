using EventSystem;
using EventSystem.Events;
using TMPro;
using UnityEngine;

namespace UI {
    public class ScoreUI : MonoBehaviour {
        [Header("Score")]
        [SerializeField] private TextMeshProUGUI scoreText;

        private void Awake() {
            UpdateUI(0);
        }

        /// Called when the behaviour becomes enabled.
        private void OnEnable() {
            EventBus.Subscribe<CoinValueChangeEvent>(e => UpdateUI(e.amount));
        }

        // Disables the ScoreUI component, unsubscribing from CoinValueChangeEvent to prevent further updates.
        private void OnDisable() {
            EventBus.Unsubscribe<CoinValueChangeEvent>(e => UpdateUI(e.amount));
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