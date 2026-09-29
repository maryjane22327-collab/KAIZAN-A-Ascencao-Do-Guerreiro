using UnityEngine;

public class MusicaCena : MonoBehaviour
{
    public AudioClip musica;

    private void Start()
    {
        GameManager.Audios.TocarMusica(musica);
    }
}