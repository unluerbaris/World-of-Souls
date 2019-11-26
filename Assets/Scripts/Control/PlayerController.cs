using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WOS.Movement;
using WOS.Combat;

namespace WOS.Control
{
    public class PlayerController : MonoBehaviour
    {
        Mover mover;
        Fighter fighter;

        private void Start()
        {
            mover = GetComponent<Mover>();
            fighter = GetComponent<Fighter>();
        }

        void Update()
        {
            PlayerRunInput();
            AttackInput();
            JumpInput();
        }

        private void PlayerRunInput()
        {
            float controlThrow = Input.GetAxis("Horizontal"); //-1 to +1
            mover.Walk(controlThrow);
        }

        private void AttackInput()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                fighter.PlayAttackAnimation();
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
