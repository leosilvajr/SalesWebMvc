using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;
        public SellersController(SellerService sellerService)
        {
            _sellerService = sellerService;
        }
        public IActionResult Index() //Chamo o controlador
        {
            //Controlador acessou o Modesl, pegou o dado na list e encaminha os dados para a view
            var list = _sellerService.FindAll();
            return View(list);
        }

        //Vamos criar a ação Create
        public IActionResult Create()
        {
            return View();
        }

        //Inserir Vendedor no Banco de Dados
        [HttpPost]
        [ValidateAntiForgeryToken]//Previnir ataques CSRF
        public IActionResult Create(Seller seller)
        {
            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index));//Retorna o Index de Sellers
        }
    }
}
