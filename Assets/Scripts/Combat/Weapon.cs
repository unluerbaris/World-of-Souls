using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WOS.Core;

namespace WOS.Combat
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] float weaponDamage = 50f;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                DoDamage(collision, weaponDamage);
            }
        }

        private void DoDamage(Collider2D collision, float damage)
        {
            EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();
            enemyHealth.gotHit = true;
            enemyHealth.damageTaken = weaponDamage;
        }
    }
}
