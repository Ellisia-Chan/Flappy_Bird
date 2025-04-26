using PlayerSystem;
using EventSystem;
using EventSystem.Events;
using UnityEngine;

namespace Collectibles {
	public class Coin : MonoBehaviour {

        // Called at the start of the game, this function has a 50% chance of deactivating the coin game object.
        private void Start() {
            if (Random.value > 0.6f) {
                gameObject.SetActive(false);
            }
        }

        // Called when the trigger collider is touched by another collider.
        // Parameters: collision - The collider that touched the trigger.
        // Returns: None
        private void OnTriggerEnter2D(Collider2D collision) {
            if (collision.gameObject.GetComponent<PlayerMovement>() != null) {
                EventBus.Publish(new CoinCollectEvent(1));
                Destroy(gameObject);
            }
        }
    }
}