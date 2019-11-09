using Game.Controls;
using Game.Interactions;
using UnityEngine;

public class Lava : MonoBehaviour
{
    [SerializeField] int damage = 20;

    [SerializeField] float moveUpSpeed = 1f;

    [SerializeField] float addForceWhenGhoulFalls = 5f;

    [SerializeField] ParticleSystem popVFX;

    [SerializeField] Collider2D midWall;

    [SerializeField] Collider2D leftBorder;
    [SerializeField] Collider2D rightBorder;

    [SerializeField] float maxDistanceToGetPush = 2;
    [SerializeField] float pushUpForce = 100;

    float timerForPopParticles = 0;

    [SerializeField] float cdBetweenDamage = 1.5f;
    float leftGhoulTimer = 0;
    float rightGhoulTimer = 0;


    private void Start()
    {
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), midWall);
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), leftBorder);
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), rightBorder);
    }
    void Update()
    {
        MoveUp();

        LavaPopVFX();

        leftGhoulTimer += Time.deltaTime;
        rightGhoulTimer += Time.deltaTime;
    }

    private void LavaPopVFX()
    {
        if (timerForPopParticles == 0)
        {
            float randomX = Random.Range(-14, 14); // the borders
            Vector3 randomPos = new Vector3(randomX, transform.position.y + 8, 0); // +8 is the top of the lava
            Instantiate(popVFX, randomPos, Quaternion.identity, transform);
            timerForPopParticles = Random.Range(2, 4);
        }
        else
        {
            timerForPopParticles = Mathf.Clamp(timerForPopParticles - Time.deltaTime, 0, timerForPopParticles);
        }
    }

    private void MoveUp()
    {
        transform.Translate(Vector3.up * moveUpSpeed * Time.deltaTime);
        if (Camera.main.transform.position.y - transform.position.y > 16) // 16 is the screen height
        {
            transform.position = new Vector3(transform.position.x, Camera.main.transform.position.y - 16, transform.position.z);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("LeftGhoul"))
        {
            float borderX = leftBorder.transform.position.x;
            float pointAtBorderY = transform.position.y + 8 + 10;

            float distanceToWall = leftBorder.GetComponent<Border>().GetDistanceFromPlayer();
            Vector2 pointAtBorder = new Vector2(borderX, pointAtBorderY);
            //Vector2 forceMultiplier = new Vector2(addForceWhenGhoulFalls, addForceWhenGhoulFalls);
            Vector2 directionFromGhoulToWall = pointAtBorder.normalized;
            Vector2 forceToApply = directionFromGhoulToWall * (addForceWhenGhoulFalls * (1 - (1 / distanceToWall)));
            //print(distanceToWall);
            Rigidbody2D rigidbody = collision.transform.GetComponent<Rigidbody2D>();
            if (distanceToWall < maxDistanceToGetPush && distanceToWall != Mathf.Infinity)
            {
                if (rigidbody.GetComponent<Grabber>().GetIsGrabbing())
                {
                    rigidbody.velocity = Vector2.up * pushUpForce;
                }
                else
                {
                    rigidbody.velocity = Vector2.up * pushUpForce;
                    rigidbody.GetComponent<Grabber>().GrabBorder(leftBorder.GetComponent<Border>());
                }
            }
            else
            {
                rigidbody.AddForce(forceToApply, ForceMode2D.Impulse);
                //print("i pushed! " + forceToApply);

            }
            if (leftGhoulTimer >= cdBetweenDamage)
            {
                collision.transform.GetComponent<Health>().TakeDamage(damage);
                leftGhoulTimer = 0;
            }
        }

        if (collision.transform.CompareTag("RightGhoul"))
        {
            float borderX = rightBorder.transform.position.x;
            float pointAtBorderY = transform.position.y + 8 + 10;

            float distanceToWall = rightBorder.GetComponent<Border>().GetDistanceFromPlayer();
            Vector2 pointAtBorder = new Vector2(borderX, pointAtBorderY);
            Vector2 directionFromGhoulToWall = pointAtBorder.normalized;
            Vector2 forceToApply = directionFromGhoulToWall * (addForceWhenGhoulFalls * (1 - (1 / distanceToWall)));
            //print(distanceToWall);
            Rigidbody2D rigidbody = collision.transform.GetComponent<Rigidbody2D>();
            if (distanceToWall < maxDistanceToGetPush && distanceToWall != Mathf.Infinity)
            {
                if (rigidbody.GetComponent<Grabber>().GetIsGrabbing())
                {
                    rigidbody.velocity = Vector2.up * pushUpForce;
                }
                else
                {
                    rigidbody.velocity = Vector2.up * pushUpForce;
                    rigidbody.GetComponent<Grabber>().GrabBorder(rightBorder.GetComponent<Border>());
                }
            }
            else
            {
                rigidbody.AddForce(forceToApply, ForceMode2D.Impulse);

            }
            if (rightGhoulTimer >= cdBetweenDamage)
            {
                collision.transform.GetComponent<Health>().TakeDamage(damage);
                rightGhoulTimer = 0;
            }
        }
    }
}
