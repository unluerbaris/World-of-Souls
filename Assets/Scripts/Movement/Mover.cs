using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WOS.Movement
{
    public class Mover : MonoBehaviour
    {
        [SerializeField] float walkSpeed = 6f;
        [SerializeField] float jumpForce = 5.8f;

        bool isFacingRight = true;

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

            FlipSprite();
        }

        private void FlipSprite() // Rotate the sprite instead of scaling it with -1. So it can also rotate local x axis too.
                                  // It is a better way if you are rotating your character before shoot.
        {
            bool hasHorizontalSpeed = Mathf.Abs(rb2D.velocity.x) > Mathf.Epsilon;

            if (hasHorizontalSpeed && Mathf.Sign(rb2D.velocity.x) == -1 && isFacingRight)
            {
                isFacingRight = false;
                transform.Rotate(0f, 180f, 0f);
            }
            else if (hasHorizontalSpeed && Mathf.Sign(rb2D.velocity.x) == 1 && !isFacingRight)
            {
                isFacingRight = true;
                transform.Rotate(0f, 180f, 0f);
            }
        }

        public void Jump() 
        {
            animator.SetBool("isJumping", true);
            Vector2 jumpVelocity = new Vector2(0f, jumpForce);
            rb2D.velocity += jumpVelocity;
        }
    }
}

