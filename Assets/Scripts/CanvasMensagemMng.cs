using TMPro;
using UnityEngine;

public class CanvasMensagemMng : MonoBehaviour
{
    public GameObject pnlMensagem;
    public TextMeshProUGUI txtMensagem;

    public void ExibirMensagem(string mensagem)
    {
        txtMensagem.text = mensagem;
        pnlMensagem.SetActive(true);
    }
}
