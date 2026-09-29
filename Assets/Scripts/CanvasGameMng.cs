using UnityEngine;

public class CanvasGameMng : MonoBehaviour
{
    public static CanvasGameMng Instance;
    public static PannelGamePlay PannelGamePlay;
    private void Awake()
    {
        if(Instance == null)
        {
            PannelGamePlay = GetComponentInChildren<PannelGamePlay>();
            Instance = this;
            return;
        }
        Destroy(gameObject);
    }

    public GameObject[] paineis;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AtivarPainel(EnumPaineisGame.Gameplay);
    }

    public void AtivarPainel(EnumPaineisGame painel)
    {
        foreach(GameObject pnl in paineis)
        {
            pnl.SetActive(false);
        }
        paineis[(int)painel].SetActive(true);
    }

    public void AtivarPainelEspecifico(EnumPaineisGame painel)
    {
        paineis[(int)painel].SetActive(true);
    }
    public void DesativarPainelEspecifico(EnumPaineisGame painel)
    {
        paineis[(int)painel].SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
