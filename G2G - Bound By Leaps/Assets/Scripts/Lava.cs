using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    [SerializeField] float speed = 1f;

    [SerializeField] ParticleSystem popVFX;

    float timerForPopParticles = 0;
    void Update()
    {
        MoveUp();

        if (timerForPopParticles == 0)
        {
            float randomX = Random.RandomRange(-14, 14); // the borders
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
        transform.Translate(Vector3.up * speed * Time.deltaTime);
        if (Camera.main.transform.position.y - transform.position.y > 16) // 16 is the screen height
        {
            transform.position = new Vector3(transform.position.x, Camera.main.transform.position.y - 16, transform.position.z);
        }
    }
}
