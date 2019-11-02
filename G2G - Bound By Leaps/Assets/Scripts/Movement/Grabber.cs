using Game.Interactions;
using UnityEngine;

namespace Game.Controls
{
    public class Grabber : MonoBehaviour
    {
        private bool isGrabbing = false;

        Rigidbody2D rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }
        void FixedUpdate()
        {
            if (isGrabbing)
            {
                rb.velocity = Vector2.zero;
            }
        }

        public void GrabBorder(Border border)
        {
            isGrabbing = true;
        }

        public void RealeseGrab()
        {
            isGrabbing = false;
        }
    }
}