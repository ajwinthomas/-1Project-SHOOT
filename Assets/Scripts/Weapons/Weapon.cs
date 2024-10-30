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


    Camera playerCamera;
    
    void Awake()
    {
        playerCamera = Camera.main;
        shotSound = GetComponent<AudioSource>();
    }
    
    void Update()
    {

        if(Input.GetMouseButtonDown(0) && Time.time >=thresholdTime)
        {
             Shoot();
            thresholdTime = Time.time + fireRate;
        }
              
    }

    private void Shoot()
    {
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
