using UnityEngine;
using ScriptableObjects;
using EventSystem;
using EventSystem.Events;

namespace SystemManagers {
    public class PipeSpawnerManager : MonoBehaviour {
        public static PipeSpawnerManager Instance { get; private set; }

        [Header("Pipe Spawner")]
        [SerializeField] private PipeListSO PipeList;
        [SerializeField] private Transform spawnPosition;
        
        private float spawnTimerMax = 3f;
        private float spawnTimer;
        private bool canSpawn = false;

        private void Awake() {
            if (Instance == null) {
                Instance = this;
            } else {
                Debug.LogWarning("PipeSpawnerManager: There is already a PipeSpawnerManager in the scene, destroying this one.");
                Destroy(gameObject);
            }
        }

        private void OnEnable() {
            EventBus.Subscribe<GameEventStateChange>(e => HandleSpawnState(e.newState));
        }

        private void OnDisable() {
            EventBus.Unsubscribe<GameEventStateChange>(e => HandleSpawnState(e.newState));
        }

        private void Update() {
            if (canSpawn) {
                HandleSpawn(); 
            }
        }

        private void HandleSpawn() {
            spawnTimer -= Time.deltaTime;

            if (PipeList.pipeObstacleList.Count == 0) {
                Debug.LogWarning("No pipes in the list");
                return;
            }

            if (spawnTimer <= 0) {
                spawnTimer = spawnTimerMax;
                int randInt = Random.Range(0, PipeList.pipeObstacleList.Count);
                GameObject pipe = PipeList.pipeObstacleList[randInt];

                Instantiate(pipe, spawnPosition.position, Quaternion.identity);
            }
        }

        private void HandleSpawnState(GameManager.GameState newState) {
            if (newState == GameManager.GameState.PLAYING) {
                canSpawn = true;
            } else {
                canSpawn = false;
            }
        }
    }
}
