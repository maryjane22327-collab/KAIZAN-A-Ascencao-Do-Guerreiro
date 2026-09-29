using UnityEngine;
using UnityEngine.SceneManagement;

public class PannelGameOver : MonoBehaviour
{   public void ReiniciarJogo()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Sair()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }
}
