using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PannelListaTorres : MonoBehaviour
{
    [SerializeField] private GameObject pnlListaTorres;
    [SerializeField] private Transform contentTorres;
    [SerializeField] private List<GameObject> listaDeTorres;
    [SerializeField] private GameObject itemTorreGameplay;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        Init();
    }

    public void Init()
    {
        //Limpar a lista de torres para poder atualizar
        foreach (GameObject item in listaDeTorres)
        {
            Destroy(item);
        }
        listaDeTorres.Clear();

        //Configura a lista de torres que o jogador possui
        List<Torre> listaTorresPlayer = DBMng.ObterTorresAtivasPlayer();
        foreach (Torre torre in listaTorresPlayer)
        {
            GameObject item = Instantiate(itemTorreGameplay, contentTorres);
            TorreSO torreSO = GameManager.GameData.torres.ToList().Find(
                torreSO => torreSO.torre.id == torre.id
            );
            Sprite icone = torreSO.icone;

            item.GetComponent<ItemTorreGameplay>().Init(torre, icone);
            listaDeTorres.Add(item);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
