using Game.Controls;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Interactions
{
    public class Border : MonoBehaviour
    {
        [SerializeField] float slowMoCD = .5f;
        public UnityEvent onTouchBorder;
        public UnityEvent enterSlowMoArea;

        private float timer = 0;

        private void Update()
        {
            timer -= Time.deltaTime;
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform.root.CompareTag("Player"))
            {
                onTouchBorder.Invoke();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.transform.root.CompareTag("Player"))
            {
                print(collision.name);
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
    }
}
