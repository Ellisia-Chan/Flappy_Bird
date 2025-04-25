using EventSystem;
using EventSystem.Events;
using PlayerSystem;
using UnityEngine;

namespace PipeObstacle {
	public class PipeKill : MonoBehaviour {
		private void OnCollisionEnter2D(Collision2D collision) {
			if (collision.gameObject.GetComponent<PlayerMovement>() != null) {
				EventBus.Publish(new PipePlayerCollisionEnterEvent());
			}
		}
	}
}