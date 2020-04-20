using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour
{
    [SerializeField] GameObject tulip;
    [SerializeField] float bloomTime = 20;
    float bloomTimer = 0;
    AudioSource audioSource;
    [SerializeField] AudioClip plantSE;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(plantSE);
    }
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
