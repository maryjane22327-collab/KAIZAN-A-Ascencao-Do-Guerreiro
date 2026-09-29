using UnityEngine;
using UnityEngine.SceneManagement;

public class PannelPauseGame : MonoBehaviour
{
    public GameObject pnlConfiguracoes;

    public void ContinuarJogo()
    {
        Time.timeScale = 1f;

        // Volta o áudio
        AudioListener.pause = false;

        pnlConfiguracoes.SetActive(false);

        CanvasGameMng.Instance.AtivarPainel(
            EnumPaineisGame.Gameplay
        );
    }

    public void ReiniciarJogo()
    {
        Time.timeScale = 1f;

        AudioListener.pause = false;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().name
        );
    }

    public void Sair()
    {
        Time.timeScale = 1f;

        AudioListener.pause = false;

        SceneManager.LoadScene("Menu");
    }

    public void ExibirConfiguracoes()
    {
        Time.timeScale = 0f;

        // Mantém o áudio tocando nas configurações
        AudioListener.pause = false;

        pnlConfiguracoes.SetActive(true);

        gameObject.SetActive(false);
    }

    public void VoltarDoConfiguracoes()
    {
        pnlConfiguracoes.SetActive(false);

        gameObject.SetActive(true);

        // Continua pausado
        Time.timeScale = 0f;

        // Continua com áudio tocando
        AudioListener.pause = false;
    }
}