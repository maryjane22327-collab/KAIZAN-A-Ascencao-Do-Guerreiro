using System;
using System.Collections.Generic;
[Serializable]
public class Jogador
{
    public Configuracao configuracoes;
    public List<Nivel> niveisCompletados;
    public List<Torre> torresCompradas;
    public List<Poder> poderesComprados;
    public int totalMoedas;
}
