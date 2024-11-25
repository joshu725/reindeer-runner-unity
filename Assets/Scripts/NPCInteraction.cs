using System.Collections;
using UnityEngine;
using TMPro;

public class NPCInteraction : MonoBehaviour
{
    public float interactionDistance = 3f; // Distancia m�nima para interactuar
    public GameObject player; // Referencia al jugador
    public string[] dialogue; // Almacena los mensajes del NPC
    public TextMeshProUGUI dialogueText; // Campo de texto para mostrar los di�logos en la UI
    public GameObject dialoguePanel; // Panel de la UI que contiene el texto

    private bool isPlayerClose = false;
    private int currentDialogueIndex = 0;
    private PlayerMovement playerMovement; // Referencia al script de movimiento del jugador
    private bool isDialogueActive = false; // Para controlar si el di�logo est� activo

    void Start()
    {
        // Asegurarse de que el panel de di�logo est� oculto al inicio
        dialoguePanel.SetActive(false);

        // Obtener el componente de movimiento del jugador
        playerMovement = player.GetComponent<PlayerMovement>();

        if (playerMovement == null)
        {
            Debug.LogError("PlayerMovement script not found on player!");
        }
    }

    void Update()
    {
        // Comprobar la distancia entre el NPC y el jugador
        float distance = Vector3.Distance(player.transform.position, transform.position);

        if (distance <= interactionDistance && !isDialogueActive)
        {
            isPlayerClose = true;
            if (Input.GetKeyDown(KeyCode.E))
            {
                StartDialogue();
            }
        }

        if (isDialogueActive && Input.GetMouseButtonDown(0)) // Clic izquierdo
        {
            DisplayNextSentence();
        }
    }

    void StartDialogue()
    {
        // Mostrar el panel de di�logo
        dialoguePanel.SetActive(true);
        dialogueText.text = dialogue[currentDialogueIndex];

        // Desactivar el movimiento del jugador
        playerMovement.enabled = false;

        // Activar el estado de di�logo
        isDialogueActive = true;
    }

    public void DisplayNextSentence()
    {
        currentDialogueIndex++;
        if (currentDialogueIndex >= dialogue.Length)
        {
            EndDialogue();
        }
        else
        {
            dialogueText.text = dialogue[currentDialogueIndex];
        }
    }

    void EndDialogue()
    {
        // Desactivar el panel de di�logo con el siguiente clic izquierdo
        if (Input.GetMouseButtonDown(0))
        {
            dialoguePanel.SetActive(false);
            currentDialogueIndex = 0;

            // Reactivar el movimiento del jugador
            playerMovement.enabled = true;

            // Desactivar el estado de di�logo
            isDialogueActive = false;
        }
    }

    void OnDrawGizmosSelected()
    {
        // Dibuja un �rea en la escena para visualizar la distancia de interacci�n
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionDistance);
    }
}
