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
            PlayerRunUpdate();
            AttackUpdate();
            JumpUpdate();
        }

        private void PlayerRunUpdate()
        {
            float controlThrow = Input.GetAxis("Horizontal"); //-1 to +1
            mover.Walk(controlThrow);
        }

        private void AttackUpdate()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                fighter.PlayAttackAnimation();
            }
        }

        private void JumpUpdate()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                mover.Jump();
            }
        }
    }
}
