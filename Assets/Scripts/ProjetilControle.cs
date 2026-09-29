using UnityEngine;

public class ProjetilControle : MonoBehaviour
{
    private float dano;

    public AudioClip somTiro;

    public float Dano
    {
        get { return dano; }
    }

    public void Init(float porcentagemDano)
    {
        dano = Constants.VALOR_PADRAO_DANO_PROJETIL * porcentagemDano;

        if (GameManager.Audios != null && somTiro != null)
        {
            GameManager.Audios.TocarSom(somTiro);
        }
    }
}