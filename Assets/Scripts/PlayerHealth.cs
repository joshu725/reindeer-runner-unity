using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100; // Salud m�xima
    public int currentHealth;

    void Start()
    {
        currentHealth = maxHealth; // Inicializa la salud actual al m�ximo
    }

    // M�todo que se llama cuando el personaje entra en contacto con el enemigo
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Danger"))
        {
            TakeDamage(20);
        }
    }

    // M�todo que recibe el da�o
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Jugador recibi� da�o, salud actual: " + currentHealth);

        // Si la salud llega a 0, el jugador muere
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Aqu� puedes manejar lo que sucede cuando el enemigo muere
        Debug.Log("Jugador ha muerto!");

        // Reiniciar la escena actual
        SceneManager.LoadScene("Death");
    }
}
