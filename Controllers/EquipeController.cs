using System;
using System.IO;
using EPlayers_11_01_main.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EPlayers_11_01_main.Controllers
{
        [Route("Equipe")]
        //http://localhost:5000/Equipe
      
        public class EquipeController : Controller
        {
        Equipe equipeModel = new Equipe();
        //http://localhost:5000/Equipe/Listar
        [Route("Listar")]
        public IActionResult Index()
        {
            ViewBag.Equipes = equipeModel.ReadAll();
            return View();
        }
        //http://localhost:5000/Cadastrar
        [Route("Cadastrar")]
        public IActionResult Cadastrar(IFormCollection form)
        {            
            Equipe novaEquipe = new Equipe();
            novaEquipe.IdEquipe = Int32.Parse( form["IdEquipe"] );
            novaEquipe.Nome = form["Nome"];

            // Upload Início

            //Verificação para saber se o ususário selecionou um arquivo
            if(form.Files.Count > 0)
            {
                //Recebi o arquivo do usuário e ele foi armazenado na variável file
                var file    = form.Files[0];
                var folder  = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Equipes");

                //Verificação existência da (pasta)
                //Se não, a criei
                if(!Directory.Exists(folder)){
                    Directory.CreateDirectory(folder);
                }
                                        //localhost:5001                                Equipes   imagem.jpg
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/", folder, file.FileName);
                using (var stream = new FileStream(path, FileMode.Create))  
                {  
                    file.CopyTo(stream);  
                }
                novaEquipe.Imagem  = file.FileName;                
            }
            else
            {
                novaEquipe.Imagem  = "padrao.png";
            }
            // Upload Final

            //Chama o método create para salvar a equipe

            equipeModel.Create(novaEquipe);

            //Atualiza a lista de equipe na View
            ViewBag.Equipes = equipeModel.ReadAll();
            return LocalRedirect("~/Equipe/Listar");
        }

        //http://localhost:5000/Equipe/1
        [Route("{id}")]
        public IActionResult Excluir(int id){
            equipeModel.Delete(id);
            ViewBag.Equipes = equipeModel.ReadAll();
            return LocalRedirect("~/Equipe/Listar");
        }
    }
}