using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using SystemManagers;

namespace UI {
    public class GameOverUI : MonoBehaviour {
        [Header("GameOver UI")]
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private Button restartButton;

        private void Awake() {
            restartButton.onClick.AddListener(() => {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            });
        }

        private void OnEnable() {
            if (ScoreManager.Instance != null) {
                scoreText.text = ScoreManager.Instance.GetScore().ToString();
            }
        }

        private void OnDestroy() {
            restartButton.onClick.RemoveListener(() => {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            });
        }
    }
}