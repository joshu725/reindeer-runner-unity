using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveColForward : MonoBehaviour
{
    public float speed = 3f;               // Velocidad de movimiento del enemigo
    public float detectionDistance = 1.5f; // Distancia para detectar una pared
    public LayerMask wallLayer;            // Capa que define qué objetos son considerados "pared"
    private Vector3 moveDirection;

    void Start()
    {
        // El enemigo se mueve hacia adelante
        moveDirection = transform.forward;
    }

    void Update()
    {
        // Mover al enemigo en la dirección actual
        transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);
        Debug.DrawRay(transform.position, transform.forward * detectionDistance, Color.red);

        // Verificar si hay una pared en frente usando Raycast
        if (Physics.Raycast(transform.position, transform.forward, detectionDistance, wallLayer))
        {
            // Si se detecta una pared, girar al enemigo
            TurnAround();
        }
    }

    // Gira al enemigo 180 grados
    private void TurnAround()
    {
        // Rotar 180 grados alrededor del eje Y (hace que el enemigo se gire)
        transform.Rotate(0f, 180f, 0f);

        // Actualizar la dirección del movimiento
        moveDirection = transform.forward;
    }
}
