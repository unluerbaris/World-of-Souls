using UnityEngine;
using WOS.Movement;

namespace WOS.Control
{
    public class EnemyAI : MonoBehaviour
    {
        GameObject target;
        Mover mover;

        [SerializeField] float slowDownDistance = 5f;
        [SerializeField] float slowDownSpeedFactor = 0.65f;
        [SerializeField] float stopDistance = 16f;
        float controlThrow = -1f;
        float directionSetDistance = 0.3f;

        void Start()
        {
            mover = GetComponent<Mover>();
            target = GameObject.FindWithTag("Player");
        }

        void Update()
        {
            if (Mathf.Abs(DistanceToPlayer()) >= stopDistance) // don't move if player is far
            {
                controlThrow = 0f;
                SetMovingSpeed();
                return;
            }
            SetDirection();
            SetMovingSpeed();
        }

        private void SetMovingSpeed()
        {
            if (DistanceToPlayer() < slowDownDistance)
            {
                mover.Walk(controlThrow * slowDownSpeedFactor);
            }
            else
            {
                mover.Walk(controlThrow);
            }
        }

        private void SetDirection() // if distanceToPlayer changes its (+, -) symbol,
        {                           // change the controlThrow and it will flip the sprite
            if (DistanceToPlayer() <= directionSetDistance)
            {
                controlThrow = -1f;
            }
            else if (DistanceToPlayer() > directionSetDistance && DistanceToPlayer() < -directionSetDistance)
            {
                controlThrow = 0f;
            }
            else
            {
                controlThrow = 1f;
            }
        }

        private float DistanceToPlayer()
        {
            if (target == null)
            {
                return 10; // setting the distence of the player to 10 makes enemies walk, after player dies
            }
            float distanceToThePlayer = target.transform.position.x - transform.position.x;
            return distanceToThePlayer;
        }
    }
}
