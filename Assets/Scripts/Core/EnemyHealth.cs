﻿using System.Collections;
using UnityEngine;
using WOS.Control;
using WOS.Combat;
using UnityEngine.UI;

namespace WOS.Core
{
    public class EnemyHealth : MonoBehaviour
    {
        Animator animator;
        EnemyAI enemyAI;
        PointsCalculator pointsCalculator;
        SpriteRenderer spriteRenderer;
        Rigidbody2D rb2D;
        AudioManager audioManager;
        EnemyWeapon enemyWeapon;

        [SerializeField] float health = 100f;
        [SerializeField] float activateEnemyTime = 0.6f; // after got hit
        [SerializeField] int enemyKillPoints = 50;
        float knockBackForce = 230f;
        float currentWeaponDamage;
        [HideInInspector] public float damageTaken;

        bool isDead = false;
        [HideInInspector] public bool gotHit = false;
        

        private void Start()
        {
            animator = GetComponent<Animator>();
            enemyAI = GetComponent<EnemyAI>();
            enemyWeapon = GetComponent<EnemyWeapon>();
            currentWeaponDamage = enemyWeapon.weaponDamage; // get the current weapon damage value
            pointsCalculator = FindObjectOfType<PointsCalculator>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            rb2D = GetComponent<Rigidbody2D>();
            audioManager = FindObjectOfType<AudioManager>();
        }

        private void Update()
        {
            if (gotHit && enemyAI.enabled)
            {
                StartCoroutine(TakeDamage(damageTaken));
            }
            gotHit = false;
        }

        public IEnumerator TakeDamage(float damage)
        {
            health = Mathf.Max(health - damage, 0); // it returns 0 when health tries to go below 0

            if (health <= 0 && !isDead)
            {
                Die(enemyKillPoints);
            }

            if (!isDead)
            {
                PlayRandomHitSFX();
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
            enemyWeapon.weaponDamage = 0; // enemy can't do damage

            // make the velocity zero, and all enemies will be knockbacked with similar force
            rb2D.velocity = new Vector3(0, 0, 0);

            // gives the character transparancy
            spriteRenderer.material.color = new Color(1f, 1f, 1f, 0.6f); 

            GameObject player = GameObject.FindWithTag("Player");

            //calculate the normal vector between enemy and player and add force
            Vector2 dir = (transform.position - player.transform.position).normalized; 
            dir.y = 0.5f;
            rb2D.AddForce(dir * knockBackForce);
        }

        private void EnableFunctions()
        {
            animator.enabled = true;
            enemyAI.enabled = true;
            spriteRenderer.material.color = new Color(1f, 1f, 1f, 1f);
            enemyWeapon.weaponDamage = currentWeaponDamage; // enemy can do damage again
        }

        private void Die(int pointsToPlayer)
        {
            isDead = true;

            audioManager.PlaySound("EnemyDeath");
            pointsCalculator.AddPoints(pointsToPlayer);
            // avoid any position change
            enemyAI.enabled = false;
            rb2D.velocity = new Vector3(0, 0, 0);

            animator.SetTrigger("isDead");
            Destroy(gameObject, 0.3f);
        }

        private void PlayRandomHitSFX()
        {
            int randomNumber = Random.Range(0, 3);

            switch (randomNumber)
            {
                case 0:
                    audioManager.PlaySound("Hit1");
                    break;
                case 1:
                    audioManager.PlaySound("Hit2");
                    break;
                case 2:
                    audioManager.PlaySound("Hit3");
                    break;
                default:
                    Debug.Log("SFX not found!!");
                    break;
            }

        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Obstacle")
            {
                Die(0); // if player doesn't kill the enemy by itself, no points for it
            }
        }
    }
}
