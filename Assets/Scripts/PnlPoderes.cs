using UnityEngine;
using UnityEngine.UI;

public class PnlPoderes : MonoBehaviour
{
    [SerializeField] Image[] imgBtns;
    [SerializeField] GameObject[] btnBloqueios;

    private Animator animator;
    private bool estaAberto = false;

    [SerializeField] private Poder[] poderesPlayer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        poderesPlayer = DBMng.ObterTodosPoderesPlayer();
        BloquearPoderes();
        for (int i = 1; i < GameManager.GameData.poderes.Count; i++)
        {
            imgBtns[i].sprite = GameManager.GameData.poderes[i].icone;
        }

        for (int i = 0; i < poderesPlayer.Length; i++)
        {
            if(poderesPlayer[i].quantidade > 0)
            {
                btnBloqueios[poderesPlayer[i].id].SetActive(false);
            }            
        }
    }

    public void ExibirOuOcultarPainel()
    {
        if (estaAberto == false)
        {
            animator.SetBool("exibirPainel", true);
            estaAberto = true;
        }
        else
        {
            animator.SetBool("exibirPainel", false);
            estaAberto = false;
        }

    }

    public void InvocarPoder(int id)
    {
        switch (id)
        {
            case 1:
                if (btnBloqueios[id].activeSelf == true) return;
                if(DBMng.ConsumirPoder(id) == true)
                {
                    FindFirstObjectByType<HordaInimigosControle>().DestruirInimigosInstanciados();
                    btnBloqueios[id].SetActive(true);
                }
                break;
            case 2:
                if (btnBloqueios[id].activeSelf == true) return;
                if (DBMng.ConsumirPoder(id) == true)
                {
                    CanvasGameMng.PannelGamePlay.IncrementarVidaJogador(Constants.PORCENTAGEM_RECUPERACAO_VIDA);
                    btnBloqueios[id].SetActive(true);
                }                
                break;
            case 3:
                if (btnBloqueios[id].activeSelf == true) return;
                if (DBMng.ConsumirPoder(id) == true)
                {
                    FindFirstObjectByType<HordaInimigosControle>().CongelarInimigos();
                    btnBloqueios[id].SetActive(true);
                }
                break;
        }

        poderesPlayer = DBMng.ObterTodosPoderesPlayer();
    }

    private void BloquearPoderes()
    {
        foreach(var btn in btnBloqueios)
        {
            if(btn != null)
            {
                btn.SetActive(true);
            }
        }
    }    
}
