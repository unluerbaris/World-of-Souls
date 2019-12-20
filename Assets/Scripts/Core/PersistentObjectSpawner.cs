using UnityEngine;

namespace WOS.Core
{
    public class PersistentObjectSpawner : MonoBehaviour 
    {
        [SerializeField] GameObject persistentObjectPrefab;

        // because it is static type bool;
        static bool hasSpawned = false; // this boolean value never dies, after this class instance is destroyed.

        private void Awake()
        {
            if (hasSpawned) { return; }

            SpawnPersistentObjects();

            hasSpawned = true;
        }

        private void SpawnPersistentObjects()
        {
            GameObject persistentObjectInstance = Instantiate(persistentObjectPrefab);
            DontDestroyOnLoad(persistentObjectInstance);
        }
    }
}
