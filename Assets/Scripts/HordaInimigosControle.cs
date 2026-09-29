using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HordaInimigosControle : MonoBehaviour
{
    public CanvasMapaMng mapaMng;
    public float tempoNovoInimigo;
    public List<InimigoNivel> inimigosDoNivel;
    public GameObject hordaInicio;

    private float tempoProximoInimigo;
    private int contagemInimigosInstanciados = 0;
    private int maximoInimigosMapa = 0;

    private List<GameObject> listaInimigosInstanciados;

    private bool tempoCongelado;

    public int MaximoInimigosMapa
    {
        get { return maximoInimigosMapa; }
    }

    public int ContagemInimigos
    {
        get { return contagemInimigosInstanciados; }
    }

    private void Awake()
    {
        contagemInimigosInstanciados = 0;

        foreach (var inimigo in inimigosDoNivel)
        {
            maximoInimigosMapa += inimigo.quantidade;
        }

        listaInimigosInstanciados = new List<GameObject>();
    }

    void Start()
    {
        tempoProximoInimigo = Time.timeSinceLevelLoad +
                              tempoNovoInimigo +
                              Constants.TEMPO_ESPERA_INICIAL_GAMEPLAY;

        CanvasGameMng.PannelGamePlay.AtualizarInimigoUI(
            contagemInimigosInstanciados,
            maximoInimigosMapa
        );
    }

    void Update()
    {
        if (tempoCongelado == true) return;

        // Remove da lista objetos que já foram destruídos
        listaInimigosInstanciados.RemoveAll(inimigo => inimigo == null);

        // Lógica do instanciamento dos inimigos
        if (Time.timeSinceLevelLoad > tempoProximoInimigo &&
            inimigosDoNivel.Count > 0)
        {
            tempoProximoInimigo =
                Time.timeSinceLevelLoad + tempoNovoInimigo;

            var inimigoId =
                new System.Random().Next(0, inimigosDoNivel.Count);

            inimigosDoNivel[inimigoId].totalInstanciados++;

            var novoInimigo =
                Instantiate(inimigosDoNivel[inimigoId].inimigo);

            novoInimigo.transform.position =
                hordaInicio.transform.position;

            novoInimigo
                .GetComponent<InimigoIA>()
                .DefinirNovoDestino(mapaMng.primeiroDestino);

            contagemInimigosInstanciados++;

            listaInimigosInstanciados.Add(novoInimigo);

            CanvasGameMng.PannelGamePlay.AtualizarInimigoUI(
                contagemInimigosInstanciados,
                maximoInimigosMapa
            );

            if (inimigosDoNivel[inimigoId].quantidade ==
                inimigosDoNivel[inimigoId].totalInstanciados)
            {
                inimigosDoNivel.Remove(
                    inimigosDoNivel[inimigoId]
                );
            }
        }
    }

    private void RemoverInimigoLista(GameObject inimigo)
    {
        if (inimigo == null) return;

        if (listaInimigosInstanciados.Contains(inimigo))
        {
            listaInimigosInstanciados.Remove(inimigo);
        }
    }

    public void DestruirInimigosInstanciados()
    {
        // Cria uma cópia para evitar problemas ao remover
        // inimigos da lista durante o foreach.
        foreach (var inimigo in listaInimigosInstanciados.ToList())
        {
            // Se o inimigo já foi destruído, simplesmente ignora.
            if (inimigo == null)
            {
                RemoverInimigoLista(inimigo);
                continue;
            }

            // Se estiver inativo, remove da lista e continua
            // procurando os outros inimigos.
            if (inimigo.activeSelf == false)
            {
                RemoverInimigoLista(inimigo);
                continue;
            }

            DanoInimigo danoInimigo =
                inimigo.GetComponent<DanoInimigo>();

            if (danoInimigo == null)
            {
                RemoverInimigoLista(inimigo);
                continue;
            }

            RemoverInimigoLista(inimigo);

            danoInimigo.DestruirInimigo();
        }
    }

    public void CongelarInimigos()
    {
        // Remove referências de objetos que já foram destruídos.
        listaInimigosInstanciados.RemoveAll(inimigo => inimigo == null);

        tempoCongelado = true;

        StartCoroutine(TempoCongelamentoInimigos());
    }

    IEnumerator TempoCongelamentoInimigos()
    {
        // Usa uma cópia da lista para evitar problemas caso
        // algum inimigo seja destruído durante o congelamento.
        List<GameObject> inimigosParaCongelar =
            listaInimigosInstanciados
            .Where(inimigo => inimigo != null && inimigo.activeSelf)
            .ToList();

        foreach (var inimigo in inimigosParaCongelar)
        {
            if (inimigo == null) continue;

            InimigoIA ia =
                inimigo.GetComponent<InimigoIA>();

            if (ia != null)
            {
                ia.CongelarInimigo();
            }
        }

        yield return new WaitForSeconds(
            Constants.TEMPO_CONGELAMENTO_INIMIGOS
        );

        foreach (var inimigo in inimigosParaCongelar)
        {
            if (inimigo == null) continue;

            InimigoIA ia =
                inimigo.GetComponent<InimigoIA>();

            if (ia != null && inimigo.activeSelf)
            {
                ia.DescongelarInimigo();
            }
        }

        tempoCongelado = false;

        // Limpa referências de inimigos que tenham sido destruídos
        // durante o período de congelamento.
        listaInimigosInstanciados.RemoveAll(inimigo => inimigo == null);
    }
}