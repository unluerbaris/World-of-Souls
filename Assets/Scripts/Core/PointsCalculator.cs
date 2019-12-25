using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace WOS.Core
{
    public class PointsCalculator : MonoBehaviour
    {
        [SerializeField] int points = 100;
        bool takingAway = false;
        [SerializeField] float secondsToTakePoints = 0.3f; // for ex: in every 0.3sec take 1 point
        [SerializeField] Text pointsText;

        private void Start()
        {
            pointsText.text = points.ToString(); // displays the begining points
        }

        private void Update()
        {
            if (takingAway == false && points > 0)
            {
                StartCoroutine(TakePoints());
            }
        }

        public void AddPoints(int pointsToAdd)
        {
            points += pointsToAdd;
            pointsText.text = points.ToString();
        }

        IEnumerator TakePoints() // take points from the player while time flows 
        {
            takingAway = true;
            yield return new WaitForSeconds(secondsToTakePoints);
            points -= 1;
            pointsText.text = points.ToString();
            takingAway = false;
        }
        
    }
}
