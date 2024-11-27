using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;    // Velocidad de movimiento horizontal
    public float currentSpeed;
    public float jumpForce = 5f;    // Fuerza del salto
    public float gravityScale = 1.5f; // Factor de gravedad personalizado
    public Transform cameraTransform; // Transform de la cámara
    public int maxJumps = 2; // Número máximo de saltos permitidos (doble salto)
    public float runMultiplier = 1.5f; // Multiplicador de velocidad al correr con Shift

    private Rigidbody rb;
    private bool isGrounded = true; // Indica si el jugador está tocando el suelo
    private int jumpCount = 0; // Contador de saltos realizados
    private float moveInputX;
    private float moveInputZ;

    private AudioSource[] audioSources;
    public AudioSource walkSound;
    public AudioSource jumpSound;
    float stepSoundTimer = 0f;
    public float stepSoundDelay = 0.8f;

    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Obtener el componente Rigidbody
        rb.useGravity = false; // Desactiva la gravedad predeterminada de Unity para personalizarla

        // Obtener sonidos
        audioSources = GetComponents<AudioSource>();
        jumpSound = audioSources[0];
        walkSound = audioSources[1];

        // Obtener el Animator
        animator = GetComponent<Animator>();

    }

    void Update()
    {
        // Obtener las entradas del jugador
        moveInputX = Input.GetAxis("Horizontal");
        moveInputZ = Input.GetAxis("Vertical");

        // Si no hay entrada de movimiento, salir de la función
        if (moveInputX == 0 && moveInputZ == 0)
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
            animator.SetBool("Moving", false);
            return; // No hacer cálculos innecesarios si no hay entrada de movimiento
        }
        else
        {
            animator.SetBool("Moving", true);
        }

        // Verificar si está corriendo (Shift)
        currentSpeed = moveSpeed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed *= runMultiplier; // Aumentar velocidad al correr
            // Al correr se escuchará mas rapido el sonido de caminar
            stepSoundDelay = 0.4f;
            animator.SetBool("Running", true);
        }
        else
        {
            stepSoundDelay = 0.8f;
            animator.SetBool("Running", false);
        }

        // Direcciones de movimiento en base a la cámara
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        // Eliminar cualquier componente de elevación para mantener al jugador en el plano
        forward.y = 0f;
        right.y = 0f;

        // Normalizar para asegurar que la magnitud del vector no afecte la velocidad
        forward.Normalize();
        right.Normalize();

        // Calcular la dirección en la que el jugador debería moverse
        Vector3 moveDirection = (forward * moveInputZ) + (right * moveInputX);
        moveDirection.Normalize(); // Asegurarse de que no hay un exceso de velocidad en diagonal

        // Aplicar el movimiento
        rb.velocity = new Vector3(moveDirection.x * currentSpeed, rb.velocity.y, moveDirection.z * currentSpeed);

        // Aplicar gravedad personalizada
        rb.AddForce(Physics.gravity * (gravityScale - 1) * rb.mass);

        // Rotar el jugador solo si hay movimiento y está en el suelo
        if (moveDirection.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 0.15f); // Rotación suave
        }

        // Manejar el salto
        HandleJump();

        // Reproducir sonido de caminar si se cumple la condición
        HandleWalkingSound(moveDirection);

    }

    void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && (isGrounded || jumpCount < maxJumps))
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
            jumpCount++; // Incrementar contador de saltos al saltar
            isGrounded = false; // Evitar múltiples saltos consecutivos
            jumpSound.Play();
        }
    }

    void HandleWalkingSound(Vector3 moveDirection)
    {
        // Solo reproducir el sonido si el personaje se está moviendo y está en el suelo
        if (moveDirection != Vector3.zero && isGrounded)
        {
            // Actualizar el temporizador
            stepSoundTimer -= Time.deltaTime;

            // Reproducir el sonido si ha pasado suficiente tiempo (delay)
            if (stepSoundTimer <= 0f && currentSpeed >= moveSpeed)
            {
                walkSound.Play();
                stepSoundTimer = stepSoundDelay; // Reiniciar el temporizador
            }
        }
        else
        {
            // Reiniciar el temporizador si el personaje deja de moverse
            stepSoundTimer = 0f;
        }
    }

    // Detectar colisión con el suelo
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Nature") || collision.gameObject.CompareTag("Platform"))
        {
            isGrounded = true;
            jumpCount = 0; // Restablecer el contador de saltos al tocar el suelo
        }
    }

    // Detectar cuando el jugador deja de tocar el suelo
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Nature"))
        {
            isGrounded = false;
        }
    }
}