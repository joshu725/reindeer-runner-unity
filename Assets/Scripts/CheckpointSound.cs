using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CheckpointSound : MonoBehaviour
{
    public AudioSource source;
    public AudioClip sound;
    public float sound_volume = 1f;

    private void OnTriggerEnter(Collider other)
    {
        AudioSource.PlayClipAtPoint(sound, gameObject.transform.position);
        Destroy(gameObject);
    }

}
