using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;    // Velocidad de movimiento horizontal
    public float currentSpeed;
    public float jumpForce = 10f;    // Fuerza del salto
    public float gravityScale = 1.75f; // Factor de gravedad personalizado
    public Transform cameraTransform; // Transform de la cámara
    public int maxJumps = 2; // Número máximo de saltos permitidos (doble salto)
    public float runMultiplier = 1.5f; // Multiplicador de velocidad al correr con Shift

    private Rigidbody rb;
    public bool isGrounded = true; // Indica si el jugador está tocando el suelo
    private int jumpCount = 0; // Contador de saltos realizados
    private float moveInputX;
    private float moveInputZ;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Obtener el componente Rigidbody
        rb.useGravity = false; // Desactiva la gravedad predeterminada de Unity para personalizarla
    }

    void Update()
    {
        // Obtener las entradas del jugador
        moveInputX = Input.GetAxis("Horizontal");
        moveInputZ = Input.GetAxis("Vertical");

        // Verificar si está corriendo (Shift)
        currentSpeed = moveSpeed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed *= runMultiplier; // Aumentar velocidad al correr
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
        Vector3 moveDirection = forward * moveInputZ + right * moveInputX;
        rb.velocity = new Vector3(moveDirection.x * currentSpeed, rb.velocity.y, moveDirection.z * currentSpeed);

        // Aplicar gravedad personalizada
        rb.AddForce(Physics.gravity * (gravityScale - 1) * rb.mass);

        // Saltar
        if (Input.GetButtonDown("Jump") && (isGrounded || jumpCount < maxJumps))
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
            jumpCount++; // Incrementa el contador de saltos al saltar
            isGrounded = false; // Asegurarse de que no está en el suelo después de un salto
        }

        // Rotar el jugador solo si hay movimiento y está en el suelo
        if (moveDirection != Vector3.zero && isGrounded)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 0.15f);
        }
    }

    // Detectar colisión con el suelo
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Nature"))
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