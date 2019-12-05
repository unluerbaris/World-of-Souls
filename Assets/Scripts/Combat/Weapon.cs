using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WOS.Core;

namespace WOS.Combat
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] float weaponDamage = 50;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                DoDamage(collision, weaponDamage);
            }
        }

        private void DoDamage(Collider2D collision, float damage)
        {
            Health enemyHealth = collision.GetComponent<Health>();
            enemyHealth.TakeDamage(damage);
        }
    }
}
