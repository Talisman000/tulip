using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour
{
    [SerializeField] GameObject tulip;
    [SerializeField] float bloomTime = 20;
    float bloomTimer = 0;
    private void Update()
    {
        bloomTimer += Time.deltaTime;
        if (bloomTimer > bloomTime)
        {
            Instantiate(tulip, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
    private void OnParticleCollision(GameObject other)
    {
        bloomTimer += 4;
    }
}
