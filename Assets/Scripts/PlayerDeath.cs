using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    public string dangerTag = "Danger"; // La etiqueta del bloque peligroso

    void OnCollisionEnter(Collision collision)
    {
        // Verifica si la colisi�n es con un objeto etiquetado como "Danger"
        if (collision.gameObject.CompareTag(dangerTag))
        {
            Die(); // Llama a la funci�n Die() para manejar la muerte del jugador
        }
    }

    // Funci�n para manejar la l�gica de muerte del jugador
    void Die()
    {
        // Aqu� puedes agregar cualquier efecto de muerte, como una animaci�n, sonido, etc.
        Debug.Log("Jugador ha muerto!");

        // Reiniciar la escena actual
        SceneManager.LoadScene("Death");
    }
}

