using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WOS.Core
{
    public class Health : MonoBehaviour
    {
        Animator animator;

        [SerializeField] float health = 100f;
        bool isDead = false;

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        public void TakeDamage(float damage)
        {
            health = Mathf.Max(health - damage, 0); // it returns 0 when health tries to go below 0
            Die(); // if the health is equals to 0 or less then 0
        }

        public bool IsDead()
        {
            return isDead;
        }

        private void Die()
        {
            if (health <= 0 && !isDead)
            {
                isDead = true;
                animator.SetTrigger("isDead");
                Destroy(gameObject, 0.3f);
            }
        }
    }
}
