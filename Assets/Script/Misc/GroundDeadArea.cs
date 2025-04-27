using Environment.Ground;
using UnityEngine;

namespace Misc {
	public class GroundDeadArea : MonoBehaviour {
		[SerializeField] private Transform groundSpawnPoint;

        private void OnTriggerEnter2D(Collider2D collision) {
            if (collision.gameObject.GetComponent<GroundMovement>() != null) {
                collision.gameObject.transform.position = groundSpawnPoint.position;
            }
        }
    }
}