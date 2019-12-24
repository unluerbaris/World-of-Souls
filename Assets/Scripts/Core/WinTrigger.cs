using UnityEngine;

namespace WOS.Core
{
    public class WinTrigger : MonoBehaviour
    {
        bool isCollided = false;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player" && !isCollided)
            {
                isCollided = true;
                StartCoroutine(GetComponentInParent<GameSession>().LoadWinScreen());
            }
        }
    }
}
