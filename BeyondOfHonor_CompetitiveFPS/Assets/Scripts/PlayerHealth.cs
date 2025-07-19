using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 100f;
    private float currentHealth;

    [Header("Fall Damage Settings")]
    public float minFallVelocity = -10f;     // скорость, после которой начинается урон
    public float maxFallVelocity = -25f;     // при этой скорости получаем 100% урона
    public float maxFallDamage = 100f;

    private CharacterController controller;
    private Vector3 previousVelocity;
    private PlayerMovement movementScript;

    void Start()
    {
        currentHealth = maxHealth;
        controller = GetComponent<CharacterController>();
        movementScript = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        ApplyFallDamageCheck();
    }

    void ApplyFallDamageCheck()
    {
        // Проверка: стоим ли на земле и падаем ли
        if (controller.isGrounded && previousVelocity.y < minFallVelocity)
        {
            float t = Mathf.InverseLerp(minFallVelocity, maxFallVelocity, previousVelocity.y);
            float damage = Mathf.Lerp(0f, maxFallDamage, t);

            TakeDamage(damage);
        }

        previousVelocity = movementScript != null ? movementScript.GetVelocity() : Vector3.zero;
    }

    public void TakeDamage(float amount)
    {
        if (amount <= 0f) return;

        currentHealth -= amount;
        Debug.Log($"Получен урон: {amount}, текущее здоровье: {currentHealth}");

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Игрок мёртв");
        // TODO: реализовать смерть (анимация, рестарт, UI и т.п.)
    }

    public float GetHealthPercent() => currentHealth / maxHealth;
}
