using System;
using EventSystem;
using EventSystem.Events;
using SystemManagers;
using UnityEngine;

namespace Animation.Player {
    public class PlayerAnimation : MonoBehaviour {
        private Animator animator;

        private Action<CoinCollectEvent> OnCoinCollect;
        private Action<GameEventStateChange> OnGameEventStateChange;

        private void Awake() {
            animator = GetComponent<Animator>();

            OnCoinCollect = e => HandleCoinCollectAnim();
            OnGameEventStateChange = e => HandleGameEventStateChange(e);
        }

        private void OnEnable() {
            EventBus.Subscribe(OnCoinCollect);
            EventBus.Subscribe(OnGameEventStateChange);
        }

        private void OnDisable() {
            EventBus.Unsubscribe(OnCoinCollect);
            EventBus.Unsubscribe(OnGameEventStateChange);
        }

        private void HandleCoinCollectAnim() {
            animator.SetTrigger("CoinCollect");
        }

        private void HandleGameEventStateChange(GameEventStateChange e) {
            if (e.newState == GameManager.GameState.GAMEOVER) {
                animator.SetTrigger("GameOver");
            }
        }
    }

}