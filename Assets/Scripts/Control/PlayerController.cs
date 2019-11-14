using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WOS.Movement;

namespace WOS.Control
{
    public class PlayerController : MonoBehaviour
    {
        Mover mover;

        private void Start()
        {
            mover = GetComponent<Mover>();
        }

        void Update()
        {
            float controlThrow = Input.GetAxis("Horizontal"); //-1 to +1
            mover.Walk(controlThrow);
        }
    }
}
