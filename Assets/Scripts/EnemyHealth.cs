using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 50; // Salud máxima del enemigo
    public int currentHealth;

    void Start()
    {
        currentHealth = maxHealth; // Inicializa la salud actual al máximo
    }

    // Método que recibe el daño del ataque
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Enemigo recibió daño, salud actual: " + currentHealth);

        // Si la salud llega a 0, el enemigo muere
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Aquí puedes manejar lo que sucede cuando el enemigo muere
        Debug.Log("Enemigo ha muerto!");

        // Destruir al enemigo (esto lo elimina de la escena)
        Destroy(gameObject);
    }
}
