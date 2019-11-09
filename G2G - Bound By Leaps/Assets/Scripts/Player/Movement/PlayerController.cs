using Game.Interactions;
using Game.Manager;
using UnityEngine;

namespace Game.Controls
{
    public class PlayerController : MonoBehaviour
    {

        //[SerializeField] float minPower = 0.3f;
        [SerializeField] GameObject arrowGameObject;
        [SerializeField] SpriteMask arrowFillMask;
        [SerializeField] float arrowWidthExpensionRate = 2f;
        //[SerializeField] float arrowWidthExpensionRateInSlowMo = 20f;
        [SerializeField] float powerToForceFactor = 10f;
        [SerializeField] float powerToForceFactorInSlowMo = 40f;

        [SerializeField] float resistanceToTopBorder = 1f;

        [SerializeField] bool isLeftPlayer = true;


        private Vector2 direction;
        private float power;
        private bool isCharging;
        Rigidbody2D rb;
        public bool isEnabled;
        private Grabber grabber;

        private float noPowerFillPosY = -2.1f;

        private AnimatorManager animatorManager;

        public bool IsAtBorder { get; private set; }

        private bool canGrabBorder;

        GameManager gm;
        void Start()
        {
            //power = minPower;
            animatorManager = GetComponent<AnimatorManager>();
            grabber = GetComponent<Grabber>();
            rb = GetComponent<Rigidbody2D>();
            gm = FindObjectOfType<GameManager>();

            if (!isLeftPlayer)
            {
                rb.AddForce(new Vector2(transform.position.x + 8, transform.position.y + 8) * 250);
            }
            else
            {
                rb.AddForce(new Vector2(transform.position.x - 8, transform.position.y + 8) * 250);
            }
        }
        void Update()
        {
            if (isEnabled)
            {
                Vector2 velocityDirection = rb.velocity.normalized;
                //print(velocityDirection);
                if (gm.GetIsSLowMo())
                {
                    gm.ActiveVignetteFXSlowMotion(gameObject);
                }
                if (grabber.GetIsGrabbing())
                {
                    gm.ToggleSlowMo(false);
                }

                if (canGrabBorder)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        //print("im here1");
                        grabber.GrabBorder(grabber.border);
                        canGrabBorder = false;
                    }
                }

                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                direction = new Vector2(
                    mousePosition.x - transform.position.x,
                    mousePosition.y - transform.position.y
                    ).normalized;
                arrowGameObject.transform.up = direction;
                arrowGameObject.transform.position = rb.worldCenterOfMass;
                arrowFillMask.transform.localPosition = new Vector2(0, 2.5f * power - 2.1f);

                if (grabber.GetIsGrabbing() || gm.GetIsSLowMo())
                {
                    if (Input.GetMouseButton(0))
                    {
                        if (grabber.GetIsGrabCD() && !gm.GetIsSLowMo() && isCharging == false) { return; }
                        power = Mathf.Clamp01(power + Time.unscaledDeltaTime * arrowWidthExpensionRate);
                        if (!isCharging)
                        {
                            animatorManager.pullAnimationTrigger();
                        }
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
                            animatorManager.pushAnimationTrigger();
                        }
                        power = 0;
                    }
                }
            }
            else
            {
                power = 0;
                isCharging = false;
            }
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.transform.GetComponent<Border>() != null)
            {
                if (collision.transform.GetComponent<Border>() == grabber.border)
                {
                    canGrabBorder = false;
                    //IsAtBorder = true;
                    //print("always here");
                    float x;
                    float y = rb.velocity.y;
                    if (isLeftPlayer)
                    {
                        x = Mathf.Max(rb.velocity.x, 0);
                        //print("2");
                    }
                    else
                    {
                        x = Mathf.Min(rb.velocity.x, 0);
                    }
                    rb.velocity = new Vector2(x, y);
                    if (!grabber.GetIsGrabbing())
                    {
                        canGrabBorder = true;
                        /*                        //print("3");
                                                if (Input.GetMouseButtonDown(0))
                                                {
                                                    //print("im here1");
                                                    grabber.GrabBorder(grabber.border);
                                                }*/
                    }
                    //else if (grabber.GetIsGrabbing()) { print("wtf"); }
                    //else { print("WTFFFF"); }
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.transform.GetComponent<Border>() != null)
            {
                canGrabBorder = false;
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            //print("me too");
            if (collision.CompareTag("TopBorder"))
            {
                rb.AddForce(-(new Vector2(0, resistanceToTopBorder)));
            }
        }

        public void EnableController()
        {
            isEnabled = true;
            arrowGameObject.SetActive(true);
            //arrowFillMask.enabled = true;
        }
        public void DisableController()
        {
            isEnabled = false;
            arrowGameObject.SetActive(false);
            //arrowFillMask.enabled = false;
        }
    }
}

/*var playerCurrentVelocity = navMeshAgent.velocity;
Vector3 localVelocity = transform.InverseTransformDirection(playerCurrentVelocity);
float velocitySpeed = localVelocity.z;
GetComponent<Animator>().SetFloat("forwardSpeed", velocitySpeed);*/
