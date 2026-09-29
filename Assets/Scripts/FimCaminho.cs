using UnityEngine;

public class FimCaminho : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Inimigo"))
        {
            //Lógica para efetuar dano ao jogador
            float valorDano = collision.GetComponent<DanoInimigo>().CalcularDanoAoJogador();
            CanvasGameMng.PannelGamePlay.DecrementarVidaJogador(valorDano);
            CanvasGameMng.PannelGamePlay.ContarInimigoMorto();
            collision.gameObject.SetActive(false);
        }
    }
}
