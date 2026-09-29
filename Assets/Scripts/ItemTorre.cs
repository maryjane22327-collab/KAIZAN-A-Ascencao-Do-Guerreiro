using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemTorre : MonoBehaviour
{
    public Image imgIcone;
    public TextMeshProUGUI txtNivel;
    public TextMeshProUGUI txtNome;
    public TextMeshProUGUI txtVelocidade;
    public TextMeshProUGUI txtAtaque;
    public TextMeshProUGUI txtPreco;
    public GameObject imgComprado;
    public GameObject btnComprar;
    private Torre torre;

    /// <summary>
    /// Vai inicializar o objeto com os dados
    /// </summary>
    public void Init(Torre novaTorre, Sprite icone)
    {
        //referenciar a nova torre com a torre local do objeto
        torre = novaTorre;

        //atualizar os textos e a imagem do item
        imgIcone.sprite = icone;
        txtNivel.text = $"Nv.{torre.nivel}";
        txtNome.text = torre.nome;
        txtVelocidade.text = $"Velocidade: {torre.velocidadeAtaque}";
        txtAtaque.text = $"Ataque: {torre.poderAtaque}";
        txtPreco.text = $"${torre.preco}";

        if(DBMng.PossuiTorre(torre.id) == true)
        {
            imgComprado.SetActive(true);
            btnComprar.SetActive(false);
        }
    }

    /// <summary>
    /// Comprar a torre
    /// </summary>
    public void Comprar()
    {
        //Lógica para pedir o jogador de confirmar a compra
        GetComponentInParent<PannelLoja>().ComprarTorre(torre);
    }
}
