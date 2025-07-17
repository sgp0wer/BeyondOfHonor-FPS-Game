using UnityEngine;

public class MeleeWeapon : Weapon
{
    public float attackRange = 2f;

    public override void Fire()
    {
        Debug.Log($"{weaponName} наносит удар ближнего боя");

        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, attackRange))
        {
            Target target = hit.collider.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
        }
    }

    public override void Reload()
    {
        // Нож не перезаряжается
    }
}