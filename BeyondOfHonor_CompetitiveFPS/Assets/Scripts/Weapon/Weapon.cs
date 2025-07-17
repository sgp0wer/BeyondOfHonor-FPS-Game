using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [Header("General Settings")]
    public string weaponName = "Unnamed Weapon";
    public int maxAmmo = 10;
    public float damage = 10f;

    protected int currentAmmo;

    protected virtual void Start()
    {
        currentAmmo = maxAmmo;
    }

    public abstract void Fire();
    public abstract void Reload();
}
