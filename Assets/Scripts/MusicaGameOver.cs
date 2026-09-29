using UnityEngine;

public class MusicaGameOver : MonoBehaviour
{
    public AudioClip musicaGameOver;

    public void TocarMusicaGameOver()
    {
        if (musicaGameOver == null)
            return;

        GameManager.Audios.TocarMusica(musicaGameOver);
    }

    private void OnEnable()
    {
        if (GameManager.Audios == null)
            return;

        // Para a música da gameplay
        if (GameManager.Audios.audioMusica != null)
        {
            GameManager.Audios.audioMusica.Stop();
        }

        // Para a marcha dos inimigos
        if (GameManager.Audios.audioMarcha != null)
        {
            GameManager.Audios.audioMarcha.Stop();
        }

        // Toca a música de Game Over
        TocarMusicaGameOver();
    }
}