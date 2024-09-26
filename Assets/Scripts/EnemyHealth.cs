using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 50; // Salud m�xima del enemigo
    public int currentHealth;

    void Start()
    {
        currentHealth = maxHealth; // Inicializa la salud actual al m�ximo
    }

    // M�todo que recibe el da�o del ataque
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Enemigo recibi� da�o, salud actual: " + currentHealth);

        // Si la salud llega a 0, el enemigo muere
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Aqu� puedes manejar lo que sucede cuando el enemigo muere
        Debug.Log("Enemigo ha muerto!");

        // Destruir al enemigo (esto lo elimina de la escena)
        Destroy(gameObject);
    }
}
