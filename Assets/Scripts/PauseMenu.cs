using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;  // El panel del menú de pausa
    private bool isPaused = false;  // Verifica si el juego está en pausa

    void Update()
    {
        // Detecta cuando el jugador presiona la tecla Esc o P para pausar
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);  // Esconde el menú
        Time.timeScale = 1f;           // Restablece el tiempo del juego
        isPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);   // Muestra el menú de pausa
        Time.timeScale = 0f;           // Congela el tiempo del juego
        isPaused = true;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;  // Asegura que el tiempo regrese a la normalidad antes de reiniciar
        SceneManager.LoadScene("Initial Zone");
    }

    public void QuitMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }


    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Saliendo del juego");
    }
}

