using UnityEngine;

public class TorretaControle : MonoBehaviour
{
    public GameObject alvo;
    public float velocidadeRotacao = 10f;
    public float anguloOffset = 0f;
    public GameObject projetil;
    public float tempoDeTiro;
    private float tempoProximoTiro;
    private float danoTorreta;
    private float cadenciaDeTiro;
    private bool habilitarTiro;

    public void Init(Torre torre, GameObject alvoPrioritario)
    {
        cadenciaDeTiro = torre.velocidadeAtaque / 100;
        tempoProximoTiro = Time.timeSinceLevelLoad + (tempoDeTiro / cadenciaDeTiro);
        danoTorreta = torre.poderAtaque;
        habilitarTiro = true;
        alvo = alvoPrioritario;
    }

    public void DesativarTorreta()
    {
        habilitarTiro = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (habilitarTiro == false || CanvasGameMng.PannelGamePlay.FimDeJogo == true) return;
        ApontarParaAlvo();
        AtirarProjetil();
    }

    public void AtirarProjetil()
    {
        if(tempoProximoTiro < Time.timeSinceLevelLoad)
        {
            tempoProximoTiro = Time.timeSinceLevelLoad + tempoDeTiro;
            GameObject novoProjetil = Instantiate(projetil, null);
            novoProjetil.transform.position = transform.position;
            novoProjetil.transform.rotation = transform.rotation;
            novoProjetil.GetComponent<ProjetilControle>().Init(danoTorreta/100);
        }
    }

    private void ApontarParaAlvo()
    {
        Vector3 direcao = alvo.transform.position - transform.position;
        direcao.z = 0f; 

        if (direcao.sqrMagnitude > 0.01f)
        {
            float angle = Mathf.Atan2(direcao.y, direcao.x) * Mathf.Rad2Deg;
            Quaternion rotacaoAlvo = Quaternion.AngleAxis(angle + anguloOffset, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacaoAlvo, velocidadeRotacao * Time.deltaTime);
        }
    }
}
