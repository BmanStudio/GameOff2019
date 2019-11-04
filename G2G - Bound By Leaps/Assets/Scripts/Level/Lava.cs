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
        if (collision.transform.CompareTag("RightGhoul"))
        {
            float borderX = rightBorder.transform.position.x;
            float pointAtBorderY = transform.position.y + 8 + 6;

            float distanceToWall = (Mathf.Abs(rightBorder.transform.position.x) - Mathf.Abs(collision.transform.position.x));

            Vector2 directionFromGhoulToWall = new Vector2(borderX, pointAtBorderY) + (Vector2.up * (distanceToWall * 4));
            Rigidbody2D rigidbody = collision.transform.GetComponent<Rigidbody2D>();
            if (distanceToWall < .6) // todo change this shit
            {
                rigidbody.velocity = Vector2.up * 50;
            }
            else
            {
                rigidbody.AddForce(directionFromGhoulToWall * addForceWhenGhoulFalls);

            }
            if (rightGhoulTimer >= cdBetweenDamage)
            {
                collision.transform.GetComponent<Health>().TakeDamage(damage);
                rightGhoulTimer = 0;
            }
        }

        if (collision.transform.CompareTag("LeftGhoul"))
        {
            float borderX = leftBorder.transform.position.x;
            float pointAtBorderY = transform.position.y + 8 + 6;

            float distanceToWall = (Mathf.Abs(leftBorder.transform.position.x) - Mathf.Abs(collision.transform.position.x));

            Vector2 directionFromGhoulToWall = new Vector2(borderX, pointAtBorderY) + (Vector2.up * (distanceToWall * 4));
            Rigidbody2D rigidbody = collision.transform.GetComponent<Rigidbody2D>();
            if (distanceToWall < .6) // todo change this shit
            {
                rigidbody.velocity = Vector2.up * 50;
            }
            else
            {
                rigidbody.AddForce(directionFromGhoulToWall * addForceWhenGhoulFalls);

            }

            if (leftGhoulTimer >= cdBetweenDamage)
            {
                collision.transform.GetComponent<Health>().TakeDamage(damage);
                leftGhoulTimer = 0;
            }
        }
    }
}
