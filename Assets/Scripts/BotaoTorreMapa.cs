using UnityEngine;
using UnityEngine.UI;

public class BotaoTorreMapa : MonoBehaviour
{
    public int id;
    public Image iconeTorre;
    public GameObject txtPlus;
    public GameObject pnlDestruirTorre;
    public AreaTorre areaTorre;
    private Torre torreAtiva;
    private bool estaComTorre;
    private CanvasMapaMng mapaMng;

    private void Start()
    {
        mapaMng = GetComponentInParent<CanvasMapaMng>();    
    }

    public bool EstaComTorre
    {
        get { return estaComTorre; }
    }

    public Torre TorreAtiva
    {
        get { return torreAtiva; }
    }

    public void AbrirSelecaoTorres()
    {
        if(estaComTorre == false)
        {
            mapaMng.ExibirTorresDisponiveis(id);
        }
        else
        {
            pnlDestruirTorre.SetActive(true);
        }        
    }

    public void DefinirTorre(Torre novaTorre)
    {
        torreAtiva = novaTorre;
        iconeTorre.sprite = GameManager.GameData.torres.Find(
            torreSO => torreSO.torre.id == novaTorre.id
        ).icone;
        estaComTorre = true;
        txtPlus.SetActive(false);
        areaTorre.AtivarTorreta();
    }

    public void DestruirTorre()
    {
        estaComTorre = false;
        iconeTorre.sprite = null;
        txtPlus.SetActive(true);
        int precoRetornado = (int)(GameManager.GameData.torres.Find(
            torreSO => torreSO.torre.id == torreAtiva.id
        ).torre.preco * Constants.PORCENTAGEM_RETORNO_TORRE_DESTRUIDA);
        CanvasGameMng.PannelGamePlay.AdicionarMoedas(precoRetornado);
        torreAtiva = null;
        pnlDestruirTorre.SetActive(false);
        areaTorre.DesativarTorreta();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
