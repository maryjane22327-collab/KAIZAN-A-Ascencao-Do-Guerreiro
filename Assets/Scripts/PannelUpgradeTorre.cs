using TMPro;
using UnityEngine;

public class PannelUpgradeTorre : MonoBehaviour
{
    public TextMeshProUGUI txtMensagem;
    public TextMeshProUGUI txtCustoUpgrade;
    public PannelTorres pnlTorres;

    public GameObject btnNao;
    public GameObject btnPreco;
    public GameObject btnOk;

    private Torre torreSelecionada;
    private int custoUpgrade;

    public void Init(Torre torre)
    {
        torreSelecionada = torre;

        txtMensagem.text = $"Deseja evoluir a torre para o próximo nível?" +
            $"\r\nNv.{torreSelecionada.nivel} > Nv.{torreSelecionada.nivel + 1}\r\n+20% Velocidade" +
            $"\r\n+10% de ataque";

        custoUpgrade = torreSelecionada.preco * (torreSelecionada.nivel + 1);
        txtCustoUpgrade.text = $"${custoUpgrade}";

        // Botões do painel normal
        btnNao.SetActive(true);
        btnPreco.SetActive(true);
        btnOk.SetActive(false);
    }

    public void ComprarUpgrade()
    {
        int moedas = DBMng.ObterMoedasPlayer();

        if (moedas >= custoUpgrade)
        {
            // Comprar o upgrade da torre
            Torre torreAtualizada = torreSelecionada;

            torreAtualizada.nivel += 1;
            torreAtualizada.velocidadeAtaque *= 1.2f;
            torreAtualizada.poderAtaque *= 1.1f;

            // Salvar na memória
            DBMng.AtualizarNivelTorre(torreAtualizada, custoUpgrade);

            // Atualizar as torres no painel
            pnlTorres.Init();

            // Exibir mensagem de sucesso
            AtivarMensagemConfirmacao("Upgrade realizado com sucesso!");
        }
        else
        {
            // Exibir mensagem de erro
            AtivarMensagemConfirmacao(
                "Não foi possível realizar o upgrade, moedas insuficientes!"
            );
        }
    }

    private void AtivarMensagemConfirmacao(string mensagem)
    {
        txtMensagem.text = mensagem;

        // Desativa os botões de confirmação
        btnNao.SetActive(false);
        btnPreco.SetActive(false);

        // Ativa o botão OK
        btnOk.SetActive(true);
    }

    public void FecharPainel()
    {
        gameObject.SetActive(false);
    }
}