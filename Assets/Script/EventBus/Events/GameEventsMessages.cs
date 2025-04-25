using UnityEngine;
using SystemManagers;

namespace EventSystem.Events {
    public class GameEventStateChange { 
        public GameManager.GameState newState { get; private set; }
        public GameEventStateChange(GameManager.GameState newState) {
            this.newState = newState;
        }
    }
}
