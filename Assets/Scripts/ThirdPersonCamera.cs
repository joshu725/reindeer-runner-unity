using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target; // El jugador o el objeto a seguir
    public float distance = 4.5f; // Distancia desde la c�mara al objetivo
    public float height = 1.0f; // Altura de la c�mara con respecto al objetivo
    public float rotationSpeed = 500.0f; // Velocidad de rotaci�n de la c�mara con el clic derecho
    public float zoomSpeed = 2.0f; // Velocidad de zoom con la rueda del rat�n
    public float minDistance = 3.0f; // Distancia m�nima al objetivo
    public float maxDistance = 10.0f; // Distancia m�xima al objetivo

    private float currentX = 180.0f;
    private float currentY = 0.0f;
    private bool isRotating = false; // Controla si estamos rotando la c�mara

    void LateUpdate()
    {
        // Controlar rotaci�n con el clic derecho
        if (Input.GetMouseButtonDown(1)) // Cuando se presiona el bot�n derecho del mouse
        {
            isRotating = true;
        }

        if (Input.GetMouseButtonUp(1)) // Cuando se suelta el bot�n derecho del mouse
        {
            isRotating = false;
        }

        if (isRotating)
        {
            // Leer la entrada del mouse para rotar
            currentX += Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            currentY -= Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;
            currentY = Mathf.Clamp(currentY, -20, 80); // Limitar el �ngulo vertical de la c�mara
        }

        // Controlar zoom con la rueda del rat�n
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        distance -= scroll * zoomSpeed;
        distance = Mathf.Clamp(distance, minDistance, maxDistance); // Limitar el zoom

        // Definir la rotaci�n y posici�n deseadas
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        Vector3 direction = new Vector3(0, height, -distance);
        Vector3 position = target.position + rotation * direction;

        // Aplicar la rotaci�n y posici�n a la c�mara
        transform.position = position;
        transform.LookAt(target.position + Vector3.up * height);
    }
}
