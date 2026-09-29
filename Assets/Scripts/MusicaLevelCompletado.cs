using UnityEngine;

public class MusicaLevelCompletado : MonoBehaviour
{
    public AudioClip musicaLevelCompletado;

    public void TocarMusicaLevelCompletado()
    {
        if (musicaLevelCompletado == null)
            return;

        GameManager.Audios.TocarMusica(musicaLevelCompletado);
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

        // Toca a música de fase concluída
        TocarMusicaLevelCompletado();
    }
}