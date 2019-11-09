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
            if (isGrabbing) { return; }
            this.border = border;
            isGrabbing = true;
            lastGrabbed = 0;
            GetComponent<AnimatorManager>().IdleAnimationTrigger();
            //print("im here3" + name) ;
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