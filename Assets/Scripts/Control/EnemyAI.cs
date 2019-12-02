using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WOS.Movement;

namespace WOS.Control
{
    public class EnemyAI : MonoBehaviour
    {
        GameObject target;
        Mover mover;
        Animator animator;

        [SerializeField] float slowDownDistance = 5f;
        [SerializeField] float slowDownSpeedFactor = 0.65f;
        float controlThrow = -1f;
        float directionSetDistance = 0.3f;

        void Start()
        {
            mover = GetComponent<Mover>();
            target = GameObject.FindWithTag("Player");
            animator = GetComponent<Animator>();
        }

        void Update()
        {
            SetDirection();
            SetMovingSpeed();
        }

        private void OnTriggerEnter2D(Collider2D collision)// todo move it to another class later
        {
            if (collision.gameObject.tag == "AttackHitbox")
            {
                animator.SetTrigger("isDead");
                Destroy(gameObject, 0.3f);
            }
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
            if (DistanceToPlayer() <= -directionSetDistance)
            {
                controlThrow = 1f;
            }
            else if (DistanceToPlayer() > -directionSetDistance && DistanceToPlayer() < directionSetDistance)
            {
                controlThrow = 0f;
            }
            else
            {
                controlThrow = -1f;
            }
        }

        private float DistanceToPlayer()
        {
            float distanceToThePlayer = transform.position.x - target.transform.position.x;
            return distanceToThePlayer;
        }
    }
}
