using System.Collections.Generic;
using System.IO;
using EPlayers_11_01_main.Interfaces;

namespace EPlayers_11_01_main.Models
{
    public class Equipe : EplayerBase , IEquipe
    {
        public int IdEquipe { get; set; }    
        public string Nome { get; set; }
        public string Imagem { get; set; }

        public const string PATH = "DataBase/Equipe.csv";
        
        public Equipe(){
            
            CreateFolderAndFile(PATH);
        
        }

        //Preparação do método para a linha csv
        public string Prepare(Equipe e ){
            return $"{e.IdEquipe};{e.Nome};{e.Imagem}";
        }

        public void Create(Equipe e)
        {
            //Preparação de um array de string para o método AppenAllLines
            string[] linhas = { Prepare(e)} ; 
            //Acreção de uma nova linha
            File.AppendAllLines(PATH, linhas);
        }

        public void Delete(int id)
        {
            List<string> linhas = ReadAllLinesCSV(PATH);

            //2;SNK;sk
            //Remoção das linhas com o código comparado
            //ToString -> converte para texto
            linhas.RemoveAll(x => x.Split(";")[0] == id.ToString());

           //Reescrita do csv com a lista alterada
            RewriteCSV(PATH, linhas);
        }

        public List<Equipe> ReadAll()
        {
            List<Equipe> equipes = new List<Equipe>();
            //Leitura de todas a linhas do csv
            string[] linhas = File.ReadAllLines(PATH);

            foreach(string item in linhas){
                
                //1;VivoKeyd;vivo.jpg
                //[0] = 1
                //[1] = VivoKyed
                //[2] = vivo.jpg
                string[] linha = item.Split(";");
                Equipe novaEquipe = new Equipe();
                novaEquipe.IdEquipe = int.Parse( linha[0] );
                novaEquipe.Nome = linha[1];
                novaEquipe.Imagem = linha[2];

                equipes.Add(novaEquipe);

            }

            return equipes;
        }

        public void Update(Equipe e)
        {
            List<string> linhas = ReadAllLinesCSV(PATH);

            //2;SNK;sk
            //Remoção das linhas com o código comparado
            
            linhas.RemoveAll(x => x.Split(";")[0] == e.IdEquipe.ToString());
            //LINHA ACIMA -> expressão x + esse x com split + índice 0  + comparação com o e.IdEquipe + conversão para texto.

            //Adição á lista (equipes alteradas)
            linhas.Add(Prepare(e) );

            //Reescrita do csv com a lista alterada
            RewriteCSV(PATH, linhas);
        }
    }
}