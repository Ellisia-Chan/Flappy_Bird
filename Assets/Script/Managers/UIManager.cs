using UnityEngine;

namespace SystemManagers {
    public class UIManager : MonoBehaviour {
        public static UIManager Instance { get; private set; }

        private void Awake() {
            if (Instance == null) {
                Instance = this;
            } else {
                Debug.LogWarning("UIManager: There is already a UIManager in the scene, destroying this one.");
                Destroy(gameObject);
            }
        }
    }
}