using System.Collections;
using UnityEngine;

public class Ammo : MonoBehaviour
{

    [System.Serializable]
    private class AmmoSlot
    {
        public string name;
        public AmmoType ammoType;
        public int ammoCount;
        public int initialAmmoCount;
    }

    [SerializeField] AmmoSlot[] ammoSlot;


    AmmoSlot GetAmmoSlot(AmmoType ammoType)
    {
        foreach (AmmoSlot slot in ammoSlot)
        {
            if(slot.ammoType == ammoType) return slot;
        }
        return null;
    }
    public int GetAmmoCount(AmmoType ammoType)
    {
        return GetAmmoSlot(ammoType).ammoCount;
    }

    public void ReduceAmmoCount(AmmoType ammoType)
    {
        GetAmmoSlot(ammoType).ammoCount--;
    }

    public void ReloadAmmoCount(AmmoType ammoType)
    {
        AmmoSlot slot = GetAmmoSlot(ammoType);
        slot.ammoCount = slot.initialAmmoCount;
    }

    


}
