using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemUpgradeTorre : MonoBehaviour
{
    public Image imgIcone;
    public TextMeshProUGUI txtNivel;
    public TextMeshProUGUI txtNome;
    public TextMeshProUGUI txtVelocidade;
    public TextMeshProUGUI txtAtaque;
    public TextMeshProUGUI txtOnOff;
    public Image imgBotaoOnOff;
    public Color corOn;
    public Color corOff;
    public GameObject btnUpgrade;
    private Torre torre;
    public Torre Torre { set { torre = value; } get { return torre; } }

    public void Init(Torre novaTorre, Sprite icone)
    {
        imgIcone.sprite = icone;
        AtualizarTorre(novaTorre);        
    }

    public void AtivarOuDesativar()
    {
        GetComponentInParent<PannelTorres>().AtivarOuDesativarTorre(this);
    }

    public void MelhorarTorre()
    {
        GetComponentInParent<PannelTorres>().AbrirPannelUpgradeTorre(torre);
    }

    public void AtualizarTorre(Torre torreAtualizada)
    {
        torre = torreAtualizada;
        if(torre.nivel == Constants.NIVEL_MAXIMO_TORRE)
        {
            btnUpgrade.SetActive(false);
            txtNivel.text = "Nv.MAX";
        }
        else
        {
            btnUpgrade.SetActive(true);
            txtNivel.text = $"Nv.{torre.nivel}";
        }
        
        txtNome.text = torre.nome;
        txtVelocidade.text = $"Velocidade: {(int)torre.velocidadeAtaque}";
        txtAtaque.text = $"Ataque: {(int)torre.poderAtaque}";

        if (torre.estaAtivo == true)
        {
            txtOnOff.text = "Desativar";
            imgBotaoOnOff.color = corOn;
        }
        else
        {
            txtOnOff.text = "Ativar";
            imgBotaoOnOff.color = corOff;
        }
    }
}
