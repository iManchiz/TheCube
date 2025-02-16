using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController characterController;

    public float speed = 10f;

    public Transform groundCheck;
    public float sphereRadius = 0.3f;
    public LayerMask groundMask;
    bool isGrounded;
    Vector3 velocity;
    public float gravity = -9.81f;
    public float jumpHeight = 3;

    public Transform playerCamera;
    public float mouseSensitivity = 100f;
    float xRotation = 0f;

    public AudioClip movementSound; // AudioClip para el sonido de movimiento
    private AudioSource audioSource; // Referencia al AudioSource para reproducir sonido

    private Animator animator; // Referencia al Animator para controlar animaciones

    void Start()
    {
        // Esconder y bloquear el cursor en el centro de la pantalla
        Cursor.lockState = CursorLockMode.Locked;

        // Obtener referencia al AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Obtener referencia al Animator
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("No se encontró el componente Animator.");
        }
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, sphereRadius, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        // Reproducir sonido al moverse en cualquier dirección
        if ((Mathf.Abs(x) > 0 || Mathf.Abs(z) > 0) && !audioSource.isPlaying && movementSound != null)
        {
            audioSource.PlayOneShot(movementSound);
        }

        // Activar animaciones según el movimiento
        if (animator != null)
        {
            bool isWalking = (z > 0);
            bool isWalkingBack = (z < 0);
            bool isWalkingLeft = (x < 0);
            bool isWalkingRight = (x > 0);

            animator.SetBool("isWalking", isWalking);
            animator.SetBool("isWalkingBack", isWalkingBack);
            animator.SetBool("isWalkingLeft", isWalkingLeft);
            animator.SetBool("isWalkingRight", isWalkingRight);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }

        characterController.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        characterController.Move(velocity * Time.deltaTime);

        // Movimiento de la cámara con el ratón
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    public void IncreaseSpeed()
    {
        speed += 1f;
    }
}
