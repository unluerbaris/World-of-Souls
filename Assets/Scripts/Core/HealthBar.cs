using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WOS.Core
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] GameObject bar;

        // scale the health bar between 0 and 1 
        public void ScaleHealthBar(float currentHealth, float startHealth)
        {
            float scaleValue = currentHealth / startHealth;
            bar.transform.localScale = new Vector2(scaleValue,
                                                   bar.transform.localScale.y);
        }
    }
}
