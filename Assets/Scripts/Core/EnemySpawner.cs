using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WOS.Core
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] GameObject enemy;
        bool enemySpawned = false;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!enemySpawned && collision.gameObject.tag == "Respawn")
            {
                enemySpawned = true;
                GameObject enemyClone = Instantiate(enemy, transform.position, Quaternion.identity) as GameObject;
                Destroy(gameObject);
            }
        }
    }
}
