using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WOS.Movement
{
    public class Mover : MonoBehaviour
    {
        [SerializeField] float walkSpeed = 6f;

        Rigidbody2D rb2D;
        Animator animator;

        private void Start()
        {
            rb2D = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
        }

        public void Walk(float controlThrow)
        {
            Vector2 characterVelocity = new Vector2(controlThrow * walkSpeed, rb2D.velocity.y);
            rb2D.velocity = characterVelocity;

            //if x velocity's absolute value is more than epsilon(very close to zero) return true
            bool hasHorizontalSpeed = Mathf.Abs(rb2D.velocity.x) > Mathf.Epsilon;

            animator.SetBool("isWalking", hasHorizontalSpeed);
        }
    }
}

