using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PannelNiveis : MonoBehaviour
{
    public TextMeshProUGUI txtNomeNivel;
    public Image imgNivel;
    public GameObject levelCompletado;
    public GameObject btnIniciar;
    public GameObject btnEsquerda;
    public GameObject btnDireita;
    private int idNivelAtual;
    private List<Nivel> listaNiveisDisponiveis = new List<Nivel>();
    

    private void OnEnable()
    {
        //limpar a lista de niveis para atualizar com novos valores
        listaNiveisDisponiveis.Clear();

        //Pegar os niveis completados do jogador.
        Nivel[] niveisCompletadosJogador = DBMng.NiveisCompletados();

        for(int i = 0; i < GameManager.GameData.niveis.Count; i++)
        {
            //Verificar se o nivel é o primeiro ou se o nivel anterior foi completado
            Nivel nivelEncontrado = niveisCompletadosJogador.ToList().Find(nivel => nivel.id == GameManager.GameData.niveis[i].nivel.id);
            if (nivelEncontrado != null)
            {
                listaNiveisDisponiveis.Add(nivelEncontrado);
            }
            else
            {
                listaNiveisDisponiveis.Add(GameManager.GameData.niveis[i].nivel);
                break;
            }
        }

        Nivel ultimoNivelDaLista = listaNiveisDisponiveis[listaNiveisDisponiveis.Count - 1];
        idNivelAtual = ultimoNivelDaLista.id;
        txtNomeNivel.text = ultimoNivelDaLista.nome;
        imgNivel.sprite = GameManager.GameData.niveis.Find(nv => nv.nivel.id == ultimoNivelDaLista.id).icone;
        levelCompletado.SetActive(ultimoNivelDaLista.completado);
        //Configurar os botões de navegação
        btnDireita.SetActive(false);
        if (idNivelAtual == 1)
        {
            btnEsquerda.SetActive(false);
        }
        else
        {
            btnEsquerda.SetActive(true);
        }

    }

    public void NivelAnterior()
    {
        if(idNivelAtual-1 > 0)
        {
            idNivelAtual--;
            NivelSO ultimoNivelDaLista = GameManager.GameData.niveis.Find(nv => nv.nivel.id == listaNiveisDisponiveis[idNivelAtual - 1].id);
            txtNomeNivel.text = ultimoNivelDaLista.nivel.nome;
            imgNivel.sprite = ultimoNivelDaLista.icone;
            levelCompletado.SetActive(listaNiveisDisponiveis[idNivelAtual - 1].completado);
            //Habilitar o botão posterior
            btnDireita.SetActive(true);
            if(idNivelAtual == 1)
            {
                btnEsquerda.SetActive(false);
            }
        }
    }

    public void NivelPosterior()
    {
        if (idNivelAtual <= listaNiveisDisponiveis.Count)
        {
            idNivelAtual++;
            NivelSO ultimoNivelDaLista = GameManager.GameData.niveis.Find(nv => nv.nivel.id == listaNiveisDisponiveis[idNivelAtual - 1].id);
            txtNomeNivel.text = ultimoNivelDaLista.nivel.nome;
            imgNivel.sprite = ultimoNivelDaLista.icone;
            levelCompletado.SetActive(listaNiveisDisponiveis[idNivelAtual - 1].completado);
            //Habilitar o botão posterior
            btnEsquerda.SetActive(true);
            if (idNivelAtual == listaNiveisDisponiveis.Count)
            {
                btnDireita.SetActive(false);
            }
        }
    }

    public void IniciarNivel()
    {
        if(DBMng.TotalTorresAtivas() == 0)
        {
            //Avisar o jogador que ele precisa comprar e ativar pelo menos uma torre para iniciar o nivel
            GameManager.CanvasMensagem.ExibirMensagem(Constants.MENSAGEM_TORRE_OBRIGATORIO);
        }
        else
        {
            SceneManager.LoadScene($"Nivel_{idNivelAtual}");
        }        
    }
}
