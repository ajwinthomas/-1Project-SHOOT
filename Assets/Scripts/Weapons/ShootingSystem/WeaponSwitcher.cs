


using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    int currentWeaponIndex = 0;
    int previousWeaponIndex;
    private WeaponZoom weaponZoom;

    [Header("Reeload animation sound triggering")]
    [SerializeField] AudioSource reloadSound;

    private void Start()
    {
        weaponZoom = GetComponentInChildren<WeaponZoom>();
    }


    void Update()
    {
        if(weaponZoom != null && weaponZoom.isScoping)
        {
            return;
        }


        previousWeaponIndex = currentWeaponIndex;

        HandleKeyPress();

        HandleScrollWheel();

        if(previousWeaponIndex != currentWeaponIndex)
        {
            SwitchWeapon();
        }
    }

    private void HandleScrollWheel()
    {
        if(Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            currentWeaponIndex = (currentWeaponIndex >= transform.childCount - 1) ? 0 : currentWeaponIndex + 1;
        }
        else if(Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            currentWeaponIndex = (currentWeaponIndex <= 0) ? transform.childCount -1 : currentWeaponIndex - 1;
        }
    }

    private void HandleKeyPress()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentWeaponIndex = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentWeaponIndex = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentWeaponIndex = 2;
        }
    }

    private void SwitchWeapon()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(i==currentWeaponIndex);
        }
    }

    public void PlayReloadSound()
    {
        reloadSound.PlayOneShot(reloadSound.clip);
    }
}
