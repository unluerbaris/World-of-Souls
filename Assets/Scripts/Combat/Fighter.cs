using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WOS.Combat
{
    public class Fighter : MonoBehaviour
    {
        Animator animator;
        [SerializeField] GameObject attackHitbox;

        [SerializeField] float timeBetweenAttacks = 1f;
        private float timeSinceLastAttack = Mathf.Infinity;

        private void Start()
        {
            animator = GetComponent<Animator>();
            if (attackHitbox != null)
            {
                attackHitbox.SetActive(false);
            }
        }

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
        }

        public IEnumerator Attack() // activate and deactivate the attackhitbox using coroutine
        {
            if (timeSinceLastAttack >= timeBetweenAttacks)
            {
                PlayAttackAnimation();
                if (attackHitbox != null)
                {
                    attackHitbox.SetActive(true);
                }
                timeSinceLastAttack = 0; // reset the timer 
                yield return new WaitForSeconds(0.1f);
                if (attackHitbox != null)
                {
                    attackHitbox.SetActive(false);
                }
            }
        }

        public void PlayAttackAnimation()
        {
            animator.SetTrigger("isAttacking");
        }
    }
}
