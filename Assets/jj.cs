using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class jj : MonoBehaviour
{


    public float range = 20;
    public float impactForce = 150;
    public int damageAmount = 20;

    public int fireRate = 10;
    private float nextTimeToFire = 0;

    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    public int currentAmmo;
    public int maxAmmo = 10;
    public int magazineAmmo = 30;

    public float reloadTime = 2f;
    public bool isReloading;
   // StarterAssetsInputs shoot;

    public Transform fpsCam;
    [SerializeField]
    private GameObject BulletPrefab;
    [SerializeField]
    private GameObject BulletPivot;
    [SerializeField]
    private StarterAssetsInputs _input;

    // Start is called before the first frame update
    void Start()
    {
        _input = transform.root.GetComponent<StarterAssetsInputs>();
       
        currentAmmo = maxAmmo;
    }

  
    private void OnEnable()
    {
        isReloading = false;
       /// animator.SetBool("isReloading", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentAmmo == 0 && magazineAmmo == 0)
        {
          //  animator.SetBool("isShooting", false);
            return;
        }

        if (isReloading)
            return;

        //  bool isShooting = CrossPlatformInputManager.GetButton("Shoot");
        //animator.SetBool("isShooting", isShooting);
       bool isShooting = Input.GetButtonDown("Fire1");
       if (isShooting && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Fire();
        }

        if (currentAmmo == 0 && magazineAmmo > 0 && !isReloading)
        {
            StartCoroutine(Reload());
        }
    }

    private void Fire()
    {
        //AudioManager.instance.Play("Shoot");

        muzzleFlash.Play();
        currentAmmo--;
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.position + fpsCam.forward, fpsCam.forward, out hit, range))
        {
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            Enemy e = hit.transform.GetComponent<Enemy>();
            if (e != null)
            {
                e.TakeDamage(damageAmount);
                return;
            }

            Quaternion impactRotation = Quaternion.LookRotation(hit.normal);
            GameObject impact = Instantiate(impactEffect, hit.point, impactRotation);
            impact.transform.parent = hit.transform;
            Destroy(impact, 5);
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        AudioManager.instance.Play("Reload");
        //animator.SetBool("isReloading", true);
        yield return new WaitForSeconds(reloadTime);
      //  animator.SetBool("isReloading", false);
        if (magazineAmmo >= maxAmmo)
        {
            currentAmmo = maxAmmo;
            magazineAmmo -= maxAmmo;
        }
        else
        {
            currentAmmo = magazineAmmo;
            magazineAmmo = 0;
        }
        isReloading = false;
    }
}