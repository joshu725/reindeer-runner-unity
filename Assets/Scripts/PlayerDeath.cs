using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    public string dangerTag = "Danger"; // La etiqueta del bloque peligroso

    void OnCollisionEnter(Collision collision)
    {
        // Verifica si la colisión es con un objeto etiquetado como "Danger"
        if (collision.gameObject.CompareTag(dangerTag))
        {
            Die(); // Llama a la función Die() para manejar la muerte del jugador
        }
    }

    // Función para manejar la lógica de muerte del jugador
    void Die()
    {
        // Aquí puedes agregar cualquier efecto de muerte, como una animación, sonido, etc.
        Debug.Log("Jugador ha muerto!");

        // Reiniciar la escena actual
        SceneManager.LoadScene("Death");
    }
}

