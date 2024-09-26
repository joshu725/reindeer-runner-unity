using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100; // Salud máxima
    public int currentHealth;

    void Start()
    {
        currentHealth = maxHealth; // Inicializa la salud actual al máximo
    }

    // Método que se llama cuando el personaje entra en contacto con el enemigo
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Danger"))
        {
            TakeDamage(20);
        }
    }

    // Método que recibe el daño
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Jugador recibió daño, salud actual: " + currentHealth);

        // Si la salud llega a 0, el jugador muere
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Aquí puedes manejar lo que sucede cuando el enemigo muere
        Debug.Log("Jugador ha muerto!");

        // Reiniciar la escena actual
        SceneManager.LoadScene("Death");
    }
}
