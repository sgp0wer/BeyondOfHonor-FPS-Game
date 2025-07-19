using UnityEngine;

public class WeaponEquipManager : MonoBehaviour
{
    [Header("Ссылки")]
    public GameObject weaponPrefab;               // Префаб оружия
    public Transform weaponHolder;                // Куда оружие будет прикрепляться (например, к руке или socket на спине)
    public WeaponIKHandler ikHandler;             // Ссылка на IK скрипт

    private GameObject currentWeaponInstance;

    void Start()
    {
        EquipWeapon();
    }

    void EquipWeapon()
    {
        // Спавним и прикрепляем оружие
        currentWeaponInstance = Instantiate(weaponPrefab, weaponHolder);
        currentWeaponInstance.transform.localPosition = Vector3.zero;
        currentWeaponInstance.transform.localRotation = Quaternion.identity;

        WeaponOffset offset = currentWeaponInstance.GetComponent<WeaponOffset>();
        if (offset != null)
        {
            currentWeaponInstance.transform.localPosition = offset.localPositionOffset;
            currentWeaponInstance.transform.localRotation = Quaternion.Euler(offset.localRotationOffset);
            currentWeaponInstance.transform.localScale = offset.localScale;
        }

        // Находим точку захвата левой рукой
        Transform leftHandTarget = currentWeaponInstance.transform.Find("LeftHandTarget");

        if (leftHandTarget != null)
        {
            ikHandler.leftHandTarget = leftHandTarget;
        }
        else
        {
            Debug.LogWarning("LeftHandTarget не найден на оружии.");
        }
    }
}