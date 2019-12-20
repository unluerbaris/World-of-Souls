using UnityEngine;

namespace WOS.Core
{
    public class EnemySpawner : MonoBehaviour
    {
        // Set SpawnerType on Spawner gameobjects (InScreenFront, InScreenBehind, OutScreenFront, OutScreenBehind)
        // Set Spawner Trigger Type on Player's trigger collider objects (RespawnIn, RespawnOut)
        // RespawnIn && InScreenFront => spawns an enemy in front of the player in the screen view
        // RespawnOut && OutScreenFront => spawns an enemy in front of the player but out of the screen view
        // RespawnIn && InScreenBehind => spawns an enemy behind the player in the screen view
        // RespawnOut && OutScreenBehind => spawns an enemy behind the player but out of the screen view
        public enum SpawnerType { InScreenFront, InScreenBehind, OutScreenFront, OutScreenBehind}
        public SpawnerType spawnerType;

        [SerializeField] GameObject enemy;
        bool enemySpawned = false;

        private void SpawnEnemy()
        {
            enemySpawned = true;
            GameObject enemyClone = Instantiate(enemy, transform.position, Quaternion.identity) as GameObject;
            Destroy(gameObject);
        }

        // use OnTriggerEnter2D to spawn an enemy in front of the player
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!enemySpawned && collision.gameObject.tag == "RespawnIn" && spawnerType == SpawnerType.InScreenFront)
            {
                SpawnEnemy();
            }
            else if (!enemySpawned && collision.gameObject.tag == "RespawnOut" && spawnerType == SpawnerType.OutScreenFront)
            {
                SpawnEnemy();
            }
        }

        // use OnTriggerExit2D to spawn an enemy behind the player
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (!enemySpawned && collision.gameObject.tag == "RespawnIn" && spawnerType == SpawnerType.InScreenBehind)
            {
                SpawnEnemy();
            }
            else if (!enemySpawned && collision.gameObject.tag == "RespawnOut" && spawnerType == SpawnerType.OutScreenBehind)
            {
                SpawnEnemy();
            }
        }
    }
}
