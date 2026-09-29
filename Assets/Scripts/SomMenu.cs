using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SomMenu : MonoBehaviour
{
   
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        audioSource.loop = true;
        audioSource.Play();
        /*
        if (audioSource.isPlaying)
        {
            Debug.Log("Esta tocando");
        }
        */
    }
}
