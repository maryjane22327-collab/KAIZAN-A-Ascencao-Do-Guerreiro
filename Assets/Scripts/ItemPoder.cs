using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemPoder : MonoBehaviour
{
    public Image imgIcone;
    public TextMeshProUGUI txtQtd;
    public TextMeshProUGUI txtPreco;
    public GameObject btnComprar;
    private Poder poder;

    public void Init(Poder novoPoder, Sprite icone)
    {
        poder = novoPoder;
        imgIcone.sprite = icone;
        txtQtd.text = $"{poder.quantidade}/{Constants.LIMITE_MAXIMO_PODERES}";
        txtPreco.text = $"${poder.preco}";

        //Verificar se tem poder para bloquear o botao de compra
        if(novoPoder.quantidade >= Constants.LIMITE_MAXIMO_PODERES)
        {
            btnComprar.SetActive(false);
        }
    }
    
    public void Comprar()
    {
        //Lógica para pedir o jogador de confirmar a compra
        GetComponentInParent<PannelLoja>().ComprarPoder(poder);
    }
}
