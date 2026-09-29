using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PannelLoja : MonoBehaviour
{
    [Header("Itens da Loja")]
    public GameObject itemPoder;
    public GameObject itemTorre;

    [Header("Contents")]
    public Transform contentTorre;
    public Transform contentPoder;

    [Header("UI")]
    public TextMeshProUGUI txtMoedas;
    public GameObject pnlConfirmacaoCompra;

    [Header("Listas de itens criados")]
    public List<GameObject> listaTorres = new List<GameObject>();
    public List<GameObject> listaPoderes = new List<GameObject>();

    private int moedasJogador;

    private void OnEnable()
    {
        AtualizarLoja();
    }

    public void AtualizarLoja()
    {
        AtualizarMoedas();


        foreach (GameObject item in listaTorres)
        {
            if (item != null)
            {
                Destroy(item);
            }
        }

        listaTorres.Clear();



        foreach (GameObject item in listaPoderes)
        {
            if (item != null)
            {
                Destroy(item);
            }
        }

        listaPoderes.Clear();


        foreach (TorreSO torreSO in GameManager.GameData.torres)
        {
            // Ignora posições vazias
            if (torreSO == null)
                continue;

            // Ignora caso o objeto TorreSO não tenha uma torre
            if (torreSO.torre == null)
                continue;

            GameObject item = Instantiate(
                itemTorre,
                contentTorre
            );

            ItemTorre itemTorreScript =
                item.GetComponent<ItemTorre>();

            if (itemTorreScript != null)
            {
                itemTorreScript.Init(
                    torreSO.torre,
                    torreSO.icone
                );
            }

            listaTorres.Add(item);
        }

        foreach (PoderSO poderSO in GameManager.GameData.poderes)
        {
            // Element 0 é vazio (None)
            // Então simplesmente ignoramos.
            if (poderSO == null)
                continue;

            // Caso exista PoderSO, mas não exista o Poder dentro dele
            if (poderSO.poder == null)
                continue;


            DBMng.InserirPoderesPlayer(poderSO.poder);


            Poder poderAtualizado =
                DBMng.BuscarPoderPlayer(
                    poderSO.poder.id
                );


            if (poderAtualizado == null)
            {
                Debug.LogWarning(
                    "PannelLoja: Não foi possível encontrar o poder do player. ID: "
                    + poderSO.poder.id
                );

                continue;
            }


            GameObject item = Instantiate(
                itemPoder,
                contentPoder
            );


            ItemPoder itemPoderScript =
                item.GetComponent<ItemPoder>();


            if (itemPoderScript != null)
            {
                itemPoderScript.Init(
                    poderAtualizado,
                    poderSO.icone
                );
            }
            else
            {
                Debug.LogError(
                    "PannelLoja: O prefab itemPoder não possui o componente ItemPoder!"
                );
            }


            listaPoderes.Add(item);
        }
    }

    public void ComprarTorre(Torre novaTorre)
    {
        pnlConfirmacaoCompra.SetActive(true);

        pnlConfirmacaoCompra
            .GetComponent<PannelConfirmacaoCompra>()
            .Init(novaTorre);
    }

    public void ComprarPoder(Poder novoPoder)
    {
        pnlConfirmacaoCompra.SetActive(true);

        pnlConfirmacaoCompra
            .GetComponent<PannelConfirmacaoCompra>()
            .Init(novoPoder);
    }

    public void AtualizarMoedas()
    {
        moedasJogador = DBMng.ObterMoedasPlayer();

        txtMoedas.text = $"${moedasJogador}";
    }
}