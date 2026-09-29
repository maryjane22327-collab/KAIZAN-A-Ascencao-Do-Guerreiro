using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemTorreGameplay : MonoBehaviour
{
    public Image imgIcone;
    public TextMeshProUGUI txtNivel;
    public TextMeshProUGUI txtNome;
    public TextMeshProUGUI txtVelocidade;
    public TextMeshProUGUI txtAtaque;
    public TextMeshProUGUI txtPreco;
    public Image imgBotaoCompra;
    public Color corOn;
    public Color corOff;
    private Torre torre;
    private CanvasMapaMng mapaMng;
    public Torre Torre { set { torre = value; } get { return torre; } }

    public void Init(Torre novaTorre, Sprite icone)
    {
        torre = novaTorre;
        imgIcone.sprite = icone;
        txtNivel.text = $"Nv. {(torre.nivel == Constants.NIVEL_MAXIMO_TORRE ? "MAX" : torre.nivel)}";
        txtNome.text = torre.nome;
        txtVelocidade.text = $"Velocidade: {(int)torre.velocidadeAtaque}";
        txtAtaque.text = $"Ataque: {(int)torre.poderAtaque}";
        txtPreco.text = $"${torre.preco}";
        mapaMng = FindFirstObjectByType<CanvasMapaMng>();
    }

    private void Update()
    {
        if(CanvasGameMng.PannelGamePlay.ObterMoedasTotais() >= torre.preco)
        {
            imgBotaoCompra.color = corOn;
        }
        else
        {
            imgBotaoCompra.color = corOff;
        }
    }

    public void ComprarTorre()
    {
        //Verificar se tem moeda suficiente
        if (CanvasGameMng.PannelGamePlay.MoedasNivel >= torre.preco)
        {
            //Comprar torre
            //Debitar as moedas
            CanvasGameMng.PannelGamePlay.DebitarMoedas(torre.preco);
            //Ativar a torre no botão que solicitou a nova torre
            mapaMng.DefinirTorre(torre);
            //Desativar tela de escolhas
            CanvasGameMng.Instance.DesativarPainelEspecifico(EnumPaineisGame.EscolhaTorre);
        }

    }
}
