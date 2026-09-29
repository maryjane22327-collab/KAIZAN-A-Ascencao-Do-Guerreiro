using TMPro;
using UnityEngine;

public class PannelConfirmacaoCompra : MonoBehaviour
{
    public TextMeshProUGUI txtTexto;
    public GameObject btnConfirmar;
    public GameObject btnCancelar;
    public GameObject btnOk;

    private Torre torreDesejada;
    private Poder poderDesejado;
    public void Init(Torre novaTorre)
    {
        torreDesejada = novaTorre;
        poderDesejado = null;
        ExibirDadosNoPainel(torreDesejada.nome, torreDesejada.preco);
    }
    public void Init(Poder novoPoder)
    {
        poderDesejado = novoPoder;
        torreDesejada = null;
        ExibirDadosNoPainel(poderDesejado.nome, poderDesejado.preco);
    }

    public void ExibirDadosNoPainel(string nome, float preco)
    {
        txtTexto.text = $"Deseja comprar {nome} por ${preco}?";
        btnConfirmar.SetActive(true);
        btnCancelar.SetActive(true);
        btnOk.SetActive(false);
    }

    public void ConfirmarCompra()
    {
        bool compraConfirmada = torreDesejada != null ?
            DBMng.ComprarTorre(torreDesejada) : poderDesejado != null ?
            DBMng.ComprarPoder(poderDesejado) : false;

        if (compraConfirmada == true)
        {
            AtivarMensagemConfirmacao("Item comprado!");
            FindAnyObjectByType<PannelLoja>().AtualizarLoja();
        }
        else
        {
            AtivarMensagemConfirmacao("Não foi possível comprar o item, verifique as possibilidades!");
        }
    }

    private void AtivarMensagemConfirmacao(string mensagem)
    {
        txtTexto.text = mensagem;
        btnConfirmar.SetActive(false);
        btnCancelar.SetActive(false);
        btnOk.SetActive(true);
    }

    public void FecharPainel()
    {
        gameObject.SetActive(false);
    }
}
