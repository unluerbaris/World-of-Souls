using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WOS.Core;

namespace WOS.Combat
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] float weaponDamage = 50f;

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.tag == "Enemy")
            {
                DoDamage(collider, weaponDamage);
            }
        }

        private void DoDamage(Collider2D collider, float damage)
        {
            EnemyHealth enemyHealth = collider.GetComponent<EnemyHealth>();
            enemyHealth.gotHit = true;
            enemyHealth.damageTaken = weaponDamage;
        }
    }
}
