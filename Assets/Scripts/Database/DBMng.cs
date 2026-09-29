using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class DBMng
{
    //Endereço para salvar os dados
    private const string dadosJogadorChave = "DadosJogador";

    //Método para poder obter os dados do jogador
    public static Jogador CarregarDadosJogador()
    {
        //Pegar os dados salvos
        string dadosJson = PlayerPrefs.GetString(dadosJogadorChave);
        //Verificar se os dados existem, caso não exista, criar um novo objeto de jogador
        if (string.IsNullOrEmpty(dadosJson))
        {
            //Criar um novo jogador
            Jogador novoJogador = new Jogador
            {
                configuracoes = new Configuracao(),
                niveisCompletados = new List<Nivel>(),
                torresCompradas = new List<Torre>(),
                poderesComprados = new List<Poder>(),
                totalMoedas = 50000
            };
            //Configurar as variaveis com valores iniciais
            novoJogador.configuracoes.volumeMusica = 0.5f;
            novoJogador.configuracoes.volumeSFX = 1f;
            Torre torreInicial = new Torre();
            torreInicial.id = 1;
            SalvarDadosJogador(novoJogador);
        }
        //Obter novamente os dados salvos
        dadosJson = PlayerPrefs.GetString(dadosJogadorChave);
        //retornar os dados do jogador
        return JsonUtility.FromJson<Jogador>(dadosJson);
    }

    public static void SalvarDadosJogador(Jogador jogador)
    {
        //Converter o objeto jogador para JSON
        string dadosJson = JsonUtility.ToJson(jogador);
        //Salvar os dados usando PlayerPrefs
        PlayerPrefs.SetString(dadosJogadorChave, dadosJson);
        PlayerPrefs.Save();
    }

    public static void SalvarVolumes(float volumeMusica, float volumeSFX)
    {
        //Carregar os dados do jogador
        Jogador jogador = CarregarDadosJogador();
        //Atualizar os volumes na configuração do jogador
        jogador.configuracoes.volumeMusica = volumeMusica;
        jogador.configuracoes.volumeSFX = volumeSFX;
        //Salvar os dados atualizados do jogador
        SalvarDadosJogador(jogador);
    }

    public static void AdicionarNivel(Nivel novoNivel)
    {
        //Carregar os dados do jogador
        Jogador jogador = CarregarDadosJogador();
        if(jogador.niveisCompletados.Exists(nivel => nivel.id == novoNivel.id))
        {
            for(int i = 0; i < jogador.niveisCompletados.Count; i++)
            {
                if (jogador.niveisCompletados[i].id == novoNivel.id)
                {
                    if (jogador.niveisCompletados[i].melhorPontuacao < novoNivel.melhorPontuacao)
                    {
                        jogador.niveisCompletados[i] = novoNivel;
                        break;
                    }
                }
            }
        }
        else
        {
            //Adicionar o nível completado à lista de níveis do jogador
            jogador.niveisCompletados.Add(novoNivel);
        }

        //Salvar os dados atualizados do jogador
        SalvarDadosJogador(jogador);

    }

    public static bool ComprarTorre(Torre novaTorre)
    {
        try
        {
            //Buscar os dados do jogador
            Jogador jogador = CarregarDadosJogador();
            //Verificar se a torre já foi comprada
            if (jogador.torresCompradas.Exists(torre => torre.id == novaTorre.id))
            {
                Debug.LogWarning("A torre já foi comprada anteriormente.");
                return false;
            }
            //Verificar se o jogador tem moedas suficientes para comprar a torre
            if (jogador.totalMoedas < novaTorre.preco)
            {
                Debug.LogWarning("Moedas insuficientes para comprar a torre.");
                return false;
            }
            else
            {
                jogador.totalMoedas -= novaTorre.preco; //Deduzir o preço da torre das moedas do jogador
                jogador.torresCompradas.Add(novaTorre); //Adicionar a torre comprada à lista de torres do jogador
                SalvarDadosJogador(jogador); //Salvar os dados atualizados do jogador
                return true;
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Erro ao comprar torre: {ex.Message}");
            return false;
        }
    }

    public static bool ComprarPoder(Poder novoPoder)
    {
        try
        {
            //Buscar os dados do jogador
            Jogador jogador = CarregarDadosJogador();
            
            //Encontra o poder do jogador
            Poder poderJogador = jogador.poderesComprados.Find(poder => poder.id == novoPoder.id);

            if (poderJogador != null) { 
                if(poderJogador.quantidade >= Constants.LIMITE_MAXIMO_PODERES)
                {
                    Debug.LogWarning("O poder já foi comprado anteriormente.");
                    return false;
                }
                else if (jogador.totalMoedas < novoPoder.preco)
                {
                    Debug.LogWarning("Moedas insuficientes para comprar o poder.");
                    return false;
                }
                else
                {
                    poderJogador.quantidade = Constants.LIMITE_MAXIMO_PODERES; //Definir a quantidade do poder para o limite máximo
                    jogador.totalMoedas -= novoPoder.preco; //Deduzir o preço do poder das moedas do jogador
                    jogador.poderesComprados[jogador.poderesComprados.FindIndex(p => p.id == poderJogador.id)] = poderJogador;
                    SalvarDadosJogador(jogador); //Salvar os dados atualizados do jogador
                    Debug.Log("Novo poder comprado!");
                    return true;
                }
            }
            else
            {
                Debug.LogWarning("O poder não existe na lista!");
                return false;
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Erro ao comprar poder: {ex.Message}");
            return false;
        }
    }

    public static bool ConsumirPoder(int idPoder)
    {
        //Buscar os dados do jogador
        Jogador jogador = CarregarDadosJogador();

        //Verificar se o poder existe na lista de poderes do jogador
        Poder poderExistente = jogador.poderesComprados.Find(poder => poder.id == idPoder);

        if (poderExistente == null) {
            Debug.LogWarning("O poder não foi encontrado na lista de poderes do jogador.");
            return false;
        }
        else if(poderExistente.quantidade <= 0)
        {
            Debug.LogWarning("Jogador não possui uma quantidade para usar o poder.");
            return false;
        }

        poderExistente.quantidade--; //Reduzir a quantidade do poder em 1

        jogador.poderesComprados[jogador.poderesComprados.FindIndex(p => p.id == idPoder)] = poderExistente;

        //Salvar os dados atualizados do jogador
        SalvarDadosJogador(jogador);

        return true;
    }

    public static void InserirPoderesPlayer(Poder poder)
    {
        //Buscar os dados do jogador
        Jogador jogador = CarregarDadosJogador();

        if (jogador.poderesComprados.Exists(p => p.id == poder.id))
        {
            Debug.LogWarning("O poder já existe na lista de poderes do jogador.");
            return; // O poder já existe, não precisa adicionar novamente
        }

        //Definir a quantidade do  poder para 0
        poder.quantidade = 0;   

        //Adicionar o poder à lista de poderes do jogador
        jogador.poderesComprados.Add(poder);

        //Salvar os dados atualizados do jogador
        SalvarDadosJogador(jogador);
    }

    public static Poder BuscarPoderPlayer(int idPoder)
    {
        Jogador jogador = CarregarDadosJogador();

        Poder poderEncontrado = jogador.poderesComprados.Find(poder => poder.id == idPoder);

        if (poderEncontrado == null)
        {
            Debug.LogWarning("O poder não foi encontrado na lista de poderes do jogador.");
            return null;
        }

        return poderEncontrado;
    }

    public static Poder[] ObterTodosPoderesPlayer()
    {
        Jogador jogador = CarregarDadosJogador();
        Poder[] poderesEncontrados = jogador.poderesComprados.ToArray();
        return poderesEncontrados;
    }

    public static bool PossuiTorre(int id)
    {
        Jogador jogador = CarregarDadosJogador();

        return jogador.torresCompradas.Exists(torre => torre.id == id);
    }

    public static int TotalTorresAtivas()
    {
        Jogador jogador = CarregarDadosJogador();
        if(jogador == null)
        {
            return 0;
        }
        //Contando quantas torres estão ativas
        return jogador.torresCompradas.FindAll(torre => torre.estaAtivo).Count;
    }

    public static bool AtivarTorre(int idTorre)
    {
        Jogador jogador = CarregarDadosJogador();

        for(int i = 0; i < jogador.torresCompradas.Count; i++)
        {
            if (jogador.torresCompradas[i].id == idTorre)
            {
                jogador.torresCompradas[i].estaAtivo = true; // Ativar a torre
                SalvarDadosJogador(jogador);
                return true; // Retornar true para indicar que a torre foi ativada
            }
        }

        return false; // Retornar false se a torre não foi encontrada
    }

    public static bool DesativarTorre(int idTorre)
    {
        Jogador jogador = CarregarDadosJogador();

        for (int i = 0; i < jogador.torresCompradas.Count; i++)
        {
            if (jogador.torresCompradas[i].id == idTorre)
            {
                jogador.torresCompradas[i].estaAtivo = false; // Desativar a torre
                SalvarDadosJogador(jogador);
                return true; // Retornar true para indicar que a torre foi ativada
            }
        }

        return false; // Retornar false se a torre não foi encontrada
    }

    public static int ObterMoedasPlayer()
    {
        Jogador jogador = CarregarDadosJogador();
        return jogador.totalMoedas;
    }

    public static List<Torre> ObterTorresPlayer()
    {
        Jogador jogador = CarregarDadosJogador();
        return jogador.torresCompradas;
    }

    public static List<Torre> ObterTorresAtivasPlayer()
    {
        Jogador jogador = CarregarDadosJogador();
        return jogador.torresCompradas.Where(torre => torre.estaAtivo == true).ToList();
    }

    public static void AtualizarNivelTorre(Torre torreAtualizada, int custoUpgrade)
    {
        Jogador jogador = CarregarDadosJogador();
        for (int i = 0; i < jogador.torresCompradas.Count; i++)
        {
            if (jogador.torresCompradas[i].id == torreAtualizada.id)
            {
                jogador.torresCompradas[i] = torreAtualizada; // Atualizar a torre com os novos dados
                jogador.totalMoedas -= custoUpgrade; // Deduzir o custo do upgrade das moedas do jogador
                SalvarDadosJogador(jogador);
                return; // Retornar após atualizar a torre
            }
        }
    }

    public static Configuracao ObterConfiguracao()
    {
        Jogador jogador = CarregarDadosJogador();
        return jogador.configuracoes;
    }

    public static Nivel[] NiveisCompletados()
    {
        Jogador jogador = CarregarDadosJogador();
        return jogador.niveisCompletados.ToArray();
    }

    public static int ObterMelhorPontuacaoNivel(int idNivel)
    {
        Jogador jogador = CarregarDadosJogador();
        Nivel nivel = jogador.niveisCompletados.Find(n => n.id == idNivel);
        return nivel != null ? nivel.melhorPontuacao : 0;
    }
}
