using Game.Manager;
using UnityEngine;

namespace Game.Controls
{
    public class PlayerController : MonoBehaviour
    {

        [SerializeField] float minPower = 0.3f;
        [SerializeField] SpriteRenderer arrowSprite;
        [SerializeField] SpriteRenderer arrowFill;
        [SerializeField] float arrowWidthExpensionRate = 2f;
        [SerializeField] float arrowWidthExpensionRateInSlowMo = 20f;
        [SerializeField] float powerToForceFactor = 10f;
        [SerializeField] float powerToForceFactorInSlowMo = 40f;

        [SerializeField] float resistanceToTopBorder = 1f;

        private Vector2 direction;
        private float power;
        private bool isCharging;
        Rigidbody2D rb;
        private bool isEnabled;

        GameManager gm;
        void Start()
        {
            power = minPower;
            rb = GetComponent<Rigidbody2D>();
            gm = FindObjectOfType<GameManager>();
        }
        void Update()
        {
            if (isEnabled)
            {
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                direction = new Vector2(
                    mousePosition.x - transform.position.x,
                    mousePosition.y - transform.position.y
                    ).normalized;
                arrowSprite.transform.up = direction;
                arrowFill.transform.localScale = new Vector3(power, 1, 1);
                if (Input.GetMouseButton(0))
                {
                    if (gm.GetIsSLowMo())
                    {
                        power = Mathf.Clamp01(power + Time.deltaTime * arrowWidthExpensionRateInSlowMo);
                    }
                    else
                    {
                        power = Mathf.Clamp01(power + Time.deltaTime * arrowWidthExpensionRate);
                    }
                    isCharging = true;
                }
                else
                {
                    if (isCharging)
                    {
                        Vector3 force = direction * power * powerToForceFactor;
                        if (gm.GetIsSLowMo()) { rb.velocity = Vector2.zero; force = direction * power * powerToForceFactorInSlowMo; }
                        rb.AddForce(force, ForceMode2D.Impulse);// AddRelativeForce(force, ForceMode2D.Impulse);
                        isCharging = false;
                        gm.ToggleSlowMo(false);
                        GetComponent<Grabber>().RealeseGrab();
                    }
                    power = 0;
                }
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.CompareTag("TopBorder"))
            {
                rb.AddForce(-(new Vector2(0, resistanceToTopBorder)));
            }
        }

        public void EnableController()
        {
            isEnabled = true;
            arrowSprite.enabled = true;
            arrowFill.enabled = true;
        }
        public void DisableController()
        {
            isEnabled = false;
            arrowSprite.enabled = false;
            arrowFill.enabled = false;
        }
    }
}