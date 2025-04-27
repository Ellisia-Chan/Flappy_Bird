using System;
using EventSystem;
using EventSystem.Events;
using UnityEngine;

namespace Animation.Player {
    public class PlayerAnimation : MonoBehaviour {
        private Animator animator;

        private Action<CoinCollectEvent> OnCoinCollect;

        private void Awake() {
            animator = GetComponent<Animator>();

            OnCoinCollect = e => HandleCoinCollectAnim();
        }

        private void OnEnable() {
            EventBus.Subscribe(OnCoinCollect);
        }

        private void OnDisable() {
            EventBus.Unsubscribe(OnCoinCollect);
        }

        private void HandleCoinCollectAnim() {
            animator.SetTrigger("CoinCollect");
        }
    }

}