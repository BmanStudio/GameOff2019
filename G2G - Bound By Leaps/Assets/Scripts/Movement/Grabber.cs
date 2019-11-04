using Game.Interactions;
using UnityEngine;

namespace Game.Controls
{
    public class Grabber : MonoBehaviour
    {
        private bool isGrabbing = false;

        Rigidbody2D rb;

        public Border border { get; private set; }

        float lastGrabbed = Mathf.Infinity;
        [SerializeField] float grabCD = 0.5f;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }
        void Update()
        {
            if (isGrabbing)
            {
                rb.velocity = Vector2.zero;
            }
                lastGrabbed += Time.deltaTime;
        }

        public void GrabBorder(Border border)
        {
            this.border = border;
            isGrabbing = true;
            lastGrabbed = 0;
            //print("im here 2");
        }

        public void RealeseGrab()
        {
            isGrabbing = false;
        }

        public bool GetIsGrabbing()
        {
            return isGrabbing;
        }

        public bool GetIsGrabCD()
        {
            return lastGrabbed <= grabCD;
        }
    }
}