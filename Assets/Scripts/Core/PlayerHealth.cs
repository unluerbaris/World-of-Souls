using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WOS.Core
{
    public class PlayerHealth : MonoBehaviour
    {
        Animator animator;
        SpriteRenderer spriteRenderer;
        Rigidbody2D rb2D;

        [SerializeField] float health = 250f;
        float transparentTime = 1f; // after got hit
        bool isDead = false;
        bool gotHit = false;
        bool untouchableState = false;
        float damageTaken = 50f; // decide it from enemy weapon later. give weapon to the enemy

        private void Start()
        {
            animator = GetComponent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            rb2D = GetComponent<Rigidbody2D>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                gotHit = true;
            }
        }

        private void Update() 
        {
            if (gotHit && !untouchableState)
            {
                StartCoroutine(TakeDamage(damageTaken));
            }
        }

        public IEnumerator TakeDamage(float damage)
        {
            untouchableState = true;

            health = Mathf.Max(health - damage, 0); // it returns 0 when health tries to go below 0

            if (health <= 0 && !isDead)
            {
                Die();
            }

            if (!isDead)
            {
                TransparentEffect();
                yield return new WaitForSeconds(transparentTime);
                EndTransparentEffect();
            }

            untouchableState = false;
            gotHit = false;
        }

        private void TransparentEffect()
        {
            rb2D.velocity = new Vector3(0, 0, 0);

            // gives the character transparancy
            spriteRenderer.material.color = new Color(1f, 1f, 1f, 0.6f);
        }

        private void EndTransparentEffect()
        {
            spriteRenderer.material.color = new Color(1f, 1f, 1f, 1f);
        }

        private void Die()
        {
            isDead = true;

            // avoid any position change
            rb2D.velocity = new Vector3(0, 0, 0);

           // animator.SetTrigger("isDead");
            Destroy(gameObject, 0.3f);
        }
    }
}

