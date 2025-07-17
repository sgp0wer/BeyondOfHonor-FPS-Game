using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public Weapon primaryWeapon;
    public Weapon secondaryWeapon;
    public Weapon meleeWeapon;

    private Weapon currentWeapon;

   void Start()
{
    if (primaryWeapon == null)
    {
        if (secondaryWeapon != null)
        {
            EquipWeapon(secondaryWeapon);
        }
        else
        {
            // Здесь логика по выдаче дефолтного оружия (например, пистолет)
            Debug.LogWarning("Нет ни основного, ни вторичного оружия. Выдаём стандартный пистолет...");
        }
    }
    else
    {
        EquipWeapon(primaryWeapon); // если есть основное — используем его
    }
}

    void Update()
    {
        if (Input.GetButton("Fire1"))
            currentWeapon?.Fire();

        if (Input.GetKeyDown(KeyCode.R))
            currentWeapon?.Reload();

        if (Input.GetKeyDown(KeyCode.Alpha1))
            EquipWeapon(primaryWeapon);

        if (Input.GetKeyDown(KeyCode.Alpha2))
            EquipWeapon(secondaryWeapon);

        if (Input.GetKeyDown(KeyCode.Alpha3))
            EquipWeapon(meleeWeapon);
    }

    void EquipWeapon(Weapon weapon)
    {
        if (weapon == null)
            return;

        currentWeapon = weapon;
        Debug.Log($"Сменили оружие на: {weapon.weaponName}");

        // Здесь позже можно:
        // - прятать/показывать префабы
        // - проигрывать анимацию смены
        // - обновлять UI
        }
}