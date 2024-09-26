using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackRange = 2f;       // Alcance del golpe
    public int attackDamage = 10;        // Da�o que inflige el golpe
    public LayerMask enemyLayer;         // Capa que define a los enemigos
    public Transform attackPoint;        // Punto desde el cual se realizar� el ataque (puede ser la mano del personaje)
    public float attackCooldown = 1f;    // Tiempo de enfriamiento entre ataques
    public float attackAngle = 45f;      // �ngulo de ataque (en grados)

    private float nextAttackTime = 0f;   // Tiempo en el que el personaje puede volver a atacar

    void Update()
    {
        // Detectar si el jugador est� intentando atacar
        if (Input.GetButtonDown("Fire1") && Time.time >= nextAttackTime)
        {
            // Realizar el ataque
            Attack();

            // Establecer el tiempo para el pr�ximo ataque
            nextAttackTime = Time.time + attackCooldown;
        }
    }

    void Attack()
    {
        // Visualizar el �rea del ataque (puede ser una esfera o un punto frontal)
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayer);

        // Aplicar da�o a todos los enemigos que est�n en el �rea de ataque
        foreach (Collider enemy in hitEnemies)
        {
            // Verificar si el enemigo est� dentro del �ngulo de ataque frontal
            Vector3 directionToEnemy = (enemy.transform.position - transform.position).normalized;

            // Corregir la orientaci�n de la direcci�n frontal
            Vector3 correctedForward = Quaternion.Euler(0, 0, 0) * transform.forward;
            float angleToEnemy = Vector3.Angle(correctedForward, directionToEnemy);

            if (angleToEnemy < attackAngle / 2)
            {
                // Obtener el script del enemigo y aplicarle da�o
                EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(attackDamage);
                    Debug.Log("Golpe efectivo a " + enemy.name + " por " + attackDamage + " de da�o.");
                }
            }
            else
            {
                Debug.Log("El enemigo " + enemy.name + " est� fuera del �ngulo de ataque.");
            }
        }
    }

    // M�todo para dibujar el �rea de ataque en la vista de la escena
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        // Dibujar l�neas que representen el �ngulo de ataque
        Gizmos.color = Color.blue;
        Vector3 forward = transform.forward * attackRange;

        // Corregir el �ngulo de ataque
        Vector3 rightLimit = Quaternion.Euler(0, attackAngle / 2 - 0, 0) * forward;
        Vector3 leftLimit = Quaternion.Euler(0, -attackAngle / 2 - 0, 0) * forward;

        Gizmos.DrawRay(attackPoint.position, rightLimit);   // L�mite derecho del �ngulo
        Gizmos.DrawRay(attackPoint.position, leftLimit);    // L�mite izquierdo del �ngulo
    }
}
