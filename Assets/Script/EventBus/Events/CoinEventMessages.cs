using System;

namespace EventSystem.Events {
    public class CoinCollectEvent {
        public int amount;

        public CoinCollectEvent(int amount) {
            this.amount = amount;
        }
    }

    public class CoinValueChangeEvent {
        public int amount;

        public CoinValueChangeEvent(int amount) {
            this.amount = amount;
        }
    }
}