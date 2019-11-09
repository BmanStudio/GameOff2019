using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronSparklesManager : MonoBehaviour
{
    [SerializeField] float minVelocityToPlay = .3f;

    [SerializeField] ParticleSystem leftPS;
    [SerializeField] ParticleSystem rightPS;


    Rigidbody2D rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (rigidbody.velocity.x > minVelocityToPlay)
        {
            if (leftPS.isPlaying) { return; }
            else { leftPS.Play(); }

        }

        if (rigidbody.velocity.x < - minVelocityToPlay)
        {
            if (rightPS.isPlaying) { return; }
            else { rightPS.Play(); }
        }
    }
}
