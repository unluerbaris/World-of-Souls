using UnityEngine;
using WOS.Core;

namespace WOS.Combat
{
    public class EnemyWeapon : MonoBehaviour
    {
        [SerializeField] float weaponDamage = 25f;

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                DoDamage(collision, weaponDamage);
            }
        }

        private void DoDamage(Collision2D collision, float damage)
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            playerHealth.gotHit = true;
            playerHealth.damageTaken = weaponDamage;
        }
    }
}

