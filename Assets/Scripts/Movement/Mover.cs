﻿using UnityEngine;

namespace WOS.Movement
{
    public class Mover : MonoBehaviour
    {
        [SerializeField] float walkSpeed = 6f;
        [SerializeField] float jumpForce = 5.8f;

        bool isFacingRight = true;
        bool hasHorizontalSpeed;

        Rigidbody2D rb2D;
        Animator animator;
        BoxCollider2D boxCollider2d;

        private void Start()
        {
            rb2D = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            boxCollider2d = GetComponent<BoxCollider2D>();
        }

        private void FixedUpdate()
        {
            if (gameObject.tag == "Player")
            {
                IsJumping();
            }
            HasHorizontalSpeed();
        }

        private bool HasHorizontalSpeed()
        {
            //if x velocity's absolute value is more than epsilon(very close to zero) return true
            hasHorizontalSpeed = Mathf.Abs(rb2D.velocity.x) > Mathf.Epsilon;
            return hasHorizontalSpeed;
        }

        public void Walk(float controlThrow)
        {
            Vector2 characterVelocity = new Vector2(controlThrow * walkSpeed, rb2D.velocity.y);
            rb2D.velocity = characterVelocity;

            if (gameObject.tag == "Player")
            {
                animator.SetBool("isWalking", HasHorizontalSpeed());
            }

            FlipSprite();
        }

        public void FlipSprite() // Rotate the sprite instead of scaling it with -1. So it can also rotate local x axis too.
                                  // It is a better way if you are rotating your character before shoot.
        {
            if (HasHorizontalSpeed() && Mathf.Sign(rb2D.velocity.x) == -1 && isFacingRight)
            {
                isFacingRight = false;
                transform.Rotate(0f, 180f, 0f);
            }
            else if (HasHorizontalSpeed() && Mathf.Sign(rb2D.velocity.x) == 1 && !isFacingRight)
            {
                isFacingRight = true;
                transform.Rotate(0f, 180f, 0f);
            }
        }

        public bool IsJumping()
        {
            bool isJumping = !boxCollider2d.IsTouchingLayers(LayerMask.GetMask("Ground"));
            animator.SetBool("isJumping", isJumping);
            return isJumping;
        }

        public void Jump() 
        {
            if (IsJumping()) return; // avoids double jump

            animator.SetTrigger("jumpTrigger"); // plays the animation once, till something triggers it again
                                                // if the groundCheck is false, player exits the jumping anim state
            Vector2 jumpVelocity = new Vector2(0f, jumpForce);
            rb2D.velocity += jumpVelocity;
        }
    }
}

