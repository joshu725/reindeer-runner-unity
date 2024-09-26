using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackRange = 2f;       // Alcance del golpe
    public int attackDamage = 10;        // Daño que inflige el golpe
    public LayerMask enemyLayer;         // Capa que define a los enemigos
    public Transform attackPoint;        // Punto desde el cual se realizará el ataque (puede ser la mano del personaje)
    public float attackCooldown = 1f;    // Tiempo de enfriamiento entre ataques
    public float attackAngle = 45f;      // Ángulo de ataque (en grados)

    private float nextAttackTime = 0f;   // Tiempo en el que el personaje puede volver a atacar

    void Update()
    {
        // Detectar si el jugador está intentando atacar
        if (Input.GetButtonDown("Fire1") && Time.time >= nextAttackTime)
        {
            // Realizar el ataque
            Attack();

            // Establecer el tiempo para el próximo ataque
            nextAttackTime = Time.time + attackCooldown;
        }
    }

    void Attack()
    {
        // Visualizar el área del ataque (puede ser una esfera o un punto frontal)
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayer);

        // Aplicar daño a todos los enemigos que estén en el área de ataque
        foreach (Collider enemy in hitEnemies)
        {
            // Verificar si el enemigo está dentro del ángulo de ataque frontal
            Vector3 directionToEnemy = (enemy.transform.position - transform.position).normalized;

            // Corregir la orientación de la dirección frontal
            Vector3 correctedForward = Quaternion.Euler(0, 0, 0) * transform.forward;
            float angleToEnemy = Vector3.Angle(correctedForward, directionToEnemy);

            if (angleToEnemy < attackAngle / 2)
            {
                // Obtener el script del enemigo y aplicarle daño
                EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(attackDamage);
                    Debug.Log("Golpe efectivo a " + enemy.name + " por " + attackDamage + " de daño.");
                }
            }
            else
            {
                Debug.Log("El enemigo " + enemy.name + " está fuera del ángulo de ataque.");
            }
        }
    }

    // Método para dibujar el área de ataque en la vista de la escena
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        // Dibujar líneas que representen el ángulo de ataque
        Gizmos.color = Color.blue;
        Vector3 forward = transform.forward * attackRange;

        // Corregir el ángulo de ataque
        Vector3 rightLimit = Quaternion.Euler(0, attackAngle / 2 - 0, 0) * forward;
        Vector3 leftLimit = Quaternion.Euler(0, -attackAngle / 2 - 0, 0) * forward;

        Gizmos.DrawRay(attackPoint.position, rightLimit);   // Límite derecho del ángulo
        Gizmos.DrawRay(attackPoint.position, leftLimit);    // Límite izquierdo del ángulo
    }
}
