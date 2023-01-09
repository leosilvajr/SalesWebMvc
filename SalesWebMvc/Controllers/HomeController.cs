using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Controllers
{
                    //Todo controlador vai ser uma subclasse de Controller
    public class HomeController : Controller
    {
                //Resultado de uma ação
                //IActionResult é uma interface do tipo generico para todo resultado de alguma ação
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            //Objeto ViewData        //Mensagem sera passada para a Chave "Message" do Objeto ViewData
            ViewData["Message"] = "Salles Web MVC App from C#";
            ViewData["Autor"] = "Leonardo Silva";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
