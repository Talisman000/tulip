using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
    private void Update()
    {

    }
    private void OnParticleCollision(GameObject other)
    {
        if (other.tag == "enemy")
        {
            Debug.Log("enemy");
        }
    }
}
