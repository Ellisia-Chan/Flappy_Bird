using UnityEngine;

namespace PipeObstacle {
    public class PipeDeadArea : MonoBehaviour {

        [SerializeField] private LayerMask pipeLayerMask;
        private void OnTriggerEnter2D(Collider2D collision) {
            if (((1 << collision.gameObject.layer) & pipeLayerMask) != 0) {
                Destroy(collision.gameObject);
            }
        }
    } 
}
