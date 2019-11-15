using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WOS.Combat
{
    public class Fighter : MonoBehaviour
    {
        Animator animator;

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        public void PlayAttackAnimation()
        {
            animator.SetTrigger("isAttacking");
        }
    }
}
