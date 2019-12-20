using UnityEngine;
using UnityEngine.UI;

namespace WOS.Core
{
    public class Score : MonoBehaviour
    {
        int score;
        [SerializeField] Text scoreText;

        void Start()
        {
            scoreText.text = "Soul Points: " + score.ToString(); // score is 0 at the start
        }

        public void AddToScore(int scoresToAdd)
        {
            score += scoresToAdd;
            scoreText.text = "Soul Points: " + score.ToString();
        }
    }
}
