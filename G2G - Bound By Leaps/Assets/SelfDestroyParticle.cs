using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroyParticle : MonoBehaviour
{
    ParticleSystem particleSystem;
    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!particleSystem.IsAlive())
        {
            Destroy(gameObject);
        }
    }
}
