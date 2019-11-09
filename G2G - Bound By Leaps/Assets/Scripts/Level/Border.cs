using Game.Controls;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Interactions
{
    public class Border : MonoBehaviour
    {
        [SerializeField] float distanceToResetPlayer = 0.1f;
        [SerializeField] float slowMoCD = .5f;
        public UnityEvent onTouchBorder;
        public UnityEvent enterSlowMoArea;

        private float timer = 0;

        Transform player;

        float distance = Mathf.Infinity;


        private void Update()
        {
            if (player != null)
            {
                distance = (Mathf.Abs(transform.position.x) - Mathf.Abs(player.position.x));
                if (distance > distanceToResetPlayer)
                {
                    player = null;
                    distance = Mathf.Infinity;
                }
            }

            timer -= Time.deltaTime;
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform.root.CompareTag("Player"))
            {
                if (player == null)
                {
                    onTouchBorder.Invoke();
                    player = collision.transform;
                }

            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.transform.root.CompareTag("Player"))
            {
                if (collision.GetComponent<PlayerController>() != null)
                {
                    if (collision.GetComponent<PlayerController>().isEnabled)
                    {
                        if (timer <= 0)
                        {
                            enterSlowMoArea.Invoke();
                            timer = slowMoCD;
                        }
                    }
                }
            }
        }

        public float GetDistanceFromPlayer()
        {
            return distance;
        }
    }
}
