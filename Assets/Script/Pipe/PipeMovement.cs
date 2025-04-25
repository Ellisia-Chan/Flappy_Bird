using UnityEngine;

namespace PipeObstacle {
    [RequireComponent(typeof(Rigidbody2D))]
    public class PipeMovement : MonoBehaviour {
        [Header("Movement")]
        [SerializeField] private float moveSpeed = 5f;

        private Rigidbody2D rb;

        private void Awake() {
            rb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate() {
            rb.linearVelocity = new Vector2(-moveSpeed, 0f);
        }
    }

}