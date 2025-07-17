using UnityEngine;
using TMPro;
using System.Collections;
public class Gun : MonoBehaviour
{
    
    [Header("Gun Settings")]
    public float range = 100f;
    public float damage = 25f;

    [Header("Firing")]
    public float fireRate = 0.2f; // время между выстрелами

    private float nextTimeToFire = 0f;

    [Header("Ammo")]
    public int maxAmmo = 10;
    private int currentAmmo;
    public float reloadTime = 1.5f;
    private bool isReloading = false;

    [Header("References")]
    public Camera fpsCamera;
    public TMP_Text ammoText;

    [Header("Audio")]
    public AudioSource gunAudio;
    public AudioClip shootSound;
    void Start()
    {
        currentAmmo = maxAmmo;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + fireRate;
                Shoot();
            }
        }
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + fireRate;
            Shoot();
        }

        if (isReloading)
            return;


        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        {
            //Счётчик патронов
            ammoText.text = currentAmmo + " / " + maxAmmo;
        }

    }

    IEnumerator Reload()
    {
        isReloading = true;
        ammoText.text = "RELOADING";
        Debug.Log("Перезарядка...");
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        isReloading = false;
    }

    void Shoot()
    {
        if (shootSound != null && gunAudio != null)
        {
            gunAudio.pitch = Random.Range(0.95f, 1.05f);
            gunAudio.PlayOneShot(shootSound);
        }
        currentAmmo--;
        Ray ray = fpsCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f)); // центр экрана
        if (Physics.Raycast(ray, out RaycastHit hit, range))
        {
            Debug.Log("Попал в " + hit.collider.name);

            // Проверим, есть ли у цели скрипт на получение урона
            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }

            // Временный эффект попадания
            Debug.DrawLine(ray.origin, hit.point, Color.red, 1f);

            IEnumerator Reload()
            {
                isReloading = true;
                Debug.Log("Перезарядка...");
                yield return new WaitForSeconds(reloadTime);
                currentAmmo = maxAmmo;
                isReloading = false;
            }
        }
    }
}