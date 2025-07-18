using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 5f;
    public float sprintSpeed = 9f;
    public float slideSpeed = 12f;
    public float jumpHeight = 1.5f;
    public float gravity = -9.81f;

    [Header("Slide Settings")]
    public float slideDuration = 1.0f;
    public float slideCooldown = 2.0f; // Настраиваемый кулдаун

    private float slideTimer = 0f;
    private float slideCooldownTimer = 0f;
    private bool isSliding = false;
    private bool canSlide = true;
    private Vector3 slideDirection;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private bool isSprinting = false;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Обновляем статус касания земли
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        // Ввод
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = Vector3.zero;

        // === ОБНОВЛЕНИЕ КУЛДАУНА ===
        if (!canSlide)
        {
            slideCooldownTimer -= Time.deltaTime;
            if (slideCooldownTimer <= 0f)
                canSlide = true;
            Debug.Log("Подкат активирован");
        }

        if (!isSliding)
        {
            // Расчёт движения
            move = transform.right * x + transform.forward * z;

            // Спринт только если двигаемся ВПЕРЁД и не в бок
            isSprinting = Input.GetKey(KeyCode.LeftShift) && z > 0 && x == 0 && isGrounded;

            float currentSpeed = isSprinting ? sprintSpeed : walkSpeed;
            controller.Move(move * currentSpeed * Time.deltaTime);

            // Условие старта подката
            if (isSprinting && Input.GetKeyDown(KeyCode.LeftControl) && canSlide)
            {
                StartSlide();
            }
        }
        else
        {
            // Подкат: движение по сохранённому направлению
            controller.Move(slideDirection * slideSpeed * Time.deltaTime);
            slideTimer -= Time.deltaTime;

            if (slideTimer <= 0f)
            {
                EndSlide();
            }
        }

        // Прыжок
        if (Input.GetButtonDown("Jump") && isGrounded && !isSliding)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Гравитация
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void StartSlide()
    {
        isSliding = true;
        slideTimer = slideDuration;
        canSlide = false;
        slideCooldownTimer = slideCooldown;

        slideDirection = transform.forward;
    }

    void EndSlide()
    {
        isSliding = false;
        // Возврат управления произойдёт в Update
    }
}
