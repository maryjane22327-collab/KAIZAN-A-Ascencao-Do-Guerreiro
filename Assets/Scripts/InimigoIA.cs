using UnityEngine;

public class InimigoIA : MonoBehaviour
{
    public float velocidade;

private Waypoint destino;
    private bool habilitaMovimentacao;
    private float velocidadeOriginal;

    private Animator animator;

    private void Start()
    {
        velocidadeOriginal = velocidade;

        animator = GetComponent<Animator>();
    }

    public void DefinirNovoDestino(Waypoint novoDestino)
    {
        destino = novoDestino;
        habilitaMovimentacao = true;
    }

    void Update()
    {
        if (habilitaMovimentacao == false) return;

        // Descobre a direção até o próximo waypoint
        Vector2 direcao = (destino.transform.position - transform.position).normalized;

        // Envia a direção horizontal para o Animator
        animator.SetFloat("DirecaoX", direcao.x);

        // Envia a direção vertical para o Animator
        animator.SetFloat("DirecaoY", direcao.y);

        transform.position = Vector2.MoveTowards(
            transform.position,
            destino.transform.position,
            velocidade * Time.deltaTime
        );

        if (Vector2.Distance(transform.position, destino.transform.position) < 0.01f)
        {
            if (destino == destino.ObterProximoDestino())
            {
                habilitaMovimentacao = false;
            }
            else
            {
                destino = destino.ObterProximoDestino();
            }
        }
    }

    public void CongelarInimigo()
    {
        velocidade = 0;
    }

    public void DescongelarInimigo()
    {
        velocidade = velocidadeOriginal;
    }
}
