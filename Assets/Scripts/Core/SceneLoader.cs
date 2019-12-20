using UnityEngine.SceneManagement;
using UnityEngine;

namespace WOS.Core
{
    public class SceneLoader : MonoBehaviour
    {
        int currentSceneIndex;

        public void LoadNextScene()
        {
            currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

            if (currentSceneIndex < SceneManager.sceneCountInBuildSettings - 1)
            {
                SceneManager.LoadScene(currentSceneIndex + 1);
            }
        }

        public void LoadCurrentScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void LoadMainMenu()
        {
            SceneManager.LoadScene(0);
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}
