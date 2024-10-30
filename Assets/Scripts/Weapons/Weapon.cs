using System;
using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    [Header("ShootingRayCast")]
    [SerializeField] LayerMask hittableLayer;
    [SerializeField] float weaponRange;
    

    [Header("Game Feel")]
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject bulletHole;
    [SerializeField] float bulletHolePositionOffset;
    [SerializeField] AudioSource shotSound;

    [Header("Handle Firerate")]
    [SerializeField] float fireRate;
    float thresholdTime;
    [Header("Ammunition")]
    [SerializeField] AmmoType ammoType;
    [SerializeField] Ammo ammo;

    Camera playerCamera;
    Animator anim;
    
    
    void Awake()
    {
        playerCamera = Camera.main;
        shotSound = GetComponent<AudioSource>();
        anim = GetComponentInParent<Animator>();
        
    }
    
    void Update()
    {
        //HandleFireRate();

        if(Input.GetMouseButtonDown(0) && ammo.GetAmmoCount(ammoType) > 0) //canShoot
        {
             Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
        }
            
              
    }

    private void Shoot()
    {
        ammo.ReduceAmmoCount(ammoType);
         HandleRayCast();
         HandleMuzzleFlash();
    }

    private void HandleRayCast()
    {
        if(Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward,out RaycastHit hit,weaponRange,hittableLayer))
        {
            Instantiate(bulletHole, hit.point + (hit.normal * bulletHolePositionOffset), Quaternion.LookRotation(hit.normal));
        }
        else
        {
            Debug.Log("not hit an wall");
        }
    }

    private void HandleMuzzleFlash()
    {
            muzzleFlash.Play();
            HandleShotSound(); 
    }

    private void HandleShotSound()
    {
        shotSound.PlayOneShot(shotSound.clip);
    }
    private IEnumerator Reload()
    {
        anim.SetBool("IsReload", true);
        yield return new WaitForSeconds(1f);
        ammo.ReloadAmmoCount(ammoType);
        anim.SetBool("IsReload", false);
    }
}
