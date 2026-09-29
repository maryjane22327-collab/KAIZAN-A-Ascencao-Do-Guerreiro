using System;
using System.Collections.Generic;
using UnityEngine;

public class AreaTorre : MonoBehaviour
{
    public List<GameObject> inimigosEmArea;
    public BotaoTorreMapa botaoTorre;
    public TorretaControle torreta;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inimigosEmArea = new List<GameObject>();
        torreta.DesativarTorreta();
    }

    public void AdicionarInimigoNaLista(GameObject inimigo)
    {
        inimigosEmArea.Add(inimigo);
        AtivarTorreta();
        //Debug.Log($"ENTROU Inimigo: {inimigo.name}");
    }

    public void RemoverInimigoDaLista(GameObject inimigo)
    {
        inimigosEmArea.Remove(inimigo);
        inimigosEmArea.RemoveAll(item => item == null);//Remove valores nulos
        AtivarTorreta();
        //Debug.Log($"SAIU Inimigo: {inimigo.name}");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Inimigo")
        {
            AdicionarInimigoNaLista(collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Inimigo")
        {
            RemoverInimigoDaLista(collision.gameObject);
        }
        else if(collision.tag == "Projetil")
        {
            Destroy(collision.gameObject);
        }
    }

    public void AtivarTorreta()
    {
        try
        {
            torreta.Init(botaoTorre.TorreAtiva, inimigosEmArea[0]);
        }
        catch(Exception e)
        {
            torreta.DesativarTorreta();
        }
        
    }

    public void DesativarTorreta()
    {
        torreta.DesativarTorreta();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
