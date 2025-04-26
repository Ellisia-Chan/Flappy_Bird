using UnityEngine;

namespace SystemManagers.DDOL {
    internal class ManagerDDOL : MonoBehaviour {
        private static ManagerDDOL instance;
        private void Awake() { 
            if (instance != null) {
                Destroy(gameObject);
                return;
            }

            instance = this;
            DontDestroyOnLoad(gameObject); 
        }
    }
}