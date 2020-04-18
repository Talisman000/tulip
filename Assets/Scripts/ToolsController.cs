using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolsController : MonoBehaviour
{
    [SerializeField] List<GameObject> seed;
    [SerializeField] List<Vector2> seedInstancePosList;
    [SerializeField] float seedCoolTime = 3;
    private float seedCoolTimer = 0;
    private bool enableSeed = true;
    [SerializeField] ParticleSystem water;
    [SerializeField] ParticleSystem weapon;
    [SerializeField] int maxWeaponMagazine = 3;
    private int weaponMagazine;
    [SerializeField] float weaponCoolTime = 0.5f;
    private float weaponCoolTimer = 0;
    private void Start()
    {
        weaponMagazine = maxWeaponMagazine;
    }
    private void Update()
    {
        seedCoolTimer += Time.deltaTime;
        if (seedCoolTimer > seedCoolTime)
        {
            enableSeed = true;
        }
        if (weaponMagazine == 0)
        {
            weaponCoolTimer += Time.deltaTime;
            if (weaponCoolTimer > weaponCoolTime)
            {
                weaponMagazine = maxWeaponMagazine;
                weaponCoolTimer = 0;
            }
        }
        if(!GameManager.isGame.Value) water.Stop();
    }
    public void WaterPlay(bool flag)
    {
        if (flag)
        {
            water.Play();

        }
        else
        {
            water.Stop();
        }
    }
    public void WeaponAttack(float angle)
    {
        if (weaponMagazine == 0) return;
        weapon.transform.rotation = Quaternion.Euler(-angle, 90, 0);
        weapon.Play();
        weaponMagazine--;
    }
    public void InstantiateSeed()
    {
        if (!enableSeed) return;
        foreach (var pos in seedInstancePosList)
        {
            GameObject obj = Instantiate(seed[Random.Range(0, seed.Count)], (Vector2)gameObject.transform.position + pos, Quaternion.identity);
        }
        enableSeed = false;
        seedCoolTimer = 0;
    }
}
