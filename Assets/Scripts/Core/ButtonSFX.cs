using UnityEngine;

namespace WOS.Core
{
    public class ButtonSFX : MonoBehaviour
    {
        AudioManager audioManager;

        void Start()
        {
            audioManager = FindObjectOfType<AudioManager>();
        }

        public void PlayHoverSFX()
        {
            audioManager.PlaySound("Hover");
        }

        public void PlayClickSFX()
        {
            audioManager.PlaySound("Click");
        }
    }
}
