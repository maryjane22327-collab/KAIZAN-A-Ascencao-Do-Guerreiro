using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DanoInimigo : MonoBehaviour
{
    public float vidaInimigo;
    public int valorInimigo;
    public Slider sldVidaInimigo;

    private Animator animator;
    private bool estaMorto = false;
    public AudioClip SomMorte;

    void Start()
    {
        animator = GetComponent<Animator>();

        sldVidaInimigo.maxValue = vidaInimigo;
        sldVidaInimigo.value = vidaInimigo;
    }

    public void EfetuarDanoAoInimigo(float valorDano)
    {
        if (CanvasGameMng.PannelGamePlay.FimDeJogo == true) return;

        // Impede que o inimigo receba dano depois de morrer
        if (estaMorto) return;

        vidaInimigo -= valorDano;

        if (vidaInimigo <= 0)
        {
            DestruirInimigo();
        }

        sldVidaInimigo.value = vidaInimigo;
    }

    public void DestruirInimigo()
    {
        if (estaMorto) return;

        estaMorto = true;
        vidaInimigo = 0;

        // Gerar moedas para o Player
        CanvasGameMng.PannelGamePlay.AdicionarMoedas(valorInimigo);
        CanvasGameMng.PannelGamePlay.TotalInimigosMortosPeloJogador += 1;
        CanvasGameMng.PannelGamePlay.ContarInimigoMorto();

        // Para o movimento
        InimigoIA ia = GetComponent<InimigoIA>();

        if (ia != null)
        {
            ia.CongelarInimigo();
        }

        // Descobre a direção que o inimigo estava andando
        float direcaoX = animator.GetFloat("DirecaoX");

        if (direcaoX < -0.1f)
        {
            animator.SetInteger("DirecaoMorte", 1);
        }
        else if (direcaoX > 0.1f)
        {
            animator.SetInteger("DirecaoMorte", 2);
        }
        else
        {
            animator.SetInteger("DirecaoMorte", 0);
        }

        // Ativa a animação de morte
        animator.SetTrigger("Morte");
        if (SomMorte != null)
        {
            GameManager.Audios.TocarSom(SomMorte);
        }

        // Espera a animação terminar
        StartCoroutine(EsperarMorte());
    }

    private IEnumerator EsperarMorte()
    {
        yield return new WaitForSeconds(
            animator.GetCurrentAnimatorStateInfo(0).length
        );

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Projetil"))
        {
            float valorDano = collision.GetComponent<ProjetilControle>().Dano;

            EfetuarDanoAoInimigo(valorDano);

            Destroy(collision.gameObject);
        }
    }

    public float CalcularDanoAoJogador()
    {
        return valorInimigo * Constants.PORCENTAGEM_DANO_INIMIGO;
    }
}