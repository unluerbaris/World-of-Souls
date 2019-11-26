using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WOS.Combat
{
    public class Fighter : MonoBehaviour
    {
        Animator animator;

        [SerializeField] float timeBetweenAttacks = 1f;
        private float timeSinceLastAttack = Mathf.Infinity;

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
        }

        public void Attack()
        {
            if (timeSinceLastAttack >= timeBetweenAttacks)
            {
                PlayAttackAnimation();
                timeSinceLastAttack = 0; // reset the timer 
            }
        }

        public void PlayAttackAnimation()
        {
            animator.SetTrigger("isAttacking");
        }
    }
}
