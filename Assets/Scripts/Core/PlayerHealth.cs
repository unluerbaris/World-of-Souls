﻿using System.Collections;
using UnityEngine;
using WOS.Control;

namespace WOS.Core
{
    public class PlayerHealth : MonoBehaviour
    {
        SpriteRenderer spriteRenderer;
        Rigidbody2D rb2D;
        AudioManager audioManager;
        [SerializeField] GameObject healthBarObject;
        HealthBar healthBar;

        [SerializeField] float health = 250f;
        float startHealth;
        [SerializeField] float healthCanGetFromPotions = 50f;
        float transparentTime = 1f; // after got hit
        bool isDead = false;
        [HideInInspector] public bool gotHit = false;
        bool untouchableState = false;
        [HideInInspector] public float damageTaken;

        private void Start()
        {
            SetHealthBarAtStart();
            spriteRenderer = GetComponent<SpriteRenderer>();
            rb2D = GetComponent<Rigidbody2D>();
            audioManager = FindObjectOfType<AudioManager>();
        }

        private void SetHealthBarAtStart()
        {
            healthBar = healthBarObject.GetComponent<HealthBar>();
            startHealth = health;
            healthBar.ScaleHealthBar(health, startHealth);
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
            healthBar.ScaleHealthBar(health, startHealth); // update the new health value on health bar

            if (health <= 0 && !isDead)
            {
                Die();
            }

            if (!isDead)
            {
                audioManager.PlaySound("HeroDamaged");
                TransparentEffect();
                yield return new WaitForSeconds(transparentTime);
                EndTransparentEffect();
            }

            untouchableState = false;
            gotHit = false; // make sure the gothit value has been reset. 
                            // sometimes if might be true at the end of the coroutine,
                            // and it causes the player lose health
        }

        public void AddHealth(float healthPotionValue)
        {
            audioManager.PlaySound("HealthPick");
            health = Mathf.Min(health + healthPotionValue, startHealth);
            healthBar.ScaleHealthBar(health, startHealth);
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
            GetComponent<PlayerController>().enabled = false;
            rb2D.velocity = new Vector3(0, 0, 0);

            GetComponent<Animator>().SetTrigger("isDead");
            audioManager.PlaySound("HeroDeath");

           // animator.SetTrigger("isDead");
            Destroy(gameObject, 0.3f);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.tag == "Obstacle")
            {
                StartCoroutine(TakeDamage(health)); // health - health = 0
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            bool isPotionUsed = false;

            if (collision.gameObject.tag == "HealthPotion" && !isPotionUsed)
            {
                AddHealth(healthCanGetFromPotions);
                Destroy(collision.gameObject);
            }

            isPotionUsed = true;
        }
    }
}

