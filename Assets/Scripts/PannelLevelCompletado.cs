using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PannelLevelCompletado : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI txtMoedasTotaisMapa;
    [SerializeField] TextMeshProUGUI txtInimigosMortosMapa;
    [SerializeField] TextMeshProUGUI txtTempoMapa;
    [SerializeField] TextMeshProUGUI txtPontuacaoFinal;
    [SerializeField] TextMeshProUGUI txtMelhorPontuacao;

    private int totalMoedasGanhasMapa;
    private int totalInimigosMortosPeloJogador;
    private int tempoTotalMapa;
    private int pontuacaoFinal;
    private int melhorPontuacao;

    void OnEnable()
    {
        totalInimigosMortosPeloJogador = CanvasGameMng.PannelGamePlay.TotalInimigosMortosPeloJogador;
        totalMoedasGanhasMapa = CanvasGameMng.PannelGamePlay.TotalMoedasGanhasMapa;
        tempoTotalMapa = CanvasGameMng.PannelGamePlay.ContagemTempo;
        txtMoedasTotaisMapa.text = $"${totalMoedasGanhasMapa}";
        txtInimigosMortosMapa.text = $"{totalInimigosMortosPeloJogador}";
        txtTempoMapa.text = $"{tempoTotalMapa}s";
        pontuacaoFinal = (totalMoedasGanhasMapa * totalInimigosMortosPeloJogador)/ tempoTotalMapa;
        txtPontuacaoFinal.text = $"{pontuacaoFinal}";
        Nivel novoNivel = new Nivel();
        novoNivel.id = SceneManager.GetActiveScene().buildIndex;
        novoNivel.nome = SceneManager.GetActiveScene().name;
        novoNivel.totalInimigos = totalInimigosMortosPeloJogador;
        novoNivel.totalMoedasColetadas = totalMoedasGanhasMapa;
        novoNivel.tempoTotal = tempoTotalMapa;
        novoNivel.melhorPontuacao = pontuacaoFinal;
        novoNivel.completado = true;
        DBMng.AdicionarNivel(novoNivel);
        txtMelhorPontuacao.text = DBMng.ObterMelhorPontuacaoNivel(novoNivel.id).ToString();
    }
    public void ReiniciarJogo()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Sair()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }

    public void ProximoNivel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
