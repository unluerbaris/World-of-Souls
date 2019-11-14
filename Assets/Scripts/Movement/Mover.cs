using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WOS.Movement
{
    public class Mover : MonoBehaviour
    {
        [SerializeField] float walkSpeed = 6f;

        Rigidbody2D rb2D;

        private void Start()
        {
            rb2D = GetComponent<Rigidbody2D>();
        }

        public void Walk(float controlThrow)
        {
            Vector2 characterVelocity = new Vector2(controlThrow * walkSpeed, rb2D.velocity.y);
            rb2D.velocity = characterVelocity;
        }
    }
}

