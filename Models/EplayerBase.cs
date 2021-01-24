using System.Collections.Generic;
using System.IO;

namespace EPlayers_11_01_main.Models
{
    public class EplayerBase
    {
        public void CreateFolderAndFile(string _path){
            
            string folder = _path.Split("/")[0];

            if(!Directory.Exists(folder)){
                
                Directory.CreateDirectory(folder);
            
            }
            if(!File.Exists(_path)){
                File.Create(_path);
            }
        }

        public List<string> ReadAllLinesCSV(string path){

            List<string> linhas = new List<string>();

            //Using -> responsável por abrir e fechar o arquivo automaticamente
            //StreaReader -> Lr dados de um arquivo
            using(StringReader file = new StringReader(path)){

                string linha;

                //Percorrer as linhas com um laço while
                while( (linha = file.ReadLine()) !=null){
                    linhas.Add(linha);
                }

            }

            return linhas;
            
        }

            public void RewriteCSV( string path, List<string> linhas){
                //StreamWriter -> Escrever ddos em um arquivo
                using(StreamWriter output = new StreamWriter(path)){
                
                    foreach(var item in linhas){
                        output.Write(item + '\n');
                    }

                }
            }
    }
}