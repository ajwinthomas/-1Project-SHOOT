using System;
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
    
    void Awake()
    {
        playerCamera = Camera.main;
        shotSound = GetComponent<AudioSource>();
    }
    
    void Update()
    {
        //HandleFireRate();

        if(Input.GetMouseButtonDown(0) && ammo.GetAmmoCount(ammoType) > 0) //canShoot
        {
             Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R))
            ammo.ReloadAmmoCount(ammoType);
              
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
}
