using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportNewZone : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Platform-Easy"))
        {
            SceneManager.LoadScene("Easy Zone");
        }

        if (collision.gameObject.CompareTag("Platform-Hard"))
        {
            SceneManager.LoadScene("Hard Zone");
        }
    }
}
