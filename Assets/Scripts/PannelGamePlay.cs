using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PannelGamePlay : MonoBehaviour
{
    [SerializeField] private GameObject pnlGamePlay;
    [SerializeField] private TextMeshProUGUI txtMoedas;
    [SerializeField] private TextMeshProUGUI txtInimigos;
    [SerializeField] private Slider sldVida;
    [SerializeField] private TextMeshProUGUI txtTempo;
    [SerializeField] private int moedasAtuaisNivel;
    private float contagemTempo;
    private float vidaAtualJogador;
    private bool fimDeJogo;
    private int contagemInimigosMortos;
    private int maximoInimigosMapa;
    private int totalInimigosMortosPeloJogador;
    private int totalMoedasGanhasMapa;

    public int ContagemTempo
    {
        get { return (int)contagemTempo; }
    }

    public int TotalMoedasGanhasMapa
    {
        get { return totalMoedasGanhasMapa; } 
    }

    public int TotalInimigosMortosPeloJogador
    {
        get { return totalInimigosMortosPeloJogador; }
        set { totalInimigosMortosPeloJogador = value; }
    }

    public bool FimDeJogo
    {
        get {return fimDeJogo;}
    }

    public int MoedasNivel
    {
        get { return moedasAtuaisNivel;}
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moedasAtuaisNivel = Constants.VALOR_INICIAL_MOEDAS_NIVEL;
        txtMoedas.text = $"${moedasAtuaisNivel}";
        contagemTempo = 0;
        vidaAtualJogador = Constants.VALOR_VIDA_JOGADOR;
        sldVida.maxValue = Constants.VALOR_VIDA_JOGADOR;
        sldVida.value = vidaAtualJogador;
        txtTempo.text = $"{contagemTempo}";
        contagemInimigosMortos = 0;
        totalMoedasGanhasMapa = 0;
    }

    public void AtualizarInimigoUI(int inimigosNoMapa, int maximoInimigos)
    {
        txtInimigos.text = $"{inimigosNoMapa}/{maximoInimigos}";
        maximoInimigosMapa = maximoInimigos;
    }

    public int ObterMoedasTotais()
    {
        return moedasAtuaisNivel;
    }

    public void DebitarMoedas(int debito)
    {
        moedasAtuaisNivel -= debito;
        txtMoedas.text = $"${moedasAtuaisNivel}";
    }

    public void AdicionarMoedas(int moedas)
    {
        moedasAtuaisNivel += moedas;
        txtMoedas.text = $"${moedasAtuaisNivel}";
        totalMoedasGanhasMapa += moedas;
    }

    public void PausarJogo()
    {
        CanvasGameMng.Instance.AtivarPainel(EnumPaineisGame.Pause);
        Time.timeScale = 0; //Congela o jogo
    }

    public void DecrementarVidaJogador(float danoJogador)
    {
        if (fimDeJogo == true) return;
        vidaAtualJogador -= danoJogador;
        if(vidaAtualJogador <=0)
        {
            vidaAtualJogador = 0;
            CanvasGameMng.Instance.AtivarPainel(EnumPaineisGame.GameOver);
            fimDeJogo = true;
        }
        sldVida.value = vidaAtualJogador;
    }

    public void IncrementarVidaJogador(float porcentagem)
    {
        if (fimDeJogo == true) return;
        vidaAtualJogador += Constants.VALOR_VIDA_JOGADOR * porcentagem; // 0.5
        if (vidaAtualJogador > Constants.VALOR_VIDA_JOGADOR)
        {
            vidaAtualJogador = Constants.VALOR_VIDA_JOGADOR;
        }
        sldVida.value = vidaAtualJogador;
    }

    public void ContarInimigoMorto()
    {
        if (fimDeJogo == true) return;
        contagemInimigosMortos += 1;
        if(contagemInimigosMortos == maximoInimigosMapa)
        {
            CanvasGameMng.Instance.AtivarPainel(EnumPaineisGame.LevelCompletado);
            fimDeJogo = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (fimDeJogo == true) return;
        contagemTempo += Time.deltaTime;
        int minutos = (int)contagemTempo / 60;
        int segundos = (int)contagemTempo % 60;
        txtTempo.text = $"{minutos:D2}:{segundos:D2}";
    }
}
