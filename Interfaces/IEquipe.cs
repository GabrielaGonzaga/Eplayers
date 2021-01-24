using System.Collections.Generic;
using EPlayers_11_01_main.Models;

namespace EPlayers_11_01_main.Interfaces
{
    public interface IEquipe
    {
        //Métodos de CRUD - Contrato
        void Create(Equipe e);
        List<Equipe> ReadAll();
        void Update (Equipe e);
        void Delete(int id);
    }
}