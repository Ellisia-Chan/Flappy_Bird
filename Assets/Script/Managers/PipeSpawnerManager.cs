using System;
using UnityEngine;
using ScriptableObjects;
using EventSystem;
using EventSystem.Events;

namespace SystemManagers {
    public class PipeSpawnerManager : MonoBehaviour {
        public static PipeSpawnerManager Instance { get; private set; }

        private Action<GameEventStateChange> _handleStateChangeCallback;
        private Action<CoinValueChangeEvent> _handleCoinValueChangeCallback;

        [Header("Pipe Spawner")]
        [SerializeField] private PipeListSO PipeList;
        [SerializeField] private Transform spawnPosition;

        private float spawnTimerMax = 2.5f;
        private float spawnTimer;
        private bool canSpawn = false;

        private void Awake() {
            if (Instance != null) {
                Debug.LogWarning("PipeSpawnerManager: There is already a PipeSpawnerManager in the scene, destroying this one.");
                Destroy(gameObject);
                return;
            }

            Instance = this;

            _handleStateChangeCallback = e => HandleSpawnState(e.newState);
            _handleCoinValueChangeCallback = e => DecreaseSpawnTime(e.amount);
        }

        private void OnEnable() {
            if (Instance != this) return;

            EventBus.Subscribe(_handleStateChangeCallback);
            EventBus.Subscribe(_handleCoinValueChangeCallback);
        }

        private void OnDisable() {
            if (Instance != this) return;

            EventBus.Unsubscribe(_handleStateChangeCallback);
            EventBus.Unsubscribe(_handleCoinValueChangeCallback);
        }

        private void Update() {
            if (canSpawn) {
                HandleSpawn();
            }
        }

        /// <summary>
        /// Handles the spawning of pipes in the game.
        /// This function is called every frame and is responsible for 
        /// decrementing the spawn timer, checking if it's time to spawn a new pipe, 
        /// and instantiating a random pipe from the PipeList at the spawn position.
        /// </summary>
        private void HandleSpawn() {
            spawnTimer -= Time.deltaTime;

            if (PipeList.pipeObstacleList.Count == 0) {
                Debug.LogWarning("No pipes in the list");
                return;
            }

            if (spawnTimer <= 0) {
                spawnTimer = spawnTimerMax;
                int randInt = UnityEngine.Random.Range(0, PipeList.pipeObstacleList.Count);
                GameObject pipe = PipeList.pipeObstacleList[randInt];

                Instantiate(pipe, spawnPosition.position, Quaternion.identity);
            }
        }

        /// <summary>
        /// Handles the game state change event and updates the canSpawn flag accordingly.
        /// </summary>
        /// <param name="newState">The new game state.</param>
        private void HandleSpawnState(GameManager.GameState newState) {
            if (newState == GameManager.GameState.PLAYING) {
                canSpawn = true;
            } else {
                canSpawn = false;
            }
        }

        /// <summary>
        /// Decreases the spawn timer maximum value by a certain amount.
        /// </summary>
        /// <param name="amount">The amount to decrease the spawn timer by.</param>
        private void DecreaseSpawnTime(int amount) {
            if (amount % 3 == 0) {
                if (spawnTimerMax > 1f) {
                    spawnTimerMax -= 0.2f;

                    if (spawnTimerMax < 1f) {
                        spawnTimerMax = 1f;
                    }

                    Debug.Log("SpawnTimerMax: " + spawnTimerMax);
                } else {
                    spawnTimerMax = 1f;
                }
            }
        }
    }
}
