using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
        if (other.tag == "seed")
        {
            Debug.Log("seed");
        }
        Debug.Log(other.name);
    }
}
