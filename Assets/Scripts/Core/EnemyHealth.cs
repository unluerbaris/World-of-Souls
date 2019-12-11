using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WOS.Control;

namespace WOS.Core
{
    public class EnemyHealth : MonoBehaviour
    {
        Animator animator;
        EnemyAI enemyAI;
        SpriteRenderer spriteRenderer;
        Rigidbody2D rb2D;
        AudioManager audioManager;

        [SerializeField] float health = 100f;
        [SerializeField] float activateEnemyTime = 0.6f; // after got hit
        float knockBackForce = 200f;
        [HideInInspector] public float damageTaken;

        bool isDead = false;
        [HideInInspector] public bool gotHit = false;
        

        private void Start()
        {
            animator = GetComponent<Animator>();
            enemyAI = GetComponent<EnemyAI>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            rb2D = GetComponent<Rigidbody2D>();
            audioManager = FindObjectOfType<AudioManager>();
        }

        private void Update()
        {
            if (gotHit && enemyAI.enabled)
            {
                StartCoroutine(TakeDamage(damageTaken));
                gotHit = false;
            }
        }

        public IEnumerator TakeDamage(float damage)
        {
            health = Mathf.Max(health - damage, 0); // it returns 0 when health tries to go below 0

            if (health <= 0 && !isDead)
            {
                audioManager.PlaySound("Hit1"); // play hit sfx
                Die();
            }

            if (!isDead)
            {
                audioManager.PlaySound("Hit1"); // play hit sfx
                KnockBackEffect();
                yield return new WaitForSeconds(activateEnemyTime);
                EnableFunctions();
            }
        }

        private void KnockBackEffect()
        {
            // disable the functions first
            animator.enabled = false; 
            enemyAI.enabled = false;

            // make the velocity zero, and all enemies will be knockbacked with similar force
            rb2D.velocity = new Vector3(0, 0, 0);

            // gives the character transparancy
            spriteRenderer.material.color = new Color(1f, 1f, 1f, 0.6f); 

            GameObject player = GameObject.FindWithTag("Player");

            //calculate the normal vector between enemy and player and add force
            Vector2 dir = (transform.position - player.transform.position).normalized; 
            dir.y = 0.3f;
            rb2D.AddForce(dir * knockBackForce);
        }

        private void EnableFunctions()
        {
            animator.enabled = true;
            enemyAI.enabled = true;
            spriteRenderer.material.color = new Color(1f, 1f, 1f, 1f);
        }

        public bool IsDead() // use it for later(scoring??)
        {
            return isDead;
        }

        private void Die()
        {
            isDead = true;

            // avoid any position change
            enemyAI.enabled = false;
            rb2D.velocity = new Vector3(0, 0, 0);

            animator.SetTrigger("isDead");
            Destroy(gameObject, 0.3f);
        }
    }
}
