using Game.Interactions;
using Game.Manager;
using UnityEngine;

namespace Game.Controls
{
    public class PlayerController : MonoBehaviour
    {

        //[SerializeField] float minPower = 0.3f;
        [SerializeField] SpriteRenderer arrowSprite;
        [SerializeField] SpriteRenderer arrowFill;
        [SerializeField] float arrowWidthExpensionRate = 2f;
        //[SerializeField] float arrowWidthExpensionRateInSlowMo = 20f;
        [SerializeField] float powerToForceFactor = 10f;
        [SerializeField] float powerToForceFactorInSlowMo = 40f;

        [SerializeField] float resistanceToTopBorder = 1f;

        private Vector2 direction;
        private float power;
        private bool isCharging;
        Rigidbody2D rb;
        public bool isEnabled;
        private Grabber grabber;

        GameManager gm;
        void Start()
        {
            //power = minPower;
            grabber = GetComponent<Grabber>();
            rb = GetComponent<Rigidbody2D>();
            gm = FindObjectOfType<GameManager>();

            if (!isEnabled)
            {
                rb.AddForce(Vector3.right * 200);
            }
            else
            {
                rb.AddForce(Vector3.left * 300);
            }
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

                if (grabber.GetIsGrabbing() || gm.GetIsSLowMo())
                {
                    if (Input.GetMouseButton(0))
                    {
                        if (grabber.GetIsGrabCD() && !gm.GetIsSLowMo() && isCharging == false) { return; }
                        power = Mathf.Clamp01(power + Time.unscaledDeltaTime * arrowWidthExpensionRate);
                        isCharging = true;
                    }
                    else
                    {
                        if (isCharging)
                        {
                            Vector3 force = direction * power * powerToForceFactor;
                            if (gm.GetIsSLowMo())
                            {
                                rb.velocity = Vector2.zero; force = direction * power * powerToForceFactorInSlowMo;
                            }
                            rb.AddForce(force, ForceMode2D.Impulse);
                            isCharging = false;
                            gm.ToggleSlowMo(false);
                            GetComponent<Grabber>().RealeseGrab();
                        }
                        power = 0;
                    }
                }
            }
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.transform.GetComponent<Border>() != null)
            {
                if (collision.transform.GetComponent<Border>() == grabber.border)
                {
                    if (!grabber.GetIsGrabbing())
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            //print("im here1");
                            grabber.GrabBorder(grabber.border);
                        }
                    }
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