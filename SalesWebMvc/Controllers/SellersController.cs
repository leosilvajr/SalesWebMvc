using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
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
        private readonly DepartmentService _departmentService;
        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
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
            var departments = _departmentService.FindAll(); //Busca do Banco de Dados, todos os departamentos
            var viewModel = new SellerFormViewModel
            {
                Departments = departments
            };
            //Passando o objeto viewModel para o retorno da View
            return View(viewModel);
            //Abrir formulario para cadastrar vendedor
        }

        //Inserir Vendedor no Banco de Dados
        [HttpPost]
        [ValidateAntiForgeryToken]//Previnir ataques CSRF
        public IActionResult Create(Seller seller)
        {
            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index));//Retorna o Index de Sellers
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var obj = _sellerService.FindById(id.Value);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _sellerService.Remove(id);
            return RedirectToAction(nameof(Index));//Retorna o Index de Sellers

        }

        public IActionResult Details(int? id)
        {//Logica bem parecida com o DeleteGet
            if (id == null)
            {
                return NotFound();
            }
            var obj = _sellerService.FindById(id.Value);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }


    }
}
