using UnityEngine;

namespace EventSystem.Events {
    public class PipePlayerCollisionEnterEvent { }
    public class PipeSpeedChangeEvent {
        public float speed;

        public PipeSpeedChangeEvent(float speed) {
            this.speed = speed;
        }
    }
}
