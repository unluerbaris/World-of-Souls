using System.Collections;
using UnityEngine;

namespace WOS.Core
{
    public class GameSession : MonoBehaviour
    {
        [SerializeField] Canvas gameOverCanvas;
        [SerializeField] Canvas winCanvas;
        [SerializeField] GameObject player;
        bool playerLost = false;

        private void Awake()
        {
            gameOverCanvas.enabled = false;
            winCanvas.enabled = false;
        }

        private void Update()
        {
            if (player == null && !playerLost)
            {
                playerLost = true;  // just call the function once, when player lose the game
                StartCoroutine(LoadGameOverScreen());
            }
        }

        public IEnumerator LoadGameOverScreen()
        {
            yield return new WaitForSeconds(2f); // wait for seconds, before show the game over screen
            gameOverCanvas.enabled = true;
        }

        public IEnumerator LoadWinScreen()
        {
            yield return new WaitForSeconds(1f);
            winCanvas.enabled = true;
        }
    }
}
