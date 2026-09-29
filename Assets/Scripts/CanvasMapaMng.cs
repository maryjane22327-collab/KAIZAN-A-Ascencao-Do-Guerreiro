using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CanvasMapaMng : MonoBehaviour
{
    public Waypoint primeiroDestino;
    private List<BotaoTorreMapa> todasAsTorresDoMapa;
    private int idTorreSelecionada;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        todasAsTorresDoMapa = GetComponentsInChildren<BotaoTorreMapa>().ToList();
    }

    public void ExibirTorresDisponiveis(int idTorre)
    {
        CanvasGameMng.Instance.AtivarPainelEspecifico(EnumPaineisGame.EscolhaTorre);
        idTorreSelecionada = idTorre;
    }

    public void DefinirTorre(Torre torreDefinida)
    {
        //Encontrar o botão que solicitou a torre
        foreach (BotaoTorreMapa botao in todasAsTorresDoMapa)
        {
            if(botao.id == idTorreSelecionada)
            {
                botao.DefinirTorre(torreDefinida);
                return;
            }
        }
    }
}
