using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoints : MonoBehaviour
{
    // Se obtienen los objetos necesarios
    [SerializeField] GameObject player;
    [SerializeField] List<GameObject> checkpoints;
    [SerializeField] Vector3 vectorPoint;
    [SerializeField] float dead;

    void Update()
    {
        // Se comprueba la altura del jugador, para cuando se caiga
        if (player.transform.position.y < -dead)
        {
            player.transform.position = vectorPoint;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Verifica si la colisión es con un objeto etiquetado como "Danger"
        if (collision.gameObject.CompareTag("DangerKill"))
        {
            // Se manda la jugador al ultimo checkpoint
            player.transform.position = vectorPoint;
        }
    }

    // Se elimina el objeto del checkpoint
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Checkpoint"))
        {
            vectorPoint = player.transform.position;
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Goal"))
        {
            SceneManager.LoadScene("Initial Zone");
        }
    }
}
