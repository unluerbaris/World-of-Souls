using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WOS.Movement;
using WOS.Combat;
using WOS.Core;

namespace WOS.Control
{
    public class PlayerController : MonoBehaviour
    {
        Mover mover;
        Fighter fighter;
        Animator animator;
        AudioManager audioManager;

        [SerializeField] GameObject attackHitbox;

        bool isAttacking = false;

        private void Start()
        {
            mover = GetComponent<Mover>();
            fighter = GetComponent<Fighter>();
            animator = GetComponent<Animator>();
            audioManager = FindObjectOfType<AudioManager>();
            attackHitbox.SetActive(false);
        }

        void Update()
        {
            PlayerRunInput();
            JumpInput();
            StartCoroutine(AttackInput());
        }

        private void PlayerRunInput()
        {
            float controlThrow = Input.GetAxis("Horizontal"); //-1 to +1

            // if player is in attacking action and on the ground
            // don't move horizontally
            if (isAttacking && !mover.IsJumping())
            {
                controlThrow = 0f;
            }

            mover.Walk(controlThrow);
        }

        // player attacking animation speed has set to 1.5f
        IEnumerator AttackInput()
        {
            if (Input.GetKeyDown(KeyCode.Space) && !isAttacking)
            {
                isAttacking = true;

                audioManager.PlaySound("SwordSwipe"); // play sword swiping sfx

                fighter.Attack();
                attackHitbox.SetActive(true);

                yield return new WaitForSeconds(0.1f); // wait for disabling the attack hitbox
                attackHitbox.SetActive(false);

                // don't attack again before attacking animation ends
                float animLength = animator.GetCurrentAnimatorStateInfo(0).length;
                yield return new WaitForSeconds(animLength); 
                isAttacking = false;
            }
        }

        private void JumpInput()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                mover.Jump();
            }
        }
    }
}
