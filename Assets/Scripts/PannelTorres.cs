using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class PannelTorres : MonoBehaviour
{
    public Transform contentTorres;
    public TextMeshProUGUI txtQtdTorresAtivas;
    public GameObject itemUpgradeTorre;
    public TextMeshProUGUI txtMoedas;
    public PannelUpgradeTorre pnlUpgradeTorre;
    private List<GameObject> listaTorres = new List<GameObject>();
    int totalTorresAtivas;
    int totalMoedas;
    private void OnEnable()
    {
        Init();
    }

    public void Init()
    {
        //Configura a informação das torres ativas
        totalTorresAtivas = DBMng.TotalTorresAtivas();
        txtQtdTorresAtivas.text = $"{totalTorresAtivas}/{Constants.LIMITE_MAXIMO_TORRES_ATIVAS}";

        //Configura a informação das moedas
        totalMoedas = DBMng.ObterMoedasPlayer();
        txtMoedas.text = $"${totalMoedas}";

        //Limpar a lista de torres para poder atualizar
        foreach(GameObject item in listaTorres)
        {
            Destroy(item);
        }
        listaTorres.Clear();

        //Configura a lista de torres que o jogador possui
        List<Torre> listaTorresPlayer = DBMng.ObterTorresPlayer();
        foreach (Torre torre in listaTorresPlayer)
        {
            GameObject item = Instantiate(itemUpgradeTorre, contentTorres);
            TorreSO torreSO = GameManager.GameData.torres.ToList().Find(torreSO => torreSO.torre.id == torre.id);
            Sprite icone = torreSO.icone;

            item.GetComponent<ItemUpgradeTorre>().Init(torre, icone);
            listaTorres.Add(item);
        }
    }

    public void AtivarOuDesativarTorre(ItemUpgradeTorre itemTorre)
    {
        //Verificar se a torre está ativa
        if (itemTorre.Torre.estaAtivo == true)
        {
            //Desativar a torre
            Torre torreAtualizada = itemTorre.Torre;
            torreAtualizada.estaAtivo = false;
            DBMng.DesativarTorre(torreAtualizada.id);
            itemTorre.AtualizarTorre(torreAtualizada);
            totalTorresAtivas--;
            txtQtdTorresAtivas.text = $"{totalTorresAtivas}/{Constants.LIMITE_MAXIMO_TORRES_ATIVAS}";
        }
        else
        {
            //Verificar o limite de torres ativas
            if (totalTorresAtivas < Constants.LIMITE_MAXIMO_TORRES_ATIVAS)
            {
                totalTorresAtivas++;
                txtQtdTorresAtivas.text = $"{totalTorresAtivas}/{Constants.LIMITE_MAXIMO_TORRES_ATIVAS}";
                Torre torreAtualizada = itemTorre.Torre;
                torreAtualizada.estaAtivo = true;
                DBMng.AtivarTorre(torreAtualizada.id);
                itemTorre.AtualizarTorre(torreAtualizada);
            }
            else
            {
                //Informar ao jogador que ele atingiu o limite de torres ativas
            }
        }
    }

    public void AbrirPannelUpgradeTorre(Torre torre)
    {
        pnlUpgradeTorre.gameObject.SetActive(true);
        pnlUpgradeTorre.Init(torre);        
    }
}
